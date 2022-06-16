using Anti__Bot.Properties;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using MathNet.Numerics.Random;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Anti__Bot
{
    public class DiceBot                    //: BaseCommandModule дискордБот
    {
        private MersenneTwister rand = new MersenneTwister();
        private DiceInfo diceInfo = new DiceInfo();


        public List<int> SimpleThrowDices(int count, int dice)
        {
            List<int> dices = rand.NextInt32Sequence(1, dice + 1).Take(count).ToList();
            return dices;
        }

        public int SimpleThrowDice(int dice)
        {
            int result = SimpleThrowDices(1, dice).FirstOrDefault();
            return result;
        }


        public string ThrowDramaDice()
        {
            string d1 = "CAACAgIAAxkBAAEVFbpiq4KTcNJhDBB5rPyEHbCP1XkAARkAAvgaAAJv3VlJwM3yoIYUcnskBA";
            string d2 = "CAACAgIAAxkBAAEVFb5iq4KXOG2fvMpMbIsxZJnerUVAOgAC9hgAAkJKWEkQ1ZD3s3jovyQE";
            string d3 = "CAACAgIAAxkBAAEVFcBiq4KZfDKR-aWzioDomwroQB0FigACvhsAAuYaYUmfm6635y240SQE";
            string d4 = "CAACAgIAAxkBAAEVFcJiq4KlkpJErdyVEeyqifOJojPhTAACbRsAAgWZYUlks2DAhplOfSQE";
            string d5 = "CAACAgIAAxkBAAEVFcRiq4Kns8AEkFJrKSk6upHy-JRdywACQx4AAiI7WUmNO2GunVOJgiQE";
            string d6 = "CAACAgIAAxkBAAEVFcZiq4KpCxmIUhvGXlZsL-eeQhk0hQACeR8AAi9vWElN-AaznZsuOCQE";

            switch (SimpleThrowDice(6))
            {
                case 1:return d1;
                case 2: return d2;
                case 3: return d3;
                case 4: return d4;
                case 5: return d5;
                case 6: return d6;
                default: return d1;
            }
        }


        public DiceInfo DiceThrower(bool telegram = false, params string[] diceConfig)
        {
            try
            {
                string[] splitedDiceConfig = diceConfig[0].Split('d', 'к');

                if (splitedDiceConfig[0] == "")
                {
                    splitedDiceConfig[0] = "1";
                }

                diceInfo.Number = int.Parse(splitedDiceConfig[0]);
                diceInfo.Dice = int.Parse(splitedDiceConfig[1]);

                List<int> dices = SimpleThrowDices(diceInfo.Number, diceInfo.Dice);
                List<int> dicesSorted = new List<int>();

                if (diceInfo.Pool)
                {
                    if (telegram)
                    {
                        diceInfo.Result += string.Format($"'''\n");
                        diceInfo.Result = string.Format($"Бросаем пул {diceConfig[0]}");
                    }
                    else
                    {
                        diceInfo.Result = string.Format($"\nБросаем {diceConfig[0]}");
                    }

                    foreach (string item in diceConfig)
                    {
                        if (item.Contains('c') || item.Contains('с'))
                        {
                            diceInfo.PoolTarget = int.Parse(item.Trim('c', 'с'));
                            diceInfo.Result += string.Format($", Сложность {diceInfo.PoolTarget}");
                        }
                        if (item.Contains('н') || item.Contains('у') || item.Contains('l') || item.Contains('s'))
                        {
                            diceInfo.PoolSkillLevel = int.Parse(item.Trim('н', 'у', 'l', 's'));
                            diceInfo.Result += string.Format($", Уровень {diceInfo.PoolSkillLevel}");
                        }
                    }

                    if (telegram)
                    {
                        diceInfo.Result += string.Format($"\n");
                    }
                    else
                    {
                        diceInfo.Result += string.Format($":```diff");
                    }

                    diceInfo.Result += "\nПровалы:\n";
                    dicesSorted = (from d in dices
                                   where d == 1
                                   select d).ToList();

                    switch (diceInfo.PoolSkillLevel)
                    {
                        case 3:
                            diceInfo.FailIgoreLevel = 1;
                            break;

                        case 6:
                            diceInfo.FailIgoreLevel = 2;
                            break;

                        case 9:
                            diceInfo.FailIgoreLevel = 3;
                            break;
                    }

                    int temp = dicesSorted.Count - diceInfo.FailIgoreLevel;

                    if (temp < 0)
                    {
                        temp = 0;
                    }

                    diceInfo.Result += $"- {string.Join(", ", dicesSorted)}";
                    diceInfo.Result += "\nУспехи:\n";

                    dicesSorted = (from d in dices
                                   where d >= diceInfo.PoolTarget || d == 10
                                   select d).ToList();
                    diceInfo.Result += $"+ {string.Join(", ", dicesSorted)}";

                    temp = dicesSorted.Count - temp;

                    switch (temp)
                    {
                        case int k when (temp < 0):
                            diceInfo.Result += $"\nИтого: {Math.Abs(temp)} провал(а, ов)";
                            break;

                        case int k when (temp == 0):
                            diceInfo.Result += $"\nИтого: Без успехов или провалов.";
                            break;

                        case int k when (temp > 0):
                            diceInfo.Result += $"\nИтого: {temp} успех(а, ов)";
                            break;
                    }

                    dicesSorted = (from d in dices
                                   where d == 10
                                   select d).ToList();

                    diceInfo.Result += $"\nКол-во критов: {dicesSorted.Count()}";

                    if (telegram)
                    {
                        diceInfo.Result += "\nКубы:\n";
                    }
                    else
                    {
                        diceInfo.Result += "```";
                        diceInfo.Result += "```diff\nКубы:\n";
                    }

                    diceInfo.Result += $"{string.Join(", ", dices)}";
                }
                else
                {
                    if (telegram)
                    {
                        diceInfo.Result = string.Format($"Бросаем {diceConfig[0]}: {string.Join(", ", dices)}");
                    }
                    else
                    {
                        diceInfo.Result = string.Format($"Бросаем {diceConfig[0]}:``` {string.Join(", ", dices)}");
                    }

                    if (diceConfig.Contains("сумм") || diceConfig.Contains("сумму"))
                    {
                        diceInfo.Result += string.Format($"\nСумма: {dices.Sum()}");
                    }
                }

                if (!telegram)
                {
                    diceInfo.Result += "\n```";
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                diceInfo.Result = $"Тут ошибка: {diceConfig[0]}.";
                return diceInfo;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                diceInfo.Result = $"Что-то пошло не так.";
                return diceInfo;
            }

            return diceInfo;
        }

        public string ThrowDiceTelegram(string diceConfig, bool isPool = false)
        {
            diceInfo = new DiceInfo();
            diceInfo.Pool = isPool;
            DiceThrower(true, diceConfig);
            return $"{diceInfo.Result}";
        }

        #region ======================================   Дискорд старое  ======================================
        //[Command("roll")]
        //[Aliases("брось", "кинь")]
        //public async Task ThrowDice(CommandContext ctx, params string[] diceConfig)
        //{
        //    diceInfo = new DiceInfo();
        //    DiceThrower(false, diceConfig);
        //    await ctx.RespondAsync($"{diceInfo.Result}");
        //}
        //[Command("pool")]
        //[Aliases("пул")]
        //public async Task ThrowDicePool(CommandContext ctx, params string[] diceConfig)
        //{
        //    diceInfo = new DiceInfo();
        //    diceInfo.Pool = true;
        //    DiceThrower(false, diceConfig);
        //    await ctx.RespondAsync($"{diceInfo.Result}");
        //}
        #endregion

        #region ======================================   Телеграм старое  ======================================

        //public async void ThrowDice(object sender, MessageEventArgs e)
        //{
        //    if (e.Message.Text != null)
        //    {
        //        List<string> message = e.Message.Text.Split(" ").ToList();

        //        diceInfo = new DiceInfo();

        //        if (message[0].ToLower() == "пул")
        //        {
        //            message[0] = "пул";
        //            message.Remove("пул");
        //            diceInfo.Pool = true;
        //            DiceThrower(true, message.ToArray());
        //        }
        //        else
        //        {
        //            diceInfo.Pool = false;
        //            DiceThrower(true, message.ToArray());
        //        }

        //        await Program.telegramBotClient.SendTextMessageAsync(
        //            chatId: e.Message.Chat,
        //            text: diceInfo.Result);
        //    }

        //        //ctx.RespondAsync($"{diceInfo.Result}");
        //}
        //public async void ThrowDice(object sender, InlineQueryEventArgs e)
        //{
        //    try
        //    {
        //        if (e.InlineQuery.Query != null)
        //        {
        //            List<string> message = e.InlineQuery.Query.Split(" ").ToList();

        //            diceInfo = new DiceInfo();

        //            if (message[0].ToLower() == "пул")
        //            {
        //                message.Remove("пул");
        //                message.Remove("Пул");
        //                diceInfo.Pool = true;
        //                DiceThrower(true, message.ToArray());
        //            }
        //            else
        //            {
        //                diceInfo.Pool = false;
        //                DiceThrower(true, message.ToArray());
        //            }

        //            await Program.telegramBotClient.AnswerInlineQueryAsync
        //                (
        //                    e.InlineQuery.Id,
        //                    cacheTime: 0,
        //                    results: new List<InlineQueryResultBase>()
        //                        {
        //                            new InlineQueryResultArticle
        //                                (
        //                                    Guid.NewGuid().ToString(),
        //                                    $"Бросить {diceInfo.Number}d{diceInfo.Dice}",
        //                                    new InputTextMessageContent(diceInfo.Result)
        //                                )
        //                        }
        //                );
        //        }

        //    }
        //    catch (Exception er)
        //    {
        //        Console.WriteLine(er.Message);
        //    }

        //}

        #endregion
    }
}