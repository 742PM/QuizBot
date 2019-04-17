using System;
using System.Collections.Generic;

namespace QuizWebHookBot.StateMachine
{
    public class StateMachine<TCommand> : IStateMachine<TCommand>, StateMachine<TCommand>.IStateMachineBuilder
    {
        // public delegate (State, TCommand) Run<State, Transition>(in TState state, in TTransition transition) where TState : State where TTransition : Transition;

        private readonly Dictionary<(Type, Type), Func<State, Transition, (State, TCommand)>> runners;

        public StateMachine()
        {
            runners = new Dictionary<(Type, Type), Func<State, Transition, (State, TCommand)>>();
        }

        /// <inheritdoc />
        (State state, TCommand command) IStateMachine<TCommand>.GetNextState<TState, TTransition>(TState currentState, TTransition transition)
        {
            if (runners.Count == 0 || currentState == null || transition == null )
                return (State.Empty, default);
            return runners.TryGetValue((typeof(TState), typeof(TTransition)), out var runner)
                       ? runner(currentState, transition)
                       : (State.Empty, default);
        }

        /// <inheritdoc />
        IStateMachineBuilder IStateMachineBuilder.Case<TState, TTransition>(
            Func<TState, TTransition, (State, TCommand)> run)
        {
            runners[(typeof(TState), typeof(TTransition))] = (s, t) => run((TState) s, (TTransition) t);
            return this;
        }

        /// <inheritdoc />
        IStateMachine<TCommand> IStateMachineBuilder.Build() => this;

        public interface IStateMachineBuilder
        {
            IStateMachineBuilder Case<TState, TTransition>(Func<TState, TTransition, (State, TCommand)> run)
                where TState : State where TTransition : Transition;

            IStateMachine<TCommand> Build();
        }
    }

    public static class StateMachine
    {
        public static StateMachine<TCommand>.IStateMachineBuilder For<TCommand>() => new StateMachine<TCommand>();
    }

    
}
