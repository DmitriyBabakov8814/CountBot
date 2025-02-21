using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using Microsoft.Extensions.Hosting;
using Telegram.Bot.Polling;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using CountBot.Controllers;

namespace CountBot
{
    internal class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build(); // Собираем

            Console.WriteLine("Сервис запущен");
            // Запускаем сервис
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }
        static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<InlineKeyboardController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<MessageController>();
            // Регистрируем объект TelegramBotClient c токеном подключения
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("7880609268:AAHXty13eN-aXKUJ5X1VlOAy0G4P0LP2c2w"));
            // Регистрируем постоянно активный сервис бота
            services.AddHostedService<Bot>();
        }
    }
}