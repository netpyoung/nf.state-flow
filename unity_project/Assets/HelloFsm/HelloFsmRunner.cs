using UnityEngine.UI;
using NF.StateMachine.Impl;

namespace HelloFsm.AutoGen
{
    public partial class HelloFsmRunner : StateMachineRunner<HelloFsmRunner.E_State>
    {
        public Image generate;
        public Image display;

        public HelloFsmRunner(Image generate, Image display)
        {
            this.generate = generate;
            this.display = display;
        }
    }
}
