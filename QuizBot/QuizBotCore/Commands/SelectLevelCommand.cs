using System;
using System.Linq;
using System.Threading.Tasks;
using QuizBotCore.Commands;
using QuizRequestService;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuizBotCore
{
    public class SelectLevelCommand : ICommand
    {
        private string topicId;
        public SelectLevelCommand(string topicId)
        {
            this.topicId = topicId;
        }
        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService)
        {
            var chatId = chat.Id;
            var messageText = "Вижу с темой ты определился. " +
                              "Выбирай уровень:";

            var topicGuid = Guid.Parse(topicId);
            
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                quizService.GetLevels(topicGuid).Select(x => InlineKeyboardButton.WithCallbackData(x.Description, x.Id.ToString())),
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Назад", "back")
                }
            });
            
            await client.SendTextMessageAsync(chatId, messageText, replyMarkup: keyboard);
        }
    }
}