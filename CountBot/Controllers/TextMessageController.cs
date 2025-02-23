using CountBot.Service; 
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace CountBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly CountStringChar _countstringchar;
        private readonly CountSum _countsum;
        public TextMessageController(ITelegramBotClient telegramClient, CountStringChar countstringchar, CountSum countsum)
        {
            _telegramClient = telegramClient;
            _countstringchar = countstringchar;
            _countsum = countsum;
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            long chatId = message.Chat.Id;
            string userState = InlineKeyboardController.GetUserState(chatId);
            if (userState == "count_chars")
            {
                int count = _countstringchar.CountChar(message.Text);
                await _telegramClient.SendTextMessageAsync(chatId, $"Количество символов: {count}", cancellationToken: ct);
                InlineKeyboardController.ClearUserState(chatId);
                return;
            }
            else if (userState == "sum_numbers")
            {

                int sum = _countsum.Counter(message.Text);
                await _telegramClient.SendTextMessageAsync(chatId, $"Сумма чисел:  {sum}", cancellationToken: ct);
                return;
            }
            switch (message.Text)
            {
                case "/start":
                    var buttons = new List<InlineKeyboardButton[]>
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Количество символов в строке", "count_chars"),
                            InlineKeyboardButton.WithCallbackData("Сумма чисел", "sum_numbers")
                        }
                    };
                    await _telegramClient.SendTextMessageAsync(chatId, "Выберите действие:", cancellationToken: ct, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;

                default:
                    await _telegramClient.SendTextMessageAsync(chatId, "Введите команду /start для начала работы", cancellationToken: ct);
                    break;
            }
        }
    }
}
