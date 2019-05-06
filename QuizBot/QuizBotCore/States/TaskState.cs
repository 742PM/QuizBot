using MongoDB.Bson.Serialization.Attributes;
using QuizRequestService;

namespace QuizBotCore.States
{
    public class TaskState : State
    {
        [BsonConstructor]
        public TaskState(TopicDTO topicDto, LevelDTO levelDto)
        {
            TopicDTO = topicDto;
            LevelDTO = levelDto;
        }

        [BsonElement] public TopicDTO TopicDTO { get; }

        [BsonElement] public LevelDTO LevelDTO { get; }

    }
}
