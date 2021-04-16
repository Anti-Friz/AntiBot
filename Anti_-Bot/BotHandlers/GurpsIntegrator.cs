using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using MathNet.Numerics.Random;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.InlineQueryResults;
using GURPSLib;
using GURPSLib.Types;
using Tababular;

namespace Anti__Bot
{
    public class GurpsIntegrator : BaseCommandModule
    {
        Weapon weapon = null;
        BLDesign bLDesign = null;
        TableFormatter formatter = null;

        [Command("randomBeam")]
        [Aliases("случайноеЛучевое", "рандомноеЛучевое", "рл")]
        public async Task SendRandomBeamWeapon(CommandContext ctx)
        {
            try
            {
                bLDesign = new BLDesign();
                weapon = bLDesign.GetRandomBeamWeapon();
                formatter = new TableFormatter();
                string deffaults = string.Format("");

                var result = new[]
                {

                    new GurpsWeapon(
                        weapon.TL.TL,
                        weapon.Name,
                        weapon.Damage,
                        weapon.WeaponBaseProps.FullAcc,
                        weapon.WeaponBaseProps.Range,
                        weapon.Weight,
                        weapon.WeaponBaseProps.FullRoF,
                        weapon.WeaponBaseProps.ShotsFull,
                        weapon.WeaponBaseProps.ST,
                        weapon.WeaponBaseProps.Bulk.ToString(),
                        weapon.WeaponBaseProps.Recoil.ToString(),
                        weapon.Cost,
                        weapon.LC.ToString())
                };

                foreach (var item in weapon.WeaponBaseProps.Defaults)
                {
                    deffaults += string.Format($"{item.SkillName} ({item.Specialization})\n");
                    break;
                }

                await ctx.RespondAsync($"```\n{deffaults}{formatter.FormatObjects(result)}```");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }


    }
}
