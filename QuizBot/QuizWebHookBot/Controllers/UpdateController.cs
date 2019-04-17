using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizWebHookBot.Services;
using Telegram.Bot.Types;

namespace QuizWebHookBot.Controllers
{
    [Route("api/[controller]")]
    public class UpdateController : Controller
    {
        private readonly IBotService botService;
        private readonly IUpdateService updateService;

        public UpdateController(IUpdateService updateService, IBotService botService)
        {
            this.updateService = updateService;
            this.botService = botService;
        }

        // POST api/update
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            if (update == null) return Ok();
            var message = update.Message;
            var userCommand = updateService.ProcessMessage(message);
            await userCommand.ExecuteAsync(message.Chat, botService);
            return Ok();
        }
    }
}
