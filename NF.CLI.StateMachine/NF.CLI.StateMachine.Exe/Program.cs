using System;
using NF.CLI.StateMachine.Parser;
using NF.CLI.StateMachine.Renderer.PUML;
using System.IO;
using NF.CLI.StateMachine.Renderer.AutogenCS;

namespace NF.CLI.StateMachine.Exe
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = @"C:\__DOING\nf.statemachine\sample_state.ss";
            FSMParser l = new FSMParser();
            var fsm = l.Parse(File.ReadAllText(x));
            //Console.WriteLine(fsm);

            using (RendererPUML r1 = new RendererPUML())
            {
                Console.WriteLine(r1.Render(fsm));
            }

            using (RendererAutogenCS r2 = new RendererAutogenCS())
            {
                Console.WriteLine(r2.RenderStateMachine(fsm));

                foreach (var state in fsm.States)
                {
                    Console.WriteLine(r2.RenderState(fsm, state));
                }
            }
        }
    }
}
