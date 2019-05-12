using QuizBotCore.Transitions;

namespace QuizBotCore.Parser
{
    public class ReplyReportTransition : Transition
    {
        public readonly int MessageId;

        public ReplyReportTransition(int messageId)
        {
            MessageId = messageId;
        }
    }
}