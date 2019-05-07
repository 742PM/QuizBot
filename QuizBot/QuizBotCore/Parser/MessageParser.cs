using System.Linq;
using QuizBotCore.States;
using QuizRequestService;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace QuizBotCore
{
    public class MessageParser : IMessageParser
    {
        public Transition Parse(State currentState, Update update, IQuizService quizService)
        {
            if (update.Type == UpdateType.Message)
                switch (update.Message.Text)
                {
                    case UserCommands.Help:
                        return new HelpTransition();
                    case UserCommands.Feedback:
                        return new FeedbackTransition();
                }
            
            switch (currentState)
            {
                case UnknownUserState _:
                    return UnknownUserStateParser(update);
                case TopicSelectionState _:
                    return TopicSelectionStateParser(update);
                case LevelSelectionState state:
                    return LevelSelectionStateParser(state, update, quizService);
                case TaskState _:
                    return TaskStateParser(update);
            }
            return new InvalidTransition();
        }

        private Transition TaskStateParser(Update update)
        {
            if (update.Type == UpdateType.CallbackQuery)
            {
                var callbackData = update.CallbackQuery.Data;
                switch (callbackData)
                {
                    case StringCallbacks.Back:
                        return new BackTransition();
                    case StringCallbacks.Hint:
                        return new ShowHintTransition();
                    default:
                        return new CorrectTransition(callbackData);
                }
            }
            return new InvalidTransition();
        }

        private Transition LevelSelectionStateParser(LevelSelectionState state, Update update, IQuizService quizService)
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                {
                    var callbackData = update.CallbackQuery.Data;
                    if (callbackData == StringCallbacks.Back)
                        return new BackTransition();
                    return new CorrectTransition(callbackData);
                }

                case UpdateType.Message:
                {
                    var message = update.Message.Text;
                    if (message.Contains(UserCommands.Level))
                    {
                        var levelId = message.Replace(UserCommands.Level, "");
                        var index = int.Parse(levelId);
                        var level = quizService.GetLevels(state.TopicDto.Id).ElementAt(index);
                        return new CorrectTransition(level.Id.ToString());
                    }

                    break;
                }
            }
            return new InvalidTransition();
        }

        private Transition TopicSelectionStateParser(Update update)
        {
            if (update.Type == UpdateType.CallbackQuery)
            {
                var callbackData = update.CallbackQuery.Data;
                if (callbackData == StringCallbacks.Back)
                    return new BackTransition();
                return new CorrectTransition(callbackData);
            }
            
            return new InvalidTransition();
        }

        private Transition UnknownUserStateParser(Update update)
        {
            return new BackTransition();
        }
    }
}
