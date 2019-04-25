namespace QuizBotCore.States
{
    public class LevelSelectionState : State
    {
        public string TopicId { get; }

        /// <inheritdoc />
        public override Transition[] AvailableTransitions { get; }

        public LevelSelectionState(string topicId)
        {
            TopicId = topicId;
        }
    }
}
