using AppCitasMedicasMAUI.Service;
using AppCitasMedicasMAUI.Services;
using AppCitasMedicasMAUI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace AppCitasMedicasMAUI
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            
            builder.Services.AddHttpClient<UsuarioService>(client =>
            {
                client.BaseAddress = new Uri(ApiConstants.BaseUrl);
            });

            
            builder.Services.AddSingleton<UsuarioService>();

            
            builder.Services.AddSingleton<Services.DatabaseService>();

            return builder.Build();
        }

    }
}

