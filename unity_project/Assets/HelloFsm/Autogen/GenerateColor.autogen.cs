namespace HelloFsm.AutoGen
{
    public partial class GenerateColor : HelloFsmRunner.BaseState
    {
        public enum E_Event
        {
            EvtNext
        }

        public GenerateColor() : base(HelloFsmRunner.E_State.GenerateColor)
        {
        }
    }
}
