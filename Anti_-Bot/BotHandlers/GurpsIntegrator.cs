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
using Anti__Bot.Properties;

namespace Anti__Bot
{
    public class GurpsIntegrator : BaseCommandModule
    {
        Weapon weapon = null;
        BLDesign bLDesign = null;
        TableFormatter formatter = null;

        [Command("randomBeam")]
        [Aliases("случайноеЛучевое", "рандомноеЛучевое", "рл")]
        public async Task SendRandomBeamWeapon(CommandContext ctx, params string[] options)
        {
            try
            {
                bLDesign = new BLDesign();


                
                formatter = new TableFormatter();            

                weapon = bLDesign.GetRandomBeamWeapon();
                string deffaults = string.Format("");
                GurpsWeapon weaponResult = new GurpsWeapon(
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
                            weapon.LC.ToString());

                List<GurpsWeapon> result = new List<GurpsWeapon>();

                //if (options[0] != null)
                //{
                //    if (options[0] == "верт" || options[0] == "в" || options[0] == "vert" || options[0] == "v")
                //    {
                //        result.Add(weaponResult);
                //    }
                //}
                //else
                //{
                //    result.Add(weaponResult);
                //}
                result.Add(weaponResult);


                foreach (var item in weapon.WeaponBaseProps.Defaults)
                {
                    deffaults += string.Format($"{item.SkillName} ({item.Specialization})\n");
                    break;
                }

                await ctx.RespondAsync($"```\n{deffaults}{formatter.FormatObjects(result)}```");
            }
            catch (Exception e)
            {
                await ctx.RespondAsync($"```\nЧто-то пошло не так.```");
                Console.WriteLine(e.Message);
            }
            
        }



    }
}
