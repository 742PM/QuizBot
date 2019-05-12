using QuizBotCore.Transitions;

namespace QuizBotCore.Parser
{
    public class ReportMessageTransition : Transition
    {
        public readonly int MessageId;

        public ReportMessageTransition(int messageId)
        {
            MessageId = messageId;
        }
    }
}