using Telegram.Bot;

namespace QuizBotCore
{
    public interface IBotService
    {
        TelegramBotClient Client { get; }
    }
}