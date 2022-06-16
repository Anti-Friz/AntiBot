using Anti__Bot.Properties;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Threading.Tasks;

namespace Anti__Bot
{
    internal class ServiceFunc
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