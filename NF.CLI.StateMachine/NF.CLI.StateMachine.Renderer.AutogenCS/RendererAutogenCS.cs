using DotLiquid;
using NF.CLI.StateMachine.Parser;
using System.IO;
using System;

namespace NF.CLI.StateMachine.Renderer.AutogenCS
{
    public class RendererAutogenCS : IDisposable
    {
        public string RenderStateMachine(FSM fsm)
        {
            Template.RegisterSafeType(typeof(FSM), new[] { "Name", "States" });
            Template.RegisterSafeType(typeof(State), new[] { "Name", "Events" });
            Template.RegisterSafeType(typeof(Event), new[] { "Name", "NextState", "Args" });
            
            Stream stream = typeof(RendererAutogenCS).Assembly.GetManifestResourceStream("statemachine.liquid");
            string liquid = new StreamReader(stream).ReadToEnd();
            Template template = Template.Parse(liquid);
            string rendered = template.Render(fsm.ToHash());
            return rendered;
        }

        public string RenderState(FSM fsm, State state)
        {
            Template.RegisterSafeType(typeof(FSM), new[] { "Name", "States" });
            Template.RegisterSafeType(typeof(State), new[] { "Name", "Events" });
            Template.RegisterSafeType(typeof(Event), new[] { "Name", "NextState", "Args" });

            Stream stream = typeof(RendererAutogenCS).Assembly.GetManifestResourceStream("state.liquid");
            string liquid = new StreamReader(stream).ReadToEnd();
            Template template = Template.Parse(liquid);
            string rendered = template.Render(Hash.FromAnonymousObject(new { fsm, state }));
            return rendered;
        }

        public void Dispose()
        {
        }
    }
}
