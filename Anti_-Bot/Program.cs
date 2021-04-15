using Anti__Bot.Properties;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using GURPSLib;
using GURPSLib.Types;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Tababular;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Anti__Bot
{
    class Program
    {
        internal static ITelegramBotClient telegramBotClient = null;

        static async Task MainAsync()
        {
            // ===================   Discord   ===================
            DiscordClient discord = new DiscordClient(
                new DiscordConfiguration()
                {
                    Token = Resources.Bot_Token,
                    TokenType = TokenType.Bot
                });
            

            CommandsNextExtension commands = discord.UseCommandsNext(new CommandsNextConfiguration()
                {
                    StringPrefixes = new[] { "!", "Бот, ", "бот, ", ".", "бот ", "Бот " }
                });

            commands.RegisterCommands(Assembly.GetExecutingAssembly());

            await discord.ConnectAsync();

            // ===================   Telegram   ===================
            telegramBotClient = new TelegramBotClient(Resources.Telegram_Bot);
            Telegram.Bot.Types.User me = telegramBotClient.GetMeAsync().Result;
            telegramBotClient.OnMessage += new DiceBot().ThrowDice;
            telegramBotClient.OnInlineQuery += new DiceBot().ThrowDice;
            telegramBotClient.StartReceiving();


            await Task.Delay(-1);
        }

        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();            
        }

    }
}
