namespace QuizBotCore
{
    public abstract class Transition
    {
    }

    class BackTransition
        : Transition
    {
    }

    class CorrectTransition : Transition
    {
        public CorrectTransition(string content)
        {
            Content = content;
        }
        
        public string Content { get;  }
    }

    class InvalidTransition : Transition
    {
    }
}
