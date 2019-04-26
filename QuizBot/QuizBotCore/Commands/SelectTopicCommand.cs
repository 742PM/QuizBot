using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizBotCore.Commands;
using QuizBotCore.Database;
using QuizRequestService;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuizBotCore
{
    internal class SelectTopicCommand : ICommand
    {
        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService,
            IUserRepository userRepository, ILogger logger)
        {
            var chatId = chat.Id;
            var messageText = "Выбирай тему и погнали!";
            
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                quizService.GetTopics().Select(x => InlineKeyboardButton.WithCallbackData(x.Name, x.Id.ToString())),
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Назад", "back")
                }
            });
            
            await client.SendTextMessageAsync(chatId, messageText, replyMarkup: keyboard);
        }
    }
}