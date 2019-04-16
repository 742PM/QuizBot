using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizWebHookBot.Commands;
using QuizWebHookBot.StateMachine;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace QuizWebHookBot.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IBotService _botService;
        private readonly ILogger<UpdateService> _logger;
        private readonly List<ICommand> commands;

        public UpdateService(IBotService botService, ILogger<UpdateService> logger)
        {
            _botService = botService;
            _logger = logger;
            commands = new List<ICommand>();
            commands.Add(new Welcome());
        }

        public ICommand RecognizeCommand(Message message)
        {
            foreach (var command in commands)
            {
                if (command.Contains(message))
                {
                    return command;
                }
            }
            return new Echo();
        }

        public State GetUserState(Message message)
        {
            var chatId = message.Chat.Id;
            //TODO: Get user last state from DB by chatId
            var userId = Guid.NewGuid();
            var command = new Welcome();
            var userState = new WelcomeState(userId, command, message, _botService.Client);
            return userState;
        }
        
        public async Task ExecuteCommand(ICommand command, Message message)
        {
            await command.Execute(message, _botService.Client);
        }
       
    }
}
