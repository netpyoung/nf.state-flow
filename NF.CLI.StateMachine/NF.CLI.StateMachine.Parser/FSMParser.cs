using DotLiquid;
using Schemy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NF.CLI.StateMachine.Parser
{
    public class FSMParser
    {
        public FSM Parse(string s)
        {
            Interpreter.CreateSymbolTableDelegate extension = itpr => new Dictionary<Symbol, object>
            {
                { Symbol.FromString("define-fsm"), NativeProcedure.Create<string,  FSM>(FSM.Gen) },
                { Symbol.FromString("define-state"), NativeProcedure.Create<string,  State>(State.Gen) },

                { Symbol.FromString("AddState"), NativeProcedure.Create<FSM, State,  FSM>(FSM.AddState) },
                { Symbol.FromString("AddEvent"), NativeProcedure.Create<State, Event, State>(State.AddEvent) },
                { Symbol.FromString("AddArgument"), NativeProcedure.Create<Event, string, Event>(Event.AddArgument) },
                { Symbol.FromString("ConnectNextState"), NativeProcedure.Create<Event, string, Event>(Event.ConnectNextState) },


                { Symbol.FromString("define-event"), NativeProcedure.Create<string, Event>(Event.Gen) },
                { Symbol.FromString("name"), NativeProcedure.Create<Symbol, string>((sym) => {
                return sym.ToString().TrimStart('\'');
                })
            },
            };

            var interpreter = new Interpreter(new[] { extension }, new ReadOnlyFileSystemAccessor());
            var stream = typeof(FSMParser).Assembly.GetManifestResourceStream("defstate.ss");
            using (TextReader reader = new StreamReader(stream))
            {
                var res = interpreter.Evaluate(reader);
                if (res.Error != null)
                {
                    Console.Error.WriteLine(res.Error);
                    return null;
                }
            }

            using (TextReader reader = new StringReader(s))
            {
                var res = interpreter.Evaluate(reader);
                if (res.Error != null)
                {
                    Console.Error.WriteLine(res.Error);
                    return null;
                }

                return res.Result as FSM;
            }
        }
    }

    public class FSM : Drop
    {
        public string Name { get; set; }
        public List<State> States { get; set; } = new List<State>();

        public FSM(string name)
        {
            this.Name = name;
        }

        internal static FSM Gen(string name)
        {
            return new FSM(name);
        }

        public static FSM AddState(FSM fsm, State state)
        {
            var find = fsm.States.Find(x => x.Name == state.Name);
            if (find == null)
            {
                fsm.States.Add(state);
                return fsm;
            }
            find.Events.AddRange(state.Events);
            return fsm;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"# fsm {Name}\n");
            foreach (var state in this.States)
            {
                sb.Append($"{state}");
            }
            return sb.ToString();
        }

        public Hash ToHash()
        {
            return Hash.FromAnonymousObject(new { fsm = this });
        }
    }

    public class State : Drop
    {
        public string Name { get; set; }
        public List<Event> Events { get; set; } = new List<Event>();
        Dictionary<string, string> _dic = new Dictionary<string, string>();
        public State(string name)
        {
            this.Name = name;
        }

        internal static State Gen(string name)
        {
            return new State(name);
        }

        public static State AddEvent(State state, Event evt)
        {
            state.Events.Add(evt);
            return state;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"## state {Name}\n");
            foreach (var x in this.Events)
            {
                sb.Append($"{x}");
            }
            sb.Append("\n");
            return sb.ToString();
        }
    }

    public class Event : Drop
    {
        public string Name { get; set; }
        public List<string> Args { get; set; } = new List<string>();
        public string NextState { get; set; }

        public Event(string name)
        {
            this.Name = name;
        }

        internal static Event Gen(string name)
        {
            return new Event(name);
        }

        public static Event AddArgument(Event evt, string arg)
        {
            evt.Args.Add(arg);
            return evt;
        }

        public static Event ConnectNextState(Event evt, string arg)
        {
            evt.NextState = arg;
            return evt;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"### event {Name}");
            foreach (var x in this.Args)
            {
                sb.Append($" {x}");
            }

            if (!string.IsNullOrEmpty(NextState))
            {
                sb.Append($" -> {NextState}");
            }
            sb.Append("\n");
            return sb.ToString();
        }
    }
}
