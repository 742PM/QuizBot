using System;
using System.Diagnostics;
using QuizWebHookBot.Commands;
using QuizWebHookBot.Database;
using QuizWebHookBot.Services;
using QuizWebHookBot.StateMachine.States;

namespace QuizWebHookBot.StateMachine
{
    public class TelegramStateMachine : IStateMachine<ICommand>
    {
        private readonly IQuizService service;
        private readonly IUserRepository userRepository;

        public TelegramStateMachine(IQuizService service, IUserRepository userRepository)
        {
            this.service = service;
            this.userRepository = userRepository;
        }
        public (State state, ICommand command) GetNextState<TState, TTransition>(TState currentState, TTransition transition) where TState : State where TTransition : Transition => throw new NotImplementedException();


        private static (State, ICommand) ProcessGodState(UnknownState state)
        {
            return (new WelcomeState(), new Welcome());
        }

        private static (State, ICommand) ProcessUnrecognizedState()
        {
            throw new NotImplementedException();
        }

        private static (State, ICommand) ProcessTaskState(TaskState state)
        {
            throw new NotImplementedException();
        }

        private static (State, ICommand) ProcessLevelSelectionState(LevelSelectionState state)
        {
            throw new NotImplementedException();
        }

        private static (State, ICommand) ProcessTopicSelectionState(TopicSelectionState state)
        {
            throw new NotImplementedException();
        }

        private static (State, ICommand) ProcessAdminState(AdminState state)
        {
            throw new NotImplementedException();
        }

        private static (State, ICommand) ProcessAboutState(AboutState state)
        {
            throw new NotImplementedException();
        }

        private static (State, ICommand) ProcessWelcomeState(WelcomeState state)
        {
            var command = new Welcome();
            return (state, command);
        }

    }
}