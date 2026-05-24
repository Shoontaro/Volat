using Microsoft.Extensions.Logging;
using Volat.Data;


namespace Volat
{
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
            builder.Services.AddSingleton<DBService>();
            builder.Services.AddScoped<DataRepository>();

            var app = builder.Build();

            InitializeDatabase(app.Services)
                .GetAwaiter()
                .GetResult();

            return app;
        }

        private static async Task InitializeDatabase(
        IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var database = scope.ServiceProvider
                .GetRequiredService<DBService>();

            await database.InitializeAsync();
        }
    }
}
