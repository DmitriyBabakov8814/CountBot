using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;

namespace CountBot.Controllers
{
    public class InlineKeyboardController
    {
        private readonly ITelegramBotClient _telegramClient;
        public InlineKeyboardController(ITelegramBotClient telegramClient)
        {
            _telegramClient = telegramClient;

        }
        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
            {
                return;
            }
            string responseText = callbackQuery.Data switch
            {
                "count_chars" => "Введите текст, и я подсчитаю количество символов!",
                "sum_numbers" => "Введите числа через пробел, и я посчитаю их сумму!",
                _ => "Неизвестная команда!"
            };
            await _telegramClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,text: responseText,cancellationToken: ct);
        }
    }
}
