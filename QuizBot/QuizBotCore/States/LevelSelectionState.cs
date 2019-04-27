using MongoDB.Bson.Serialization.Attributes;

namespace QuizBotCore.States
{
    public class LevelSelectionState : State
    {
        [BsonElement]
        public string TopicId { get; }

        [BsonConstructor]
        public LevelSelectionState(string topicId)
        {
            TopicId = topicId;
        }
    }
}
