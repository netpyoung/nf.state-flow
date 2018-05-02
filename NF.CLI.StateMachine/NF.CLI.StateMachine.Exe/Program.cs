using System;
using NF.CLI.StateMachine.Parser;
using NF.CLI.StateMachine.Renderer.PUML;
using System.IO;
using NF.CLI.StateMachine.Renderer.AutogenCS;
using CommandLine;
using System.Collections.Generic;

namespace NF.CLI.StateMachine.Exe
{
    class Program
    {
        internal class Options
        {
            [Option('I', "input", Required = true, HelpText = "input state .ss")]
            public string Input { get; set; }

            [Option('P', "puml", Required = false, HelpText = "generate puml")]
            public string Puml { get; set; }

            [Option('C', "cs", Required = false, HelpText = "generate cs directory")]
            public string Cs { get; set; }
        }

        static void Main(string[] args)
        {
            ParserResult<Options> result = CommandLine.Parser.Default.ParseArguments<Options>(args);
            result.WithParsed(Run).WithNotParsed(Fail);
        }

        private static void Fail(IEnumerable<Error> obj)
        {
            foreach (var o in obj)
            {
                Console.Error.WriteLine(o);
            }
        }

        private static void Run(Options obj)
        {
            string inputFpath = obj.Input;
            string pumlOutFpath = obj.Puml;
            string csOutputDir = obj.Cs;


            string txt = File.ReadAllText(inputFpath);

            FSMParser parser = new FSMParser();
            FSM fsm = parser.Parse(txt);
            if (fsm == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(pumlOutFpath))
            {
                using (RendererPUML renderer = new RendererPUML())
                {
                    string renderResult = renderer.Render(fsm);
                    File.WriteAllText(pumlOutFpath, renderResult);
                }
            }

            if (!string.IsNullOrEmpty(csOutputDir))
            {
                if (!Directory.Exists(csOutputDir))
                {
                    Directory.CreateDirectory(csOutputDir);
                }

                using (RendererAutogenCS renderer = new RendererAutogenCS())
                {
                    string runnerCsFname = $"{ fsm.Name}Runner.autogen.cs";

                    File.WriteAllText(Path.Combine(csOutputDir, runnerCsFname), renderer.RenderStateMachine(fsm));

                    foreach (var state in fsm.States)
                    {
                        string stateCsFname = $"{state.Name}.autogen.cs";
                        File.WriteAllText(Path.Combine(csOutputDir, stateCsFname), renderer.RenderState(fsm, state));
                    }
                }
            }
        }
    }
}
