using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizRequestService;
using QuizWebHookBot.Services;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

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
            botService.Client.OnCallbackQuery += BotOnCallbackQueryReceived;
        }

        private async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs e)
        {
            var callbackQuery = e.CallbackQuery;

//            await botService.Client.AnswerCallbackQueryAsync(
//                callbackQuery.Id,
//                $"Received {callbackQuery.Data}");

            await botService.Client.SendTextMessageAsync(
                callbackQuery.Message.Chat.Id,
                $"Received {callbackQuery.Data}");
        }

        // POST api/update
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            if (update == null) return Ok();
            var message = update.Message;
            if (message == null) return Ok();
//            if (message.Type == MessageType.Text)
//            {
//                await botService.Client.SendTextMessageAsync(message.Chat.Id, message.Text);
//            }
            var userCommand = updateService.ProcessMessage(message);
            await userCommand.ExecuteAsync(message.Chat, botService.Client, quizService);
            return Ok();
        }
    }
}
