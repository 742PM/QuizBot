using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizWebHookBot.Services;
using QuizWebHookBot.StateMachine;
using Telegram.Bot.Types;

namespace QuizWebHookBot.Controllers
{
    [Route("api/[controller]")]
    public class UpdateController : Controller
    {
        private readonly IUpdateService updateService;

        public UpdateController(IUpdateService updateService)
        {
            this.updateService = updateService;
        }

        // POST api/update
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Update update)
        {
            State state = default;
            var nextState = state.GetNextState();
            //блаблабла чета сделать с новым состоянием
            await updateService.EchoAsync(update);
            return Ok();
        }
    }
}
