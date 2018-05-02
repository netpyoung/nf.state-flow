namespace HelloFSM.AutoGen
{
    public partial class ValidateColor : HelloFSMRunner.BaseState
    {
        public enum E_Event
        {
           EvtInvalid,
           EvtValid
        }

        public ValidateColor() : base(HelloFSMRunner.E_State.ValidateColor)
        {
        }
    }
}
