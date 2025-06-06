using AppCitasMedicasMAUI.Service;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using AppCitasMedicasMAUI.Services;

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

            builder.Services.AddHttpClient<PacienteService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7062/"); 
            });

            return builder.Build();
        }
    }

}
