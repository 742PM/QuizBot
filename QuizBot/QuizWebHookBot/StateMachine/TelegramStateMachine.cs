using System;
using QuizWebHookBot.Commands;
using QuizWebHookBot.StateMachine.States;

namespace QuizWebHookBot.StateMachine
{
    public static class
        TelegramStateMachine //пока сделал такой класс, возможно будет лучше делать для каждого юзера вообще отдельную стейт-машину и не хранить ИД
    {
        public static (State, Command) GetNextState(this State currentState) //едиственный доступный снаружи метод
        {
            switch (currentState) //c C# 8.0 будет более красиво
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

        private static (State, Command) ProcessGodState(GodState state) => (new WelcomeState(), new Welcome().Execute);

        private static (State, Command) ProcessUnrecognizedState() => throw new NotImplementedException();

        private static (State, Command) ProcessTaskState(TaskState state) => throw new NotImplementedException();

        private static (State, Command) ProcessLevelSelectionState(LevelSelectionState state) =>
            throw new NotImplementedException();

        private static (State, Command) ProcessTopicSelectionState(TopicSelectionState state) =>
            throw new NotImplementedException();

        private static (State, Command) ProcessAdminState(AdminState state) => throw new NotImplementedException();

        private static (State, Command) ProcessAboutState(AboutState state) => throw new NotImplementedException();

        private static (State, Command) ProcessWelcomeState(WelcomeState state)
        {
            Command command = new Welcome().Execute;
            return (state, command);
        }
    }
}
