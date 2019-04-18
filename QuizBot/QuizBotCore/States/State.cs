namespace QuizBotCore.States
{
    public abstract class State
    {

        public abstract Transition[] AvailableTransitions { get; }
    }
}
