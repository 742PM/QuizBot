using System;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace QuizWebHookBot.Services
{
    public class BotService : IBotService
    {
        const string TelegramTokenVariableName = "TELEGRAM_TOKEN";

        public BotService(IOptions<BotConfiguration> config) => Client = new TelegramBotClient("869417315:AAGHi5L1wMyN7D5sLcxWm-bgsINqQbHpNh8");

        public TelegramBotClient Client { get; }
    }
}
