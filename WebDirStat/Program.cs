using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

//построитель приложения
//за что отвечает: добавление конфигурации, добавление сервисов, настройка журналирования, общая конфигурация IHostBuilder и IWebHostBuilder
var builder = WebApplication.CreateBuilder(args);

//Создаем экземпляр WebApplication - используется для управления обработкой запроса (настройки конвейера) и маршрутов
//Реализует интерфейсы: IHost - запуск, остановка хоста, IApplicationBuilder - построение конвейера, IEndpointRouteBuilder - добавление конечных точек
var app = builder.Build();
//запуск приложения
app.Run();
//есть 3 подхода для настройки приложения:
//WebHost.CreateDefaultBuilder() начиная с ASP.NET Core 2.x
//Host.CreateDefaultBuilder() Подход по умолчанию в .NET Core 3.x и .NET 5
//WebApplication.CreateBuilder(): .NET 6
//Во всех предыдущих версиях ASP.NET Core конфигурация разделена на 2 файла.
//В .NET 6 добавлено множество изменений в C#, BCL и ASP.NET Core, и теперь всё может быть в одном файле.
