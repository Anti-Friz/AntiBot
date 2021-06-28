using Anti__Bot.Properties;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.InlineQueryResults;

namespace Anti__Bot
{
    class ServiceFunc
    {

        [Command("translates")]
        [Aliases("преводы", "рандомноеЛучевое", "рл")]
        public async Task SendTranslates(CommandContext ctx)
        {
            try
            {
                await ctx.RespondAsync(Resources.Disclaimer);
            }
            catch (Exception e)
            {
                await ctx.RespondAsync($"```\nЧто-то пошло не так.```");
                Console.WriteLine(e.Message);
            }
        }

        [Command("disclaimer")]
        public async Task SendDisclaimer(CommandContext ctx)
        {
            try
            {
                await ctx.RespondAsync(Resources.Disclaimer);
            }
            catch (Exception e)
            {
                await ctx.RespondAsync($"```\nЧто-то пошло не так.```");
                Console.WriteLine(e.Message);
            }
        }
    }
}
