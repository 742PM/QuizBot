using System;
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
    public class SelectLevelCommand : ICommand
    {
        private string topicId;

        public SelectLevelCommand(string topicId)
        {
            this.topicId = topicId;
        }

        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService,
            IUserRepository userRepository, ILogger logger)
        {
            var chatId = chat.Id;

            var user = userRepository.FindByTelegramId(chatId);
            var topicGuid = Guid.Parse(topicId);

            var keyboard = new InlineKeyboardMarkup(new[]
            {
                quizService
                    .GetAvailableLevels(user.Id, topicGuid)
                    .Select(x => 
                        InlineKeyboardButton
                            .WithCallbackData(x.Description, x.Id.ToString())),
                new[]
                {
                    InlineKeyboardButton
                        .WithCallbackData(ButtonNames.Back, StringCallbacks.Back)
                }
            });

            await client.SendTextMessageAsync(chatId, DialogMessages.SelectLevelMessage, replyMarkup: keyboard);
        }
    }
}