using System;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace QuizWebHookBot.Services
{
    public class BotService : IBotService
    {
        const string TelegramTokenVariableName = "TELEGRAM_TOKEN";

        public BotService(IOptions<BotConfiguration> config)
        {
            Client = new TelegramBotClient(Environment.GetEnvironmentVariable(TelegramTokenVariableName));
        }

        public TelegramBotClient Client { get; }
    }
}
