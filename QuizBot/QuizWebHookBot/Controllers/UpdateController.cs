using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace QuizWebHookBot.Controllers
{
    [Route("api/[controller]")]
    public class UpdateController : Controller
    {
        private readonly ProcessUpdate update;

        public UpdateController(ProcessUpdate update)
        {
            this.update = update;
        }

        // POST api/update
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            if (update == null || update.Type != UpdateType.Message)
                return Ok();

            var message = update.Message;
            await this.update(message);
            return Ok();
        }
    }
}
