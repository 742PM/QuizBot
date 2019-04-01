using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizWebHookBot.Commands;
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
            commands.Add(new Start());
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

        public async Task ExecuteCommand(ICommand command, Message message)
        {
            await command.Execute(message, _botService.Client);
        }
       
    }
}
