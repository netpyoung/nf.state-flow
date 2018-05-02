using UnityEngine.UI;

namespace HelloFSM.AutoGen
{
    public partial class HelloFSMRunner
    {
        public Image generate;
        public Image display;

        public HelloFSMRunner(Image generate, Image display)
        {
            this.generate = generate;
            this.display = display;
        }
    }
}
