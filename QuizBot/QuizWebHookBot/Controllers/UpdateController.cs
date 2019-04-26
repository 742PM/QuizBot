using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizBotCore.Database;
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
        private readonly ILogger<UpdateController> logger;
        private readonly IUserRepository userRepository;

        public UpdateController(IUpdateService updateService, IBotService botService, IQuizService quizService,
            ILogger<UpdateController> logger, IUserRepository userRepository)
        {
            this.updateService = updateService;
            this.botService = botService;
            this.quizService = quizService;
            this.logger = logger;
            this.userRepository = userRepository;
        }

        // POST api/update
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            Chat chat = null;
            switch (update.Type)
            {
                case UpdateType.Message:
                    chat = update.Message.Chat;
                    break;
                case UpdateType.CallbackQuery:
                    chat = update.CallbackQuery.Message.Chat;
                    await botService.Client.AnswerCallbackQueryAsync(update.CallbackQuery.Id);
                    break;
            }
            var userCommand = updateService.ProcessMessage(update);
            await userCommand.ExecuteAsync(chat, botService.Client, quizService, userRepository, logger);

            return Ok();
        }
    }
}