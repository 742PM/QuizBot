using System;
using Microsoft.Extensions.Logging;
using QuizBotCore;
using QuizBotCore.Commands;
using QuizBotCore.Database;
using QuizBotCore.States;
using Telegram.Bot.Types;

namespace QuizWebHookBot.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly ILogger<UpdateService> logger;
        private readonly IMessageParser parser;
        private readonly IStateMachine<ICommand> stateMachine;
        private readonly IUserRepository userRepository;

        public UpdateService(
            ILogger<UpdateService> logger,
            IUserRepository userRepository,
            IMessageParser parser,
            IStateMachine<ICommand> stateMachine)
        {
            this.logger = logger;
            this.userRepository = userRepository;
            this.parser = parser;
            this.stateMachine = stateMachine;
        }

        public ICommand ProcessMessage(Message message)
        {
            var userId = message.From.Id;
            var userEntity = userRepository.FindById(userId) ??
                             userRepository.Insert(new UserEntity(Guid.NewGuid(), new UnknownState(), userId));

            var state = userEntity.CurrentState;

            var transition = parser.Parse(state, message);

            var (currentState, currentCommand) = stateMachine.GetNextState(state, transition);

            userRepository.Insert(new UserEntity(userEntity.ServiceId, currentState, userId));

            return currentCommand;
        }
    }
}
