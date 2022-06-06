using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Bot_Feodot;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Text.Encodings.Web;
using System.Text.Unicode;


var botClient = new TelegramBotClient("5250704485:AAGO-A4Lt8BTdkFxwjrCIAUgu-mNkVtOscg");




using var cts = new CancellationTokenSource();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = { } // receive all update types
};
botClient.StartReceiving(
    HandleUpdateAsync,
    HandleErrorAsync,
    receiverOptions,
    cancellationToken: cts.Token);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

// Send cancellation request to stop bot
cts.Cancel();




async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    Random rnd = new();
    
    // Only process Message updates: https://core.telegram.org/bots/api#message
    if (update.Type != UpdateType.Message)
        return;
    if (update.Message!.Type != MessageType.Text)
        return;

    // Only process text messages

    var chatId = update.Message.Chat.Id;
    if (chatId == -1001354844390)
    {
        return;
    }
    var messageText = update.Message.Text?.Split('@')[0];
    string[]? messageWords = messageText?.Split(' ');
    var reply = update.Message.ReplyToMessage?.From;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}."); 
    
    await Shariy(false);

    
    async Task Sticker()
    {
        /*StreamReader sr = new(@"C:\Users\38050\RiderProjects\Bot_Feodot\Bot_Feodot\marx.txt");
        var quotes = sr.ReadToEnd().Split('\n');        */

        string fileName = @"C:\Users\38050\RiderProjects\Bot_Feodot\Bot_Feodot\result.json";
        string jsonString = System.IO.File.ReadAllText(fileName);
        JsonSerializerOptions? options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        };
        Messages? feoMessages = 
            JsonSerializer.Deserialize<Messages>(jsonString, options);
        List<CustomMessage> feoMes = new List<CustomMessage>();
        foreach (var mes in feoMessages?.messages!)
        {
            string? mesType = mes.media_type;
            if (mes.from_id == "user5021024226" && mesType == "sticker" && mes.file.Contains("webp"))
            {
                feoMes.Add(mes);
            }
        }
        var mesToSend = feoMes[rnd.Next(0, feoMes.Count)];
        if (mesToSend.media_type == "sticker")
        {
            string path = @"https://raw.githubusercontent.com/PavelKuzennyi/BotF/main/stickers/" + 
                          mesToSend.file.Split('/')[1].Replace(" ", "%20");
            Console.WriteLine(path);
            Message message = await botClient.SendStickerAsync(
                chatId: chatId,
                sticker: path,
                cancellationToken: cancellationToken);
        }
    }

    async Task FeoMes()
    {
        /*StreamReader sr = new(@"C:\Users\38050\RiderProjects\Bot_Feodot\Bot_Feodot\marx.txt");
        var quotes = sr.ReadToEnd().Split('\n');        */

        string fileName = @"C:\Users\38050\RiderProjects\Bot_Feodot\Bot_Feodot\result.json";
        string jsonString = System.IO.File.ReadAllText(fileName);
        JsonSerializerOptions? options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        };
        Messages? feoMessages = 
            JsonSerializer.Deserialize<Messages>(jsonString, options);
        List<CustomMessage> feoMes = new List<CustomMessage>();
        foreach (var mes in feoMessages?.messages!)
        {
            string? thisMes = mes.text.ToString();
            if (mes.from_id == "user5021024226" && thisMes != "") 
            {
                feoMes.Add(mes);
            }
            string? mesType = mes.media_type;
            if (mes.from_id == "user5021024226" && mesType == "sticker" && mes.file.Contains("webp"))
            {
                feoMes.Add(mes);
            }
        }
        var mesToSend = feoMes[rnd.Next(0, feoMes.Count)];
        if (mesToSend.media_type == "sticker")
        {
            string path = @"https://raw.githubusercontent.com/PavelKuzennyi/Bot_Feodot/master/Bot_Feodot/stickers/" + 
                          mesToSend.file.Split('/')[1].Replace(" ", "%20");
            Console.WriteLine(path);
            Message message = await botClient.SendStickerAsync(
                chatId: chatId,
                sticker: path,
                cancellationToken: cancellationToken);
        }
        else
        {
            string? quote = mesToSend.text.ToString();
            Message message = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: quote,
                cancellationToken: cancellationToken);
        }
    }
    
    async Task Shiza()
    {
        int voiceNumber = rnd.Next(1, 13);
        await using (var stream = System.IO.File.
                         OpenRead($@"C:\Users\38050\RiderProjects\Bot_Feodot\Bot_Feodot\shiz\{voiceNumber}.ogg")) 
        {
            Message message = await botClient.SendVoiceAsync(
                chatId: chatId,
                voice: stream,
                cancellationToken: cancellationToken);
        }
    }

    async Task Voice()
    {
        int voiceNumber = rnd.Next(1, 11);
        Message message;
        await using (var stream = System.IO.File.
            OpenRead($@"C:\Users\38050\RiderProjects\Bot_Feodot\Bot_Feodot\{voiceNumber}.ogg")) 
        {
            message = await botClient.SendVoiceAsync(
                chatId: chatId,
                voice: stream,
                cancellationToken: cancellationToken);
        }
    }
    
    async Task Quote()
    {
        /*StreamReader sr = new(@"C:\Users\38050\RiderProjects\Bot_Feodot\Bot_Feodot\marx.txt");
        var quotes = sr.ReadToEnd().Split('\n');        */
        
        var quotes = DownLoadHtml.HtmlRead(rnd.Next(1, 231));
        var quote = quotes[rnd.Next(0, quotes.Count)];
        Message message = await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: quote,
            cancellationToken: cancellationToken);
    }
    
    async Task Shariy(bool condition)
    {
        var links = DownLoadHtml.HtmlRead("https://www.youtube.com/c/STERNENKO/videos");
        var firstLink = "https://www.youtube.com/"+links[1];
        StreamReader sr = new(@"C:\Users\38050\RiderProjects\Bot_Feodot\Bot_Feodot\shariy.txt");
        var shariy = sr.ReadToEnd().Split("\n");
        sr.Close();
        if(condition)
        {
            var link = "https://www.youtube.com/"+links[rnd.Next(1, links.Count)];
            Message message = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: link,
                cancellationToken: cancellationToken);
        }
        else if (!shariy.Contains(firstLink))
        {
            await using (StreamWriter sw = new(
                             @"C:\Users\38050\RiderProjects\Bot_Feodot\Bot_Feodot\shariy.txt",
                             false, System.Text.Encoding.Default))
            {
                await sw.WriteLineAsync(firstLink+"\n");
                sw.Close();
            }
            Message message = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: firstLink,
                cancellationToken: cancellationToken);
        }
    }


    async Task Frequency()
    {
        if (messageWords[^1].All(char.IsDigit))
        {
            if (messageWords?[^1].Length < 10)
            {
                int randomScale = int.Parse(messageWords?[^1] ?? "10");
                await using (StreamWriter sw = new(
                    @"C:\Users\38050\RiderProjects\Bot_Feodot\Bot_Feodot\timer.txt",
                    false, System.Text.Encoding.Default))
                {
                    await sw.WriteLineAsync(randomScale.ToString());
                    sw.Close();
                }
                
                Message message = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"Хорошо, теперь буду высираться раз в {randomScale} сообщений",
                    cancellationToken: cancellationToken);
            }

            else
            {
                Message message = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"Игорь пошёл нахуй",
                    cancellationToken: cancellationToken);
            }
        }
    }

    if (messageWords != null && messageWords.Contains("Ф") 
                             && (messageWords.Contains("пизди") || messageWords.Contains("пиздите") ))
    {
        await Frequency();
    }
    
    if (messageWords != null && messageWords.Contains("Ф") 
                             && (messageWords.Contains("Маркс") || messageWords.Contains("маркс") 
                                                                || messageWords.Contains("Маркса") 
                                                                || messageWords.Contains("маркса")))
    {
        await Quote();
    }

    else switch (messageText)
    {
        case "/voice" :
            await Voice();
            break;
        case "/shiza":
            await Shiza();
            break;
        case "/cytata":
            await Quote();
            break;
        case "/sticker":
            await Sticker();
            break;
        case "/shariy":
            await Shariy(true);
            break;
        default:
            StreamReader sr = new(@"C:\Users\38050\RiderProjects\Bot_Feodot\Bot_Feodot\timer.txt");
            var frequency = int.Parse(sr.ReadToEnd());
            sr.Close();
            if (rnd.Next(0, frequency) == 0 || messageText == "Ф")
            {
                switch (rnd.Next(0, 5))
                {
                    case 0:
                        await Voice();
                        break;
                    case 1:
                        await Shiza();
                        break;
                    case 2:
                        await Quote();
                        break;
                    case 3:
                        await FeoMes();
                        break;
                    case 4:
                        await Sticker();
                        break;
                }
                break;
            }

            if (messageText.Contains('Ф') || reply?.ToString() == "@feodot_bot (5250704485)")
            {
                await FeoMes();
            }
            break;   
    }

}

Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}