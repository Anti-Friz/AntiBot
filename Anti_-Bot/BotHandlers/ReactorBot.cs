using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using MathNet.Numerics.Random;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anti__Bot
{
    public class ReactorBot
    {
        [Command("help")]
        public async Task BotHelp(CommandContext ctx, string diceConfig)
        {
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                await ctx.RespondAsync($"Что-то пошло не так.");
                throw;
            }

        }

    }
}