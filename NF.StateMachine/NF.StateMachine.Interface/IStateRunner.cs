using System;

namespace NF.StateMachine.Interface
{
    public interface IStateRunner
    {
        E_TICKRESULT Tick(float deltaTime);
        bool TryChangeState(IState nextState);
        void SetState(IState state);
        void SetState(IState state, params object[] args);
        void SetState<TState>() where TState : IState;
        void SetState<TState>(params object[] args) where TState : IState;

        IState CurrentState { get; }
        bool AddState(IState state);
    }

    public interface IStateRunner<T> : IStateRunner where T : struct, IConvertible
    {
        bool TryChangeState(IState<T> nextState);
        IState<T> SetState(T t);
    }
}