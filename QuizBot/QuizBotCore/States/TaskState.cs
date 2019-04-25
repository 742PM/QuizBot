namespace QuizBotCore.States
{
    public class TaskState : State
    {
        public readonly string TopicId;
        public readonly string LevelId;

        /// <inheritdoc />
        public override Transition[] AvailableTransitions { get; }

        public TaskState(string topicId, string levelId)
        {
            TopicId = topicId;
            LevelId = levelId;
        }
    }
}
