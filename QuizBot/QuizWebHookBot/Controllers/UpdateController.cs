using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public UpdateController(IUpdateService updateService, IBotService botService, IQuizService quizService)
        {
            this.updateService = updateService;
            this.botService = botService;
            this.quizService = quizService;
        }

        // POST api/update
        [HttpPost]
        public async Task Post([FromBody] Update update)
        {
            if (update.Type == UpdateType.CallbackQuery)
            {
                await botService.Client.SendTextMessageAsync(
                    update.Message.Chat.Id,
                    $"Received {update.CallbackQuery.Data}");
                return;
            }

            if (update.Type != UpdateType.Message)
                return;
            
            var message = update.Message;
            
            if (message.Type == MessageType.Text)
            {
                var userCommand = updateService.ProcessMessage(message);
                await userCommand.ExecuteAsync(message.Chat, botService.Client, quizService);
            }
        }
    }
}