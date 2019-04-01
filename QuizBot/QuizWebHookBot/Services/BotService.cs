using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace QuizWebHookBot.Services
{
    public class BotService : IBotService
    {
        public BotService(IOptions<BotConfiguration> config)
        {
            var config1 = config.Value;
            Client = new TelegramBotClient(config1.BotToken);
        }

        public TelegramBotClient Client { get; }
    }
}
