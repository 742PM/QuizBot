using System;

namespace QuizWebHookBot.StateMachine
{
    public class LevelSelectionState : State
    {
        public LevelSelectionState(Guid userId) : base(userId)
        {
        }
    }
}