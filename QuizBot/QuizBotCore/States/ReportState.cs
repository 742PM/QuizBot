using QuizRequestService.DTO;

namespace QuizBotCore.States
{
    class ReportState : State
    {
        public readonly TopicDTO TopicDto;
        public readonly LevelDTO LevelDto;

        public ReportState(TopicDTO topicDto, LevelDTO levelDto)
        {
            this.TopicDto = topicDto;
            this.LevelDto = levelDto;
        }
    }
}