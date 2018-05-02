using UnityEngine;

namespace HelloFSM.AutoGen
{
    public partial class ValidateColor
    {
        public override void OnEnter(params object[] args)
        {
            this.color = (Color)args[0];
 
        }
        Color color;
        float a = 0;

        public override bool Tick(float deltaTime)
        {
            this.a += deltaTime;
            if (a < 1)
            {
                return true;
            }

            a = 0;
            if (this.color.r > 0.5)
            {
                Runner.ProcessEvent(E_Event.EvtValid, color);
            }
            else
            {
                Runner.ProcessEvent(E_Event.EvtInvalid);
            }
            return true;
        }
    }
}
