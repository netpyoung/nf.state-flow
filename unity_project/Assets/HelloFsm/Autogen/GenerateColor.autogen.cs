namespace HelloFSM.AutoGen
{
    public partial class GenerateColor : HelloFSMRunner.BaseState
    {
        public enum E_Event
        {
           EvtNext
        }

        public GenerateColor() : base(HelloFSMRunner.E_State.GenerateColor)
        {
        }
    }
}
