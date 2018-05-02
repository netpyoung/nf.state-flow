namespace HelloFSM.AutoGen
{
    public partial class DisplayColor : HelloFSMRunner.BaseState
    {
        public enum E_Event
        {
           EvtNext
        }

        public DisplayColor() : base(HelloFSMRunner.E_State.DisplayColor)
        {
        }
    }
}
