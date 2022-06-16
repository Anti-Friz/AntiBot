using Anti__Bot.Properties;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;

namespace Anti__Bot.Bots
{
    internal class TGBot
    {
        internal static ITelegramBotClient telegramBotClient = null;

        #region startBot

        public async Task StartBotAsync()
        {
            telegramBotClient = new TelegramBotClient(Resources.Telegram_Bot);

            using var cts = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            telegramBotClient.StartReceiving
                (
                    updateHandler: HandleUpdateAsync,
                    pollingErrorHandler: HandlePollingErrorAsync,
                    receiverOptions: receiverOptions,
                    cancellationToken: cts.Token
                );

            var me = await telegramBotClient.GetMeAsync();

            Console.WriteLine($"{DateTime.UtcNow}: Telegram API Bot Started.");
        }

        #endregion startBot

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => BotOnMessageReceived(botClient, update.Message!),
                UpdateType.CallbackQuery => BotOnCallbackQueryReceived(botClient, update.CallbackQuery!),
                UpdateType.InlineQuery => BotOnInlineQueryReceived(botClient, update.InlineQuery!),
                _ => UnknownUpdateHandlerAsync(botClient, update)
            };
        }

        #region handlers

        private static async Task BotOnMessageReceived(ITelegramBotClient botClient, Message message)
        {
            Console.WriteLine($"{DateTime.UtcNow}: Telegram API Received message type: {message.Text}");

            await botClient.SendTextMessageAsync(message.Chat.Id, $"{message.From.FirstName}, за вами вже виїхали");
        }

        private static async Task BotOnCallbackQueryReceived(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            await botClient.AnswerCallbackQueryAsync(
                callbackQueryId: callbackQuery.Id,
                text: $"Received {callbackQuery.Data}");

            await botClient.SendTextMessageAsync(
                chatId: callbackQuery.Message!.Chat.Id,
                text: $"Received {callbackQuery.Data}");
        }

        private static async Task BotOnInlineQueryReceived(ITelegramBotClient botClient, InlineQuery inlineQuery)
        {
            Console.WriteLine($"{DateTime.UtcNow}: Telegram API Received inline query from: {inlineQuery.From.Id} ||" +
                $" {inlineQuery.Query}");

            List<InlineQueryResult> results = new List<InlineQueryResult>();

            results = CompileAnswers(inlineQuery.Query);
            string stickerId = new DiceBot().ThrowDramaDice();

            results.Add(new InlineQueryResultCachedDocument(
                    id: "1005",
                    documentFileId: stickerId,
                    title: "DRAMA!"
                ));

            await botClient.AnswerInlineQueryAsync(inlineQueryId: inlineQuery.Id,
                                                   results: results,
                                                   isPersonal: true,
                                                   cacheTime: 0);

        }

        private static Task UnknownUpdateHandlerAsync(ITelegramBotClient botClient, Update update)
        {
            Console.WriteLine($"Unknown update type: {update.Type}");
            return Task.CompletedTask;
        }

        #endregion handlers

        #region helpers

        private static List<InlineQueryResult> CompileAnswers(string query)
        {
            List<InlineQueryResult> results = new List<InlineQueryResult>();
            int ids = 1;

            for (int i = 1; i <= 3; i++)
            {
                results.Add(new InlineQueryResultArticle(
                        id: ids.ToString(),
                        title: $"{i}d10",
                        inputMessageContent: new InputTextMessageContent(
                            new DiceBot().ThrowDiceTelegram($"{i}d10"))
                        ));

                ids++;
            }
            //for (int i = 1; i <= 3; i++)
            //{
            //    results.Add(new InlineQueryResultArticle(
            //            id: ids.ToString(),
            //            title: $"Пул {i}d10",
            //            inputMessageContent: new InputTextMessageContent(
            //                new DiceBot().ThrowDiceTelegram($"{i}d10", true))
            //            ));
            //    ids++;
            //}

            switch (query)
            {
                case "3":
                case "3d":
                case "3d6":
                    results.Add(new InlineQueryResultArticle(
                        id: (ids + 1).ToString(),
                        title: "3d6",
                        inputMessageContent: new InputTextMessageContent(
                            "hello")
                        ));
                    break;

                default:
                    break;
            }

            return results;
        }

        #endregion helpers

        #region errorHandling

        private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
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

        #endregion errorHandling
    }
}