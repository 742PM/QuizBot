using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace QuizWebHookBot.Services
{
    public class Echo : ICommand
    {
        public string Command => throw new System.NotImplementedException();

        public async Task Execute(Message message, TelegramBotClient client)
        {
            if (message.Type != (MessageType) UpdateType.Message)
            {
                return;
            }

            if (message.Type == MessageType.Text)
            {
                // Echo each Message
                await client.SendTextMessageAsync(message.Chat.Id, message.Text);
            }
            else if (message.Type == MessageType.Photo)
            {
                // Download Photo
                var fileId = message.Photo.LastOrDefault()?.FileId;
                var file = await client.GetFileAsync(fileId);

                var filename = file.FileId + "." + file.FilePath.Split('.').Last();

                using (var saveImageStream = System.IO.File.Open(filename, FileMode.Create))
                {
                    await client.DownloadFileAsync(file.FilePath, saveImageStream);
                }

                await client.SendTextMessageAsync(message.Chat.Id, "Thx for the Pics");
            }
        }

        public bool Contains(Message message)
        {
            throw new System.NotImplementedException();
        }
    }
}