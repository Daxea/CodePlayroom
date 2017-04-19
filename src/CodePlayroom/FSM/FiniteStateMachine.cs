using System;
using System.Collections.Generic;
using System.Linq;
using CodePlayroom.Math;

namespace CodePlayroom.FSM
{
    public class FiniteStateMachine<TState, TTransition>
        where TState : class, IState
        where TTransition : class, ITransition
    {
        private IList<TState> _states = new List<TState>();
        private IList<TTransition> _transitions = new List<TTransition>();

        private int _currentState = -1;
        public TState CurrentState => _currentState.IsWithin(0, _states.Count, BoundInclusion.Exclusive) ? _states[_currentState] : null;

        public void ProcessTransitions()
        {
            var state = CurrentState;
            if (state == null)
                return;

            var transitions = _transitions.Where(t => t.From == state).ToArray();
            if (transitions.Length == 0)
            {
                _currentState = -1;
                return;
            }

            foreach (var transition in transitions)
            {
                if (!transition.Process(state))
                    continue;
                if (transition.To == null || transition.To == state)
                {
                    _currentState = -1;
                    return;
                }
                _currentState = _states.IndexOf((TState)transition.To);
                return;
            }
        }

        public void AddState(TState state)
        {
            _states.Add(state);
            if (_states.Count == 1 && _currentState < 0)
                _currentState = 0;
        }

        public T CreateTransition<T>(TState from, TState to)
            where T : TTransition
        {
            var transition = Activator.CreateInstance<T>();
            transition.From = from;
            transition.To = to;
            _transitions.Add(transition);
            return transition;
        }

        public T CreateTransition<T>(TState from, TState to, params object[] parameters)
            where T : TTransition
        {
            var transition = (T)Activator.CreateInstance(typeof(T), parameters);
            transition.From = from;
            transition.To = to;
            _transitions.Add(transition);
            return transition;
        }
    }

    public interface IState
    {

    }

    public interface ITransition
    {
        IState From { get; set; }
        IState To { get; set; }
        bool Process(IState state);
    }
}