using UnityEngine;

namespace HelloFSM.AutoGen
{
    public partial class DisplayColor
    {
        public override void OnEnter(params object[] args)
        {
            Color color = (Color)args[0];
            Debug.Log(color);
            Runner.display.color = color;
            Runner.ProcessEvent(E_Event.EvtNext);
        }
    }
}
