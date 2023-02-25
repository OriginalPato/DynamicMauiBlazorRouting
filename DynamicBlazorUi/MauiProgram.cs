using DynamicBlazor.Services;
using Microsoft.Extensions.Logging;
using DynamicBlazorUi.Data;
using DynamicBlazorUi.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicBlazorUi;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<IRemoteDependencyResolver, RemoteDependencyResolver>();
		builder.Services.AddSingleton<TestService>();
		builder.Services.AddSingleton<IUiAssemblyService, UiAssemblyService>();
		builder.Services.AddSingleton<WeatherForecastService>();

		return builder.Build();
	}
}

