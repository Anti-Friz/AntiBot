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
        public async Task BotHelp(CommandContext ctx, string concreteHelp)
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

        [Command("translate")]
        public async Task SendTranslate(CommandContext ctx, string translateOptions)
        {
            try
            {
                //1. все переводы в спойлере.
                //2. Все переводы из вики в спойлере.
                //3. Поиск по переводам.
                //3.1. Выдача названий.
                //3.2. Выдача названий и ссылок. 
                //4. Поиск конкретного перевода.
                //4.1. Выдача полного названия и ссылки.
                string options;

                if (translateOptions != null)
                {
                    options = translateOptions.ToLower().Trim(',', '.');


                    await ctx.RespondAsync(options);

                }




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