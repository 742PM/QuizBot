using MongoDB.Bson.Serialization.Attributes;
using QuizRequestService;

namespace QuizBotCore.States
{
    public class LevelSelectionState : State
    {
        [BsonElement]
        public TopicDTO TopicDto { get; }

        [BsonConstructor]
        public LevelSelectionState(TopicDTO topicDto)
        {
            TopicDto = topicDto;
        }
    }
}
