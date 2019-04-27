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
                case var t when t.state is UnknownUserState:
                    return (new WelcomeState(), new WelcomeCommand());
                case var t when t.state is WelcomeState:
                    return ProcessWelcomeState(t.state, t.transition);
                case var t when t.state is TopicSelectionState:
                    return ProcessTopicSelectionState(t.state, t.transition);
                case var t when t.state is LevelSelectionState:
                    return ProcessLevelSelectionState(t.state, t.transition);
                case var t when t.state is TaskState:
                    return ProcessTaskState(t.state, t.transition);
            }

            return default;
        }

        private static (State, ICommand) ProcessTaskState(State state, Transition transition)
        {
            switch (transition)
            {
                case BackTransition backTransition:
                    var taskState = state as TaskState;
                    return (new LevelSelectionState(taskState.TopicId), new SelectLevelCommand(taskState.TopicId));
                case CorrectTransition correctTransition:
                {
                    switch (correctTransition.Content)
                    {
                        case "next":
                            return (state, new NextTaskCommand());
                        case "hint":
                            return (state, new ShowHintCommand());
                        default:
                            return (state, new CheckTaskCommand(correctTransition.Content));
                    }
                }
            }

            return (new WelcomeState(), new WelcomeCommand());
        }

        private static (State, ICommand) ProcessLevelSelectionState(State state, Transition transition)
        {
            switch (transition)
            {
                case BackTransition backTransition:
                    return (new TopicSelectionState(), new SelectTopicCommand());
                case CorrectTransition correctTransition:
                    return
                        (new TaskState(((LevelSelectionState) state).TopicId, correctTransition.Content),
                            new ShowTaskCommand(((LevelSelectionState) state).TopicId, correctTransition.Content));
            }

            return (new WelcomeState(), new WelcomeCommand());
        }

        private static (State, ICommand) ProcessTopicSelectionState(State state, Transition transition)
        {
            switch (transition)
            {
                case BackTransition backTransition:
                    return (new WelcomeState(), new WelcomeCommand());
                case CorrectTransition correctTransition:
                    return (new LevelSelectionState(correctTransition.Content),
                        new SelectLevelCommand(correctTransition.Content));
            }

            return (new WelcomeState(), new WelcomeCommand());
        }

        private static (State, ICommand) ProcessWelcomeState(State state, Transition transition)
        {
            switch (transition)
            {
                case CorrectTransition correctTransition:
                    switch (correctTransition.Content)
                    {
                        case "topics":
                            return (new TopicSelectionState(), new SelectTopicCommand());
                        case "info":
                            return (new WelcomeState(), new AboutCommand());
                        case "feedback":
                            return (new WelcomeState(), new FeedBackCommand());
                    }

                    break;
            }

            return (new WelcomeState(), new WelcomeCommand());
        }
    }
}