using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MauiScientificCalculator;


public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
                fonts.AddFont("Cairo-Light.ttf", "RegularFont");
                fonts.AddFont("Cairo-ExtraLight.ttf", "LightFont");
            });


        builder.Services.AddSingleton(sp => new HttpClient()
        {
            BaseAddress = new Uri("http://77.223.107.117/")
        });

        builder.Services.AddTransient<BackendService>();

        builder.Services.AddSingleton<App>();
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddTransient<CalculatorPage>();
        builder.Services.AddTransient<CalculatorPageViewModel>();
        builder.Services.AddTransient<HistoryPage>();
        builder.Services.AddTransient<HistoryPageViewModel>();

        return builder.Build();
	}
}
