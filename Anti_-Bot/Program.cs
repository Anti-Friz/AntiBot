using Anti__Bot.Bots;
using System.Threading.Tasks;

namespace Anti__Bot
{
    internal class Program
    {
        //internal static ITelegramBotClient telegramBotClient = null;

        private static async Task MainAsync()
        {
            #region ===================   Discord   ===================

            //https://github.com/Soyvolon/DSharpPlus.SlashCommands

            //DiscordClient discord = new DiscordClient(
            //    new DiscordConfiguration()
            //    {
            //        Token = Resources.Bot_Token,
            //        TokenType = TokenType.Bot
            //    });

            //CommandsNextExtension commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            //    {
            //        StringPrefixes = new[] { "!", "Бот, ", "бот, ", ".", "бот ", "Бот ", ">", "/" }
            //    });

            //commands.RegisterCommands(Assembly.GetExecutingAssembly());

            //await discord.ConnectAsync();

            #endregion ===================   Discord   ===================

            // ===================   Telegram   ===================
            //telegramBotClient = new TelegramBotClient(Resources.Telegram_Bot);

            TGBot telegramBotHandler = new TGBot();
            await telegramBotHandler.StartBotAsync();

            //QueuedUpdateReceiver updateReceiver = new QueuedUpdateReceiver(telegramBotClient);
            //updateReceiver.StartReceiving();

            //await foreach (Update update in updateReceiver)
            //{
            //    if (update.Message is Message message)
            //    {
            //        await telegramBotClient.SendTextMessageAsync(
            //            message.Chat,
            //            $"Still have to process {updateReceiver.PendingUpdates} updates"
            //        );
            //    }
            //}

            //Telegram.Bot.Types.User me = telegramBotClient.GetMeAsync().Result;
            //telegramBotClient.OnMessage += new DiceBot().ThrowDice;
            //telegramBotClient.OnInlineQuery += new DiceBot().ThrowDice;
            //telegramBotClient.StartReceiving();

            await Task.Delay(-1);
        }

        private static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }
    }
}