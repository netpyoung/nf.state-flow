using UnityEngine;

namespace HelloFSM.AutoGen
{
    public partial class GenerateColor
    {
        public void Gen()
        {
            var r = Random.Range(0, 1.0f);
            var g = Random.Range(0, 1.0f);
            var b = Random.Range(0, 1.0f);
            var color = new UnityEngine.Color(r, g, b);
            this.Runner.generate.color = color;
            this.Runner.ProcessEvent(E_Event.EvtNext, color);
        }
    }
}
