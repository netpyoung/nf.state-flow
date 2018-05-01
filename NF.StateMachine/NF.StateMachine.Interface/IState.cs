using System;

namespace NF.StateMachine.Interface
{
    public interface IState
    {
        void OnEnter(params object[] args);
        bool Tick(float deltaTime);
        void OnExit();
        void SetRunner(IStateRunner runner);
    }

    public interface IState<T> : IState where T : struct, IConvertible
    {
        T ID { get; }
        void SetState<U>() where U : IState<T>;
        void SetState<U>(params object[] args) where U : IState<T>;
    }
}