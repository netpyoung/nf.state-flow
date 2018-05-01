namespace HelloFsm.AutoGen
{
    public partial class DisplayColor : HelloFsmRunner.BaseState
    {
        public DisplayColor() : base(HelloFsmRunner.E_State.DisplayColor)
        {
        }

        public enum E_Event
        {
            EvtNext
        }
    }
}
