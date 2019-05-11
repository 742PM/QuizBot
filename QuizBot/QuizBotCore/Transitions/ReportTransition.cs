namespace QuizBotCore.Transitions
{
    public class ReportTransition : Transition
    {
        public readonly int MessageId;

        public ReportTransition(int messageId)
        {
            MessageId = messageId;
        }
    }
}