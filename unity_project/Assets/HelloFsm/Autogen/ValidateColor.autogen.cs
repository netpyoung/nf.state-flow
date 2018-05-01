namespace HelloFsm.AutoGen
{
    public partial class ValidateColor : HelloFsmRunner.BaseState
    {
        public enum E_Event
        {
            EvtInvalid,
            EvtValid,
        }

        public ValidateColor() : base(HelloFsmRunner.E_State.ValidateColor)
        {
        }
    }
}
