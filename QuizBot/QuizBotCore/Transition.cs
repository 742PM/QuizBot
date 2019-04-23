using QuizBotCore.Database;

namespace QuizBotCore
{
    public abstract class Transition
    {
    }

    class BackTransition
        : Transition
    {
    }

    class InvalidTransition : Transition
    {
    }
}
