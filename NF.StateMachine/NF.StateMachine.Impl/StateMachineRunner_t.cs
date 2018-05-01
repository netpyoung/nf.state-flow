using System;
using System.Collections.Generic;
using System.Linq;
using NF.StateMachine.Interface;

namespace NF.StateMachine.Impl
{
    // TODO(pyoung): need more thiking.
    // how to dynamic adding state.
    public class StateMachineRunner<T> : StateMachineRunner, IStateRunner<T> where T : struct, IConvertible
    {
        private readonly Dictionary<T, IState<T>> _dic = new Dictionary<T, IState<T>>();

        public T Current
        {
            get
            {
                IState<T> state = (IState<T>) this.CurrentState;

                return state.ID;
            }
            private set
            {
                T t = value;
                IState<T> first = GetState(t);
                base.SetState(first);
            }
        }

        private IState<T> GetState(T t)
        {
            IState<T> state;
            if (this._dic.TryGetValue(t, out state))
            {
                return state;
            }

            foreach (IState<T> s in this.States.Cast<IState<T>>())
            {
                this._dic[s.ID] = s;
            }

            this._dic.TryGetValue(t, out state);

            return state;
        }

        public IState<T> SetState(T t)
        {
            this.Current = t;

            return (IState<T>)this.CurrentState;
        }

        public bool TryChangeState(IState<T> nextState)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"<State : {this.CurrentState}>";
        }
    }
}