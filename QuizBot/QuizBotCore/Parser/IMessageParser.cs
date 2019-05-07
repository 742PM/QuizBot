using QuizBotCore.States;
using QuizRequestService;
using Telegram.Bot.Types;

namespace QuizBotCore
{
    public interface IMessageParser
    {
        Transition Parse(State currentState, Update update, IQuizService quizService);
    }
}
