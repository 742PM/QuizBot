using System;

namespace QuizWebHookBot.StateMachine
{
    public static class TelegramStateMachine        //пока сделал такой класс, возможно будет лучше делать для каждого юзера вообще отдельную стейт-машину и не хранить ИД
    {
        public static State GetNextState(this State currentState)    //едиственный доступный снаружи метод
        {
            switch (currentState)                 //c C# 8.0 будет более красиво
            {
                case WelcomeState welcome:
                    return ProcessWelcomeState(welcome);// я хз какое следующее состояние
                        // ... много других состояний, каждое обработать по такой же схеме
                default:
                    throw new ArgumentException("Как-то обработать неизвестное состояние");
            }
        }


        private static State ProcessWelcomeState(WelcomeState welcomeState)
        {
            throw new NotImplementedException();
        }
    }
}