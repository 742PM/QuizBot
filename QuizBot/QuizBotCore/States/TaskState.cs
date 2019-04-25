using MongoDB.Bson.Serialization.Attributes;

namespace QuizBotCore.States
{
    public class TaskState : State
    {
        [BsonConstructor]
        public TaskState(string topicId, string levelId)
        {
            TopicId = topicId;
            LevelId = levelId;
        }

        [BsonElement] public string TopicId { get; }

        [BsonElement] public string LevelId { get; }

        /// <inheritdoc />
        public override Transition[] AvailableTransitions { get; }
    }
}
