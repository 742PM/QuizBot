using System;
using System.Diagnostics;
using QuizWebHookBot.Commands;
using QuizWebHookBot.StateMachine.States;

namespace QuizWebHookBot.StateMachine
{
    public static class TelegramStateMachine        //пока сделал такой класс, возможно будет лучше делать для каждого юзера вообще отдельную стейт-машину и не хранить ИД
    {
        public static (State, ICommand) GetNextState(this State currentState)    //едиственный доступный снаружи метод
        {
            switch (currentState)                 //c C# 8.0 будет более красиво
            {
                case GodState state:
                    return ProcessGodState(state);
                case WelcomeState state:
                    return ProcessWelcomeState(state);
                case AboutState state:
                    return ProcessAboutState(state);
                case AdminState state:
                    return ProcessAdminState(state);
                case TopicSelectionState state:
                    return ProcessTopicSelectionState(state);
                case LevelSelectionState state:
                    return ProcessLevelSelectionState(state);
                case TaskState state:
                    return ProcessTaskState(state);
                default:
                    return ProcessUnrecognizedState();
            }
        }

        private static (State, ICommand) ProcessGodState(GodState state)
        {
            return (new WelcomeState(), new Welcome());
        }

        private static (State, ICommand) ProcessUnrecognizedState()
        {
            throw new NotImplementedException();
        }

        private static (State, ICommand) ProcessTaskState(TaskState state)
        {
            throw new NotImplementedException();
        }

        private static (State, ICommand) ProcessLevelSelectionState(LevelSelectionState state)
        {
            throw new NotImplementedException();
        }

        private static (State, ICommand) ProcessTopicSelectionState(TopicSelectionState state)
        {
            throw new NotImplementedException();
        }

        private static (State, ICommand) ProcessAdminState(AdminState state)
        {
            throw new NotImplementedException();
        }

        private static (State, ICommand) ProcessAboutState(AboutState state)
        {
            throw new NotImplementedException();
        }

        private static (State, ICommand) ProcessWelcomeState(WelcomeState state)
        {
            var command = new Welcome();
            return (state, command);
        }
        
        

    }
}