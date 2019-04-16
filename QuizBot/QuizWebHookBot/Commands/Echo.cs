using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using File = System.IO.File;

namespace QuizWebHookBot.Services
{
    public class Echo : ICommand
    {
        public string Command => throw new NotImplementedException();

        public async Task Execute(Message message, TelegramBotClient client)
        {
            if (message.Type != Telegram.Bot.Types.Enums.MessageType.Sticker) return;

            if (message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                // Echo each Message
                await client.SendTextMessageAsync(message.Chat.Id, message.Text);
            }
            else if (message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
            {
                // Download Photo
                var fileId = message.Photo.LastOrDefault()?.FileId;
                var file = await client.GetFileAsync(fileId);

                var filename = file.FileId + "." + file.FilePath.Split('.').Last();

                using (var saveImageStream = File.Open(filename, FileMode.Create))
                    await client.DownloadFileAsync(file.FilePath, saveImageStream);

                await client.SendTextMessageAsync(message.Chat.Id, "Thx for the Pics");
            }
        }

        public bool Contains(Message message) => throw new NotImplementedException();
    }
}
