using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace QuizWebHookBot.Services
{
    public class BotService : IBotService
    {
        private const string TelegramTokenVariableName = "TELEGRAM_TOKEN";

        public BotService(IOptions<BotConfiguration> config)
        {
            Client = new TelegramBotClient("840366370:AAEROBZsf6wxPg_5D8eVoF9ibJ4DyFnqiuQ");
        }
        
        public TelegramBotClient Client { get; }
    }
}
