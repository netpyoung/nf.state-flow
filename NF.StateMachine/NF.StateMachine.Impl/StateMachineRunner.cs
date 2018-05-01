using System;
using System.Collections.Generic;
using NF.StateMachine.Interface;

namespace NF.StateMachine.Impl
{
    public class StateMachineRunner : IStateRunner
    {
        protected readonly HashSet<IState> States = new HashSet<IState>();
        private readonly Dictionary<Type, IState> _typedic = new Dictionary<Type, IState>();

        private IState _current;

        public IState CurrentState => this._current;

        public bool TryChangeState(IState nextState)
        {
            if (!IsContains(nextState))
            {
                return false;
            }

            this.__ChangeState(nextState);

            return true;
        }

        private void __ChangeState(IState nextState)
        {
            IState before = this._current;
            before?.OnExit();
            this._current = nextState;
            OnStateChanged(before, this._current);
            this._current.OnEnter();
        }

        private void __ChangeState(IState nextState, params object[] args)
        {
            IState before = this._current;
            before?.OnExit();
            this._current = nextState;
            OnStateChanged(before, this._current);
            this._current.OnEnter(args);
        }

        public E_TICKRESULT Tick(float deltaTime)
        {
            IState current = this.CurrentState;
            if (current == null)
            {
                return E_TICKRESULT.ERR_CURRSTATE_IS_NULL;
            }

            if (!current.Tick(deltaTime))
            {
                return E_TICKRESULT.CURRSTATE_UNTICKABLE;
            }

            // TODO(pyoung): apply state changes.
            return E_TICKRESULT.OK;
        }


        public bool AddState(IState state)
        {
            if (IsContains(state))
            {
                return false;
            }

            this.States.Add(state);
            state.SetRunner(this);
            return true;
        }

        private bool IsContains(IState state)
        {
            return this.States.Contains(state);
        }

        public virtual void SetState(IState state)
        {
            if (!IsContains(state))
            {
                return;
            }

            this.__ChangeState(state);
        }

        public virtual void SetState(IState state, params object[] args)
        {
            if (!IsContains(state))
            {
                return;
            }

            this.__ChangeState(state, args);
        }


        private IState GetState(Type t)
        {
            IState state;
            if (this._typedic.TryGetValue(t, out state))
            {
                return state;
            }

            foreach (IState s in this.States)
            {
                this._typedic[s.GetType()] = s;
            }

            this._typedic.TryGetValue(t, out state);

            return state;
        }

        public void SetState<TState>() where TState : IState
        {
            this.__ChangeState(GetState(typeof(TState)));
        }

        public void SetState<TState>(params object[] args) where TState : IState
        {
            this.__ChangeState(GetState(typeof(TState)), args);
        }

        public event Action<IState, IState> OnStateChanged = delegate { };
    }
}