using System;
using NF.StateMachine.Interface;

namespace NF.StateMachine.Impl
{
    public class BaseState<TRunner, T> : IState<T>
        where TRunner : IStateRunner<T>
        where T : struct, IConvertible
    {
        public T ID { get; private set; }
        public TRunner Runner { get; private set; }

        public BaseState(T id)
        {
            this.ID = id;
        }

        public virtual void OnEnter(params object[] args)
        {
        }

        public virtual bool Tick(float deltaTime)
        {
            return true;
        }

        public virtual void OnExit()
        {
        }


        public void SetRunner(IStateRunner runner)
        {
            this.Runner = (TRunner)runner;
        }

        public void SetState<U>() where U : IState<T>
        {
            this.Runner.SetState<U>();
        }

        public void SetState<U>(params object[] args) where U : IState<T>
        {
            this.Runner.SetState<U>(args);
        }
    }
}
