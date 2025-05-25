using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using HysConsoleTestTask.Models;
using HysConsoleTestTask.Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders(); // ❗ Отключает все логгеры, включая консоль
    })
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<HysTestTaskDbContext>(options =>
            options.UseSqlServer(connectionString)); 
        services.AddScoped<ITaskService, TaskService>();
    })
    .Build();



using var scope = host.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<HysTestTaskDbContext>();

var service = scope.ServiceProvider.GetRequiredService<ITaskService>();
Console.Clear();

while (true)
{
    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
    {
        Console.WriteLine("\nExiting...");
        break;
    }

    Console.WriteLine("\nSelect a task to execute:");
    Console.WriteLine("1 - Task 1: Customers with only TV or only DSL products");
    Console.WriteLine("2 - Task 2: Overlapping active TV products per customer");
    Console.WriteLine("3 - Task 3: Same person with two accounts (TV and DSL)");
    Console.Write("Your choice: ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            service.RunTask1();
            break;
        case "2":
            service.RunTask2();
            break;
        case "3":
            service.RunTask3();
            break;
        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}

