using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizWebHookBot.Commands;
using QuizWebHookBot.Database;
using QuizWebHookBot.StateMachine;
using QuizWebHookBot.StateMachine.States;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace QuizWebHookBot.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IBotService botService;
        private readonly IUserRepository userRepository;
        private readonly ILogger<UpdateService> logger;
        private readonly List<ICommand> commands;

        public UpdateService(IBotService botService, ILogger<UpdateService> logger, IUserRepository userRepository)
        {
            this.botService = botService;
            this.logger = logger;
            this.userRepository = userRepository;
            commands = new List<ICommand>();
            commands.Add(new Welcome());
        }

        public ICommand GetUserState(Message message)
        {
            var userId = message.From.Id;
            //TODO: Get user last state from DB by chatId
            var userGuid = Guid.NewGuid();
            var userEntity = userRepository.FindById(userId) 
                             ?? userRepository.Insert(new UserEntity(userGuid, new GodState(), userId));
            var parsedMessage = MessageParser.Parse(message, userEntity.CurrentState);

            var (currentState, currentCommand) = userEntity.CurrentState.GetNextState();
            userEntity.CurrentState = currentState;
            userRepository.Insert(userEntity);
            return currentCommand;
        }

        public ICommand RecognizeCommand(Message message)
        {
            throw new NotImplementedException();
        }

        public async Task ExecuteCommand(ICommand command, Message message)
        {
            await command.Execute(message, botService.Client);
        }
       
    }
}
