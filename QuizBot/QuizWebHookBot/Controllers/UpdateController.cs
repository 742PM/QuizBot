using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizRequestService;
using QuizWebHookBot.Services;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace QuizWebHookBot.Controllers
{
    [Route("api/[controller]")]
    public class UpdateController : Controller
    {
        private readonly IBotService botService;
        private readonly IUpdateService updateService;
        private readonly IQuizService quizService;
        private readonly ILogger logger;

        public UpdateController(IUpdateService updateService, IBotService botService, IQuizService quizService, 
            ILogger logger)
        {
            this.updateService = updateService;
            this.botService = botService;
            this.quizService = quizService;
            this.logger = logger;
        }

        // POST api/update
        [HttpPost]
        public async Task Post([FromBody] Update update)
        {
            logger.LogInformation("Entering POST request");
            logger.LogInformation($"Update Type: {update.Type}");
            if (update.Type == UpdateType.CallbackQuery)
            {
                logger.LogInformation("CallbackQuery processing");
                await botService.Client.SendTextMessageAsync(
                    update.Message.Chat.Id,
                    $"Received {update.CallbackQuery.Data}");
                logger.LogInformation("CallbackQuery processed");
                return;
            }

            if (update.Type != UpdateType.Message)
                return;
            
            var message = update.Message;
            
            if (message.Type == MessageType.Text)
            {
                logger.LogInformation("TextMessage processing");
                var userCommand = updateService.ProcessMessage(message);
                await userCommand.ExecuteAsync(message.Chat, botService.Client, quizService);
                logger.LogInformation("TextMessage processed");
            }
        }
    }
}