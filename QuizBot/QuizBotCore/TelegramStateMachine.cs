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
                case var t when t.transition is HelpTransition:
                    return (new TopicSelectionState(), new SelectTopicCommand()); 
                case var t when t.state is UnknownUserState:
                    return (new TopicSelectionState(), new SelectTopicCommand());
//                case var t when t.state is WelcomeState welcomeState:
//                    return ProcessWelcomeState(welcomeState, t.transition);
                case var t when t.state is TopicSelectionState topicSelectionState:
                    return ProcessTopicSelectionState(topicSelectionState, t.transition);
                case var t when t.state is LevelSelectionState levelSelectionState:
                    return ProcessLevelSelectionState(levelSelectionState, t.transition);
                case var t when t.state is TaskState taskState:
                    return ProcessTaskState(taskState, t.transition);
            }

            return default;
        }

        private static (State, ICommand) ProcessTaskState(TaskState state, Transition transition)
        {
            switch (transition)
            {
                case BackTransition _:
                    return (new LevelSelectionState(state.TopicId), new SelectLevelCommand(state.TopicId));
                case NextTaskTransition _:
                    return (state, new NextTaskCommand());
                case ShowHintTransition _:
                    return (state, new ShowHintCommand());
                case CorrectTransition correctTransition:
                    return (state, new CheckTaskCommand(correctTransition.Content));
            }

            return (new TopicSelectionState(), new SelectTopicCommand());
        }

        private static (State, ICommand) ProcessLevelSelectionState(LevelSelectionState state, Transition transition)
        {
            switch (transition)
            {
                case BackTransition _:
                    return (new TopicSelectionState(), new SelectTopicCommand());
                case CorrectTransition correctTransition:
                    return
                        (new TaskState(state.TopicId, correctTransition.Content),
                            new ShowTaskCommand(state.TopicId, correctTransition.Content));
            }

            return (new TopicSelectionState(), new SelectTopicCommand());
        }

        private static (State, ICommand) ProcessTopicSelectionState(TopicSelectionState state, Transition transition)
        {
            switch (transition)
            {
                case BackTransition _:
                    return (new TopicSelectionState(), new SelectTopicCommand());
                case CorrectTransition correctTransition:
                    return (new LevelSelectionState(correctTransition.Content),
                        new SelectLevelCommand(correctTransition.Content));
            }
            return (new TopicSelectionState(), new SelectTopicCommand());
        }

//        private static (State, ICommand) ProcessWelcomeState(WelcomeState state, Transition transition)
//        {
//            if (transition is CorrectTransition correctTransition)
//            {
//                switch (correctTransition.Content)
//                {
//                    case StringCallbacks.Topics:
//                        return (new TopicSelectionState(), new SelectTopicCommand());
//                    case StringCallbacks.Info:
//                        return (new WelcomeState(), new AboutCommand());
//                    case StringCallbacks.Feedback:
//                        return (new WelcomeState(), new FeedBackCommand());
//                }
//            }
//            return (new WelcomeState(), new WelcomeCommand());
//        }
    }
}