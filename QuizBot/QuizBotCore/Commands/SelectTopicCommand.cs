using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
            var jsons = quizService.GetTopics().Select(x => JsonConvert.SerializeObject(x));
            logger.LogInformation(String.Join("", jsons));
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                quizService
                    .GetTopics()
                    .Select(x => 
                        InlineKeyboardButton
                            .WithCallbackData(x.Name, JsonConvert.SerializeObject(x))),
                new[]
                {
                    InlineKeyboardButton
                        .WithCallbackData(ButtonNames.Back, StringCallbacks.Back)
                }
            });
            await client.SendTextMessageAsync(chatId, DialogMessages.WelcomeMessage, replyMarkup: keyboard);
        }
    }
}