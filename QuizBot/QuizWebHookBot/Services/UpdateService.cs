using System;
using Microsoft.Extensions.Logging;
using QuizBotCore;
using QuizBotCore.Commands;
using QuizBotCore.Database;
using QuizBotCore.States;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

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

        public ICommand ProcessMessage(Update update)
        {
            long userId = -1;
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    userId = update.CallbackQuery.Message.Chat.Id;
                    break;
                case UpdateType.Message:
                    userId = update.Message.Chat.Id;
                    break;
            }

            var userEntity = userRepository.FindByTelegramId(userId) ??
                            userRepository.Insert(new UserEntity(new UnknownUserState(), userId, Guid.NewGuid()));

            var state = userEntity.CurrentState;

            var transition = parser.Parse(state, update);
            logger.LogInformation($"Parsed transition {transition}");
            logger.LogInformation($"Parsed state {state}");
            if (transition is CorrectTransition correct)
                logger.LogInformation($"State content: {correct.Content}");
            var (currentState, currentCommand) = stateMachine.GetNextState(state, transition);
            
            logger.LogInformation($"New state {currentState}");

            userRepository.Update(new UserEntity(currentState, userId, userEntity.Id));

            return currentCommand;
        }
    }
}
