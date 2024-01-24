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