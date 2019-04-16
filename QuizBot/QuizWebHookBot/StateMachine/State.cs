using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebHookBot.StateMachine
{
    public class State
    {
        public State(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
       
    }
}
