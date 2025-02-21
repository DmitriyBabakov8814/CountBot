using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace CountBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        public TextMessageController(ITelegramBotClient telegramClient)
        {
            _telegramClient = telegramClient;
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine("TextMessageController получил сообщение");
            switch (message.Text)
            {
                case "/start":
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Количество символов в строке", "count_chars"),
                        InlineKeyboardButton.WithCallbackData($" Сумма чисел", "sum_numbers")
                    });
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id,"Выберите действие:" ,cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;
                default:
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Введите команду /start для начала работы", cancellationToken: ct);
                    break;
            }
        }
    }
}
