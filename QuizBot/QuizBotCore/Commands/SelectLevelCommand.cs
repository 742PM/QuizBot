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
        private TopicDTO topicDto;

        public SelectLevelCommand(TopicDTO topicDto)
        {
            this.topicDto = topicDto;
        }

        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, IQuizService quizService,
            IUserRepository userRepository, ILogger logger)
        {
            var chatId = chat.Id;

            var user = userRepository.FindByTelegramId(chatId);
            var allLevels = quizService.GetLevels(topicDto.Id);
            var availableLevels = quizService.GetAvailableLevels(user.Id, topicDto.Id).ToList();
            var closedLevels = allLevels.Select(x => x.Description).Except(availableLevels.Select(x => x.Description));
            
            var activeLevels = availableLevels.Select((e,index)=> $"/level{index} {e.Description}");
            var nonActiveLevels = closedLevels.Select(x => $"{DialogMessages.ClosedLevel} {x}");
            
            var activeLevelsMessage = string.Join("\n", activeLevels);
            var nonActiveLevelsMessage = string.Join('\n', nonActiveLevels);
            
            var message = $"{DialogMessages.SelectLevelMessage}\n{activeLevelsMessage}\n{nonActiveLevelsMessage}";
            

            var keyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton
                        .WithCallbackData(ButtonNames.Back, StringCallbacks.Back)
                }
            });

            await client.SendTextMessageAsync(chatId, message, replyMarkup: keyboard);
        }
    }
}