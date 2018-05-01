using NF.StateMachine.Impl;

namespace HelloFsm.AutoGen
{
    public partial class HelloFsmRunner : StateMachineRunner<HelloFsmRunner.E_State>
    {
        public enum E_State
        {
            GenerateColor,
            ValidateColor,
            DisplayColor,
        }
    }

    public partial class HelloFsmRunner : StateMachineRunner<HelloFsmRunner.E_State>
    {
        public abstract class BaseState : BaseState<HelloFsmRunner, E_State>
        {
            public BaseState(E_State id) : base(id)
            {
            }
        }
    }

    public partial class HelloFsmRunner : StateMachineRunner<HelloFsmRunner.E_State>
    {
        public void Init()
        {
            this.AddState(new GenerateColor());
            this.AddState(new ValidateColor());
            this.AddState(new DisplayColor());
            this.SetState<GenerateColor>();
        }

        public void ProcessEvent(GenerateColor.E_Event e, params object[] args)
        {
            if (e == GenerateColor.E_Event.EvtNext)
            {
                base.SetState<ValidateColor>(args);
            }
        }

        public void ProcessEvent(ValidateColor.E_Event e, params object[] args)
        {
            if (e == ValidateColor.E_Event.EvtValid)
            {
                base.SetState<DisplayColor>(args);
            }
            if (e == ValidateColor.E_Event.EvtInvalid)
            {
                base.SetState<GenerateColor>(args);
            }
        }

        public void ProcessEvent(DisplayColor.E_Event e, params object[] args)
        {
            if (e == DisplayColor.E_Event.EvtNext)
            {
                base.SetState<GenerateColor>(args);
            }
        }
    }
}
