using QuizBotCore.States;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace QuizBotCore
{
    public class MessageParser : IMessageParser
    {
        /// <inheritdoc />
        public Transition Parse(State currentState, Update update)
        {
            if (update.Type == UpdateType.Message)
                if (update.Message.Text == UserCommands.Help)
                    return new HelpTransition();
            
            switch (currentState)
            {
                case UnknownUserState _:
                    return UnknownUserStateParser(update);
//                case WelcomeState _:
//                    return WelcomeStateParser(update);
                case TopicSelectionState _:
                    return TopicSelectionStateParser(update);
                case LevelSelectionState _:
                    return LevelSelectionStateParser(update);
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
                    case StringCallbacks.NextTask:
                        return new NextTaskTransition();
                    case StringCallbacks.Hint:
                        return new ShowHintTransition();
                    default:
                        return new CorrectTransition(callbackData);
                }
            }
            return new InvalidTransition();
        }

        private Transition LevelSelectionStateParser(Update update)
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

//        private Transition WelcomeStateParser(Update update)
//        {
//            if (update.Type == UpdateType.CallbackQuery)
//            {
//                var callbackData = update.CallbackQuery.Data;
//                switch (callbackData)
//                {
//                    case StringCallbacks.Topics:
//                        return new CorrectTransition(StringCallbacks.Topics);
//                    case StringCallbacks.Info:
//                        return new CorrectTransition(StringCallbacks.Info);
//                    case StringCallbacks.Feedback:
//                        return new CorrectTransition(StringCallbacks.Feedback);
//                }
//            }
//            return new InvalidTransition();
//        }

        private Transition UnknownUserStateParser(Update update)
        {
            return new BackTransition();
        }
    }
}
