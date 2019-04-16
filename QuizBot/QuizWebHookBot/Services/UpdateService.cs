using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizWebHookBot.Commands;
using QuizWebHookBot.Database;
using QuizWebHookBot.StateMachine;
using QuizWebHookBot.StateMachine.States;
using Telegram.Bot.Types;

namespace QuizWebHookBot.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IBotService botService;
        private readonly List<Command> commands;
        private readonly ILogger<UpdateService> logger;
        private readonly IUserRepository userRepository;

        public UpdateService(IBotService botService, ILogger<UpdateService> logger, IUserRepository userRepository)
        {
            this.botService = botService;
            this.logger = logger;
            this.userRepository = userRepository;
            commands = new List<Command>();
            commands.Add(new Welcome().Execute);
        }

        public Command GetUserState(Message message)
        {
            var userId = message.From.Id;
            //TODO: Get user last state from DB by chatId
            var userEntity = userRepository.FindById(userId) ??
                             userRepository.Insert(new UserEntity(Guid.NewGuid(), new GodState(), userId));
            ParseMessage p = null;
            var parsedMessage = p(message);

            var (currentState, currentCommand) = userEntity.CurrentState.GetNextState();
            userEntity.CurrentState = currentState;
            userRepository.Insert(userEntity);
            return currentCommand;
        }

        public Command RecognizeCommand(Message message) => throw new NotImplementedException();

        public async Task ExecuteCommand(Command command, Message message)
        {
            await command(message, botService.Client);
        }
    }
}
