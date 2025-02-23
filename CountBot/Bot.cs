using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using CountBot.Controllers;

namespace CountBot
{
    public class Bot : BackgroundService
    {
        ITelegramBotClient _telegramClient;
        TextMessageController _textmessagecontroller;
        MessageController _messagecontroller;
        InlineKeyboardController _inlinekeyboardcontrollerl;
        public Bot(ITelegramBotClient telegramClient, TextMessageController textmessagecontroller, MessageController messagecontroller, InlineKeyboardController inlinekeyboardcontrollerl)
        {
            _telegramClient = telegramClient;
            _textmessagecontroller = textmessagecontroller;
            _messagecontroller = messagecontroller;
            _inlinekeyboardcontrollerl = inlinekeyboardcontrollerl;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _telegramClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, new ReceiverOptions() { AllowedUpdates = { } }, cancellationToken: stoppingToken);
            Console.WriteLine("Бот запущен");
        }
        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if(update.Type == UpdateType.Message)
            {
                switch (update.Message!.Type)
                {
                    case MessageType.Text:
                        await _textmessagecontroller.Handle(update.Message, cancellationToken);
                        return;
                    default:
                        await _messagecontroller.Handle(update.Message, cancellationToken);
                        return;
                }
            }
            else if(update.Type == UpdateType.CallbackQuery)
            {
                await _inlinekeyboardcontrollerl.Handle(update.CallbackQuery, cancellationToken);
            }
        }
        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}