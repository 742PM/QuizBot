using System;
using QuizBotCore.Commands;
using QuizBotCore.Database;
using QuizBotCore.States;
using QuizRequestService;

namespace QuizBotCore
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

        public (State state, ICommand command) GetNextState<TState, TTransition>(
            TState currentState,
            TTransition transition) where TState : State where TTransition : Transition
        {
            switch ((state: currentState, transition: transition))
            {
                case var t when t.transition is InvalidTransition:
                    return (currentState, new InvalidActionCommand("You got the wrong door buddy!"));
                case var t when t.state is UnknownUserState:
                    return (new WelcomeState(), new WelcomeCommand());
                case var t when t.transition is BackTransition && t.state is WelcomeState:
                    return (currentState, new InvalidActionCommand("The leatherman club is two blocks below"));
            }

            return default;
        }

        private static (State, ICommand) ProcessGodState(UnknownUserState state) => (new WelcomeState(), new WelcomeCommand());

        private static (State, ICommand) ProcessUnrecognizedState() => throw new NotImplementedException();

        private static (State, ICommand) ProcessTaskState(TaskState state) => throw new NotImplementedException();

        private static (State, ICommand) ProcessLevelSelectionState(LevelSelectionState state) =>
            throw new NotImplementedException();

        private static (State, ICommand) ProcessTopicSelectionState(TopicSelectionState state) =>
            throw new NotImplementedException();

        private static (State, ICommand) ProcessAdminState(AdminState state) => throw new NotImplementedException();

        private static (State, ICommand) ProcessAboutState(AboutState state) => throw new NotImplementedException();

        private static (State, ICommand) ProcessWelcomeState(WelcomeState state)
        {
            var command = new WelcomeCommand();
            return (state, command);
        }
    }
}
