using QuizBotCore.Database;

namespace QuizBotCore
{
    public abstract class Transition
    {
        public abstract UserEntity User { get; }
    }

    class BackTransition
        : Transition
    {
        public BackTransition(UserEntity user)
        {
            User = user;
        }

        /// <inheritdoc />
        public override UserEntity User  { get; }
    }

    class InvalidTransition : Transition
    {
        public InvalidTransition(UserEntity user)
        {
            User = user;
        }
        public override UserEntity User { get; }
    }
}
