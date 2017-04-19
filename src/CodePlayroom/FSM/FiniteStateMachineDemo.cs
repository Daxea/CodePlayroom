using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CodePlayroom.FSM
{
    public class FiniteStateMachineDemo : IDemo
    {
        public string Name => "Finite State Machine Demo";
        public void OnComplete() => Thread.Sleep(100);

        private FiniteStateMachine<AnimationState, AnimationTransition> _stateMachine;

        public FiniteStateMachineDemo()
        {
            _stateMachine = new FiniteStateMachine<AnimationState, AnimationTransition>();

            var state1 = new AnimationState("Walk Cycle");
            _stateMachine.AddState(state1);

            var state2 = new AnimationState("Run Cycle");
            _stateMachine.AddState(state2);
            _stateMachine.CreateTransition<AnimationTransition>(state1, state2);
        }

        public void Run(params string[] args)
        {
            while (_stateMachine.CurrentState != null)
            {
                _stateMachine.CurrentState.Animate();
                Thread.Sleep(1000);
                _stateMachine.ProcessTransitions();
            }
        }
    }

    public class AnimationState : IState
    {
        public bool IsPlaying { get; private set; }

        private readonly string _text;

        public AnimationState(string text)
        {
            _text = text;
        }

        public void Animate()
        {
            Console.WriteLine($"Begin {_text}");
            IsPlaying = true;
            Thread.Sleep(1000);
            IsPlaying = false;
            Console.WriteLine($"End {_text}\n");
        }
    }

    public class AnimationTransition : ITransition
    {
        public IState From { get; set; }
        public IState To { get; set; }

        public bool Process(IState state)
        {
            var animState = state as AnimationState;
            if (animState == null)
                return true;
            return !animState.IsPlaying;
        }
    }
}