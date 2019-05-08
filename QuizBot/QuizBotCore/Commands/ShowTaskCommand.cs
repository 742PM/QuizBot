using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizBotCore.Database;
using QuizBotCore.ProgressBar;
using QuizRequestService;
using QuizRequestService.DTO;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuizBotCore.Commands
{
    internal class ShowTaskCommand : ICommand
    {
        private readonly TopicDTO topicDto;
        private readonly LevelDTO levelDto;
        private readonly bool isNext;

        public ShowTaskCommand(TopicDTO topicDto, LevelDTO levelDto, bool isNext = false)
        {
            this.topicDto = topicDto;
            this.levelDto = levelDto;
            this.isNext = isNext;
        }

        public async Task ExecuteAsync(Chat chat, TelegramBotClient client, ServiceManager serviceManager)
        {
            var user = serviceManager.userRepository.FindByTelegramId(chat.Id);

            var task = await GetTask(user, chat, client, serviceManager);
            if (task != null)
                await SendTask(task, chat, user, client, serviceManager.quizService, serviceManager.logger);
        }

        private async Task<TaskDTO> GetTask(UserEntity user, Chat chat, TelegramBotClient client,
            ServiceManager serviceManager)
        {
            TaskDTO task = null;
            if (isNext)
            {
                task = serviceManager.quizService.GetNextTaskInfo(user.Id);
                if (task == null)
                    await client.SendTextMessageAsync(chat.Id, DialogMessages.NextTaskNotAvailable);
            }
            else
                task = serviceManager.quizService.GetTaskInfo(user.Id, topicDto.Id, levelDto.Id);

            return task;
        }

        private async Task SendTask(TaskDTO task, Chat chat, UserEntity user, TelegramBotClient client,
            IQuizService quizService, ILogger logger)
        {
            var userProgress = quizService.GetCurrentProgress(user.Id, topicDto.Id, levelDto.Id);
            var progress = PrepareProgress(logger, userProgress);
            if (userProgress.TasksSolved == userProgress.TasksCount)
                progress = $"{progress}\n{DialogMessages.LevelSolved}";

            var question = task.Question;
            logger.LogInformation($"Question: {question}");

            var answers = task.Answers.Select((e, index) => (letter: DialogMessages.Alphabet[index], answer: $"{e}"))
                .ToList();
            var answerBlock = PrepareAnswers(answers, logger);

            var message = FormatMessage(question, progress, answerBlock);
            logger.LogInformation($"messageToSend : {message}");

            var keyboard = PrepareButtons(task, logger, answers);

            await client.SendTextMessageAsync(chat.Id, message, replyMarkup: keyboard,
                parseMode: ParseMode.Markdown);
        }

        private static string PrepareProgress(ILogger logger, ProgressDTO userProgress)
        {
            logger.LogInformation($"Progress: {userProgress.TasksSolved}:{userProgress.TasksCount}");
            var progressBar = new CircleProgressBar();
            var progress = progressBar.GenerateProgressBar(userProgress.TasksSolved, userProgress.TasksCount);
            return progress;
        }

        private static string PrepareAnswers(IEnumerable<(char letter, string answer)> answers, ILogger logger)
        {
            var answerBlock = string.Join('\n', answers.Select(x => $"{x.letter}. {x.answer}"));
            logger.LogInformation($"Answers: {answerBlock}");
            return answerBlock;
        }

        private static InlineKeyboardMarkup PrepareButtons(TaskDTO task, ILogger logger,
            IEnumerable<(char letter, string answer)> answers)
        {
            var controlButtons = new[]
            {
                InlineKeyboardButton
                    .WithCallbackData(ButtonNames.Back, StringCallbacks.Back)
            };
            logger.LogInformation($"HasHints: {task.HasHints}");
            if (task.HasHints)
                controlButtons =
                    new[]
                    {
                        InlineKeyboardButton
                            .WithCallbackData(ButtonNames.Back, StringCallbacks.Back),
                        InlineKeyboardButton
                            .WithCallbackData(ButtonNames.Hint, StringCallbacks.Hint)
                    };
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                answers.Select(x => InlineKeyboardButton
                    .WithCallbackData(x.letter.ToString(), x.answer)),
                controlButtons
            });
            return keyboard;
        }

        private string FormatMessage(string question, string progressBar, string answers)
        {
            var topicName = $"{DialogMessages.TopicName} {topicDto.Name} \n";
            var levelName = $"{DialogMessages.LevelName} {levelDto.Description} \n";
            var progress = $"{DialogMessages.Progress} {progressBar} \n";

            var questionFormatted = "```csharp\n" +
                                    $"{question}\n" +
                                    "```";
            
            var answersFormatted = "```\n" +
                                    $"{answers}\n" +
                                    "```";

            return $"{topicName}" +
                   $"{levelName}" +
                   $"{progress}\n" +
                   $"{questionFormatted}" +
                   $"{answersFormatted}";
        }
    }
}