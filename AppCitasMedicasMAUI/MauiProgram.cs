using AppCitasMedicasMAUI.Data;
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

            // Registro de AppDatabase como singleton asincrónico
            builder.Services.AddSingleton(async provider =>
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "citasmedicas.db3");
                return await AppDatabase.CreateAsync(dbPath);
            });

            return builder.Build();
        }
    }
}
