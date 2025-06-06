using AppCitasMedicasMAUI.Service;
using Microsoft.Extensions.Logging;

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

            
            builder.Services.AddHttpClient("Api", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7062/"); 
            });

            builder.Services.AddSingleton<PacienteService>();

            return builder.Build();
        }
    }
}
