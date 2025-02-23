using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace CountBot.Controllers
{
    public class InlineKeyboardController
    {
        private readonly ITelegramBotClient _telegramClient;
        private static Dictionary<long, string> _userStates = new Dictionary<long, string>();
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
            long chatId = callbackQuery.Message.Chat.Id;
            switch (callbackQuery.Data)
            {
                case "count_chars":
                    _userStates[chatId] = "count_chars";
                    await _telegramClient.SendTextMessageAsync(chatId, "Отправьте строку, чтобы посчитать количество символов.", cancellationToken: ct);
                    break;
                case "sum_numbers":
                    _userStates[chatId] = "sum_numbers";

                    await _telegramClient.SendTextMessageAsync(chatId, "Отправьте числа через пробел, чтобы получить их сумму.", cancellationToken: ct);
                    break;
            }
        }
        public static string GetUserState(long chatId)
        {
            return _userStates.ContainsKey(chatId) ? _userStates[chatId] : "";
        }
        public static void ClearUserState(long chatId)
        {
            if (_userStates.ContainsKey(chatId))
            {
                _userStates.Remove(chatId);
            }
        }
    }
}
