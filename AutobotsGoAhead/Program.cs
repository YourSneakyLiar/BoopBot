using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AutobotsGoAhead
{
    internal class Program
    {
        static async Task  Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var botClient = new TelegramBotClient("6354241271:AAEEjIWKJxQ97kTc5ClRusRu7zCwK_hb6qo");
            using CancellationTokenSource cts = new ();

            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
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

            cts.Cancel();
        
        }

      



        static List<string> previousMessages = new List<string>(); // Статический список для хранения сообщений
        static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Message is not { } message)
                return;


            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;

            Console.WriteLine($"Received a'{messageText}' message in chat '{chatId}'.");

          




            if (message.Text == "ПИНГ")
            {
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "ПОНГ",
                    cancellationToken: cancellationToken

                    );
            }

            if (message.Text == "Привет")
            {
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Приветики, как дела?",
                    cancellationToken: cancellationToken

                    );
            }



            previousMessages.Add(message.Text);

            if (message.Text == "Пуньк")
            {
                Random random = new Random();
                int index = random.Next(previousMessages.Count); // Выберите случайное сообщение из списка

                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: previousMessages[index], // Отправьте случайное сообщение в ответ
                    cancellationToken: cancellationToken
                );
            }



            List<string> imageUrls = new List<string>
            {
                "https://github.com/YourSneakyLiar/BoopBot/blob/main/cb9a4359-279d-4153-8f5a-35c29a38073a.jpg?raw=true",
                "https://github.com/YourSneakyLiar/BoopBot/blob/main/b5d623cf-26e7-4197-af61-5835fec35573.jpg?raw=true",
                "https://github.com/YourSneakyLiar/BoopBot/blob/main/b42d362d-d464-4e81-8135-39b99f005367.jpg?raw=true",
                "https://github.com/YourSneakyLiar/BoopBot/blob/main/8463fbb2-cb9e-49ff-b5ff-472b03080698.jpg?raw=true",
                "https://github.com/YourSneakyLiar/BoopBot/blob/main/7f396adc-b356-4bf6-b179-5afc53b1b9b6.jpg?raw=true",
                "https://github.com/YourSneakyLiar/BoopBot/blob/main/3daca018-b54d-4bc4-8195-d444276b04ca.jpg?raw=true",
                "https://github.com/YourSneakyLiar/BoopBot/blob/main/2fbc98e2-8253-4e95-b2f5-b72706021091.jpg?raw=true",
                // добавьте URL-адреса других изображений...
            };

            if (message.Text == "Мем")
            {
                Random random = new Random();
                int index = random.Next(imageUrls.Count); // Выберите случайное изображение из списка

                await botClient.SendPhotoAsync(
                    chatId: chatId,
                    photo: InputFile.FromUri(imageUrls[index]), // Отправьте случайное изображение в ответ
                    cancellationToken: cancellationToken
                );
            }

            if (message.Text == "Стикер")
            {
                var stickerSet = await botClient.GetStickerSetAsync("BoopBot");
                var stickers = stickerSet.Stickers;

                Random random = new Random();
                int index = random.Next(stickers.Length); // Выберите случайный стикер из списка

                await botClient.SendStickerAsync(
                    chatId: chatId,
                    sticker: InputFile.FromUri(stickers[index].FileId), // Отправьте случайный стикер в ответ
                    cancellationToken: cancellationToken
                );
            }


        }


        static  Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {

            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                => $"Telegram Api error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",

            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;

        }


    }
}