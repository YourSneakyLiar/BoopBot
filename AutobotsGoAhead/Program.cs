using System.Threading;
using System.IO;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

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



            if (message.Text == "Помощь")
            {
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Привет для приветствия\n" + " ПИНГ для проверки работоспособности \n"+"Пуньк для рандомного ответа\n"+ " Мем, Видосик и Ты че для мема, видео и стикера\n ",
                    cancellationToken: cancellationToken

                    );
            }


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




        

            List<string> Stikers = new List<string>
            {
                "https://raw.githubusercontent.com/YourSneakyLiar/BoopBot/main/sticker%20(1).webp",
                "https://raw.githubusercontent.com/YourSneakyLiar/BoopBot/main/sticker%20(2).webp",
                "https://raw.githubusercontent.com/YourSneakyLiar/BoopBot/main/sticker%20(3).webp",               
                "https://raw.githubusercontent.com/YourSneakyLiar/BoopBot/main/sticker%20(5).webp",
                
               
                // добавьте URL-адреса других изображений...
            };




            if (message.Text == "Ты че")
            {
                Random random = new Random();
                int index = random.Next(Stikers.Count); // Выберите случайный URL из списка

                await botClient.SendStickerAsync(
                    chatId: chatId,
                    sticker: InputFile.FromUri(Stikers[index]), // Отправьте случайный стикер в ответ
                    cancellationToken: cancellationToken
                );
            }


         
             


            if (message.Text == "Видосик")
            {
               

                await botClient.SendVideoAsync(
                 chatId: chatId,
                 video: InputFile.FromUri("https://rr5---sn-4g5edndd.googlevideo.com/videoplayback?expire=1706117531&ei=O_WwZdmiJIPN6dsPgOiEwAo&ip=185.253.160.7&id=o-ANMLNdOGVg2H-8uJlanZLmJYH1GG-C4ad5qIgULT3kEQ&itag=18&source=youtube&requiressl=yes&xpc=EgVo2aDSNQ%3D%3D&mh=H0&mm=31%2C29&mn=sn-4g5edndd%2Csn-4g5e6nsz&ms=au%2Crdu&mv=m&mvi=5&pl=24&initcwndbps=163750&vprv=1&svpuc=1&mime=video%2Fmp4&cnr=14&ratebypass=yes&dur=10.890&lmt=1699289046021968&mt=1706095510&fvip=2&fexp=24007246&beids=24350017&c=ANDROID&txp=5318224&sparams=expire%2Cei%2Cip%2Cid%2Citag%2Csource%2Crequiressl%2Cxpc%2Cvprv%2Csvpuc%2Cmime%2Ccnr%2Cratebypass%2Cdur%2Clmt&sig=AJfQdSswRQIgXd5qH_L4e8odbu54wW1BdVikHZ0GY6TFxNKkEUDMjjMCIQCaUoRAbbKwbRAShNYenGKBK3_H14JsZwQLoL6izf1lmQ%3D%3D&lsparams=mh%2Cmm%2Cmn%2Cms%2Cmv%2Cmvi%2Cpl%2Cinitcwndbps&lsig=AAO5W4owRQIhAOhJw-2xe0wf8bdRzquiOcDZuoGEpodGDgtO5XMuivUAAiAJORd97X90v7jGTH_oN9dVlKvMzQxwkQQ1odOKZ-pa7A%3D%3D&title=%D0%9B%D1%8F%20%D0%BB%D1%8F%20%D0%BB%D1%8F%20%D0%B1%D0%B0%D0%B1%D1%83%D1%88%D0%BA%D0%B0%20%F0%9F%91%B5"),              
                 supportsStreaming: true,
                 cancellationToken: cancellationToken
                );
            }






            if (message.Text == "ПОНГ?")
            {
                var keyboard = new InlineKeyboardMarkup(new[]
                {
        new [] // ряд кнопок
        {
            InlineKeyboardButton.WithCallbackData("ДА ПИНГ", "callback_data_ping"),
            InlineKeyboardButton.WithCallbackData("ДА ПОНГ", "callback_data_pong"),
            InlineKeyboardButton.WithCallbackData("ДА ПОНЬК", "callback_data_ponyk"),
        }
    });

                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "ПОНГ",
                    replyMarkup: keyboard, // Добавьте кнопки к сообщению
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