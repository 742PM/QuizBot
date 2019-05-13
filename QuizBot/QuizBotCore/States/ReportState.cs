using MongoDB.Bson.Serialization.Attributes;
using QuizRequestService.DTO;

namespace QuizBotCore.States
{
    class ReportState : State
    {
        [BsonElement]
        public readonly TopicDTO TopicDto;
        
        [BsonElement]
        public readonly LevelDTO LevelDto;
        
        [BsonConstructor]
        public ReportState(TopicDTO topicDto, LevelDTO levelDto)
        {
            TopicDto = topicDto;
            LevelDto = levelDto;
        }
    }
}