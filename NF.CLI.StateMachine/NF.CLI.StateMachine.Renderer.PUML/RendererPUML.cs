using System;
using System.IO;
using DotLiquid;
using NF.CLI.StateMachine.Parser;

namespace NF.CLI.StateMachine.Renderer.PUML
{
    public class RendererPUML : IDisposable
    {
        public string Render(FSM fsm)
        {
            Template.RegisterSafeType(typeof(FSM), new[] { "Name", "States" });
            Template.RegisterSafeType(typeof(State), new[] { "Name", "Events" });
            Template.RegisterSafeType(typeof(Event), new[] { "Name", "NextState", "Args" });

            Stream stream = typeof(RendererPUML).Assembly.GetManifestResourceStream("puml.liquid");
            string liquid = new StreamReader(stream).ReadToEnd();
            Template template = Template.Parse(liquid);
            string rendered = template.Render(fsm.ToHash());
            return rendered;
        }

        public void Dispose()
        {
        }
    }
}
