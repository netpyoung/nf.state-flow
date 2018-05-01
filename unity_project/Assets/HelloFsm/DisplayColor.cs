using UnityEngine;

namespace HelloFsm.AutoGen
{
    public partial class DisplayColor : HelloFsmRunner.BaseState
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
