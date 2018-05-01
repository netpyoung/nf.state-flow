namespace NF.StateMachine.Interface
{
    public enum E_TICKRESULT
    {
        ERR_LINK_FAIL_TO_FIND = -2,
        ERR_CURRSTATE_IS_NULL = -1,
        OK = 1,
        CURRSTATE_UNTICKABLE = 2,
    }
}