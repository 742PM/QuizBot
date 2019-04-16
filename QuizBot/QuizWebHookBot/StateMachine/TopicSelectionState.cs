using System;

namespace QuizWebHookBot.StateMachine
{
    public class TopicSelectionState : State
    {
        public TopicSelectionState(Guid userId) : base(userId)
        {
        }
    }
}