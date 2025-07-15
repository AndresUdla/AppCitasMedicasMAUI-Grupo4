using AppCitasMedicasMAUI.Data;
using AppCitasMedicasMAUI.Services;
using Microsoft.Extensions.Logging;
using System.Net.Http;

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

            // Registro del HttpClient para API
            builder.Services.AddHttpClient();

            // Registro de ApiServices como transient o singleton según prefieras
            builder.Services.AddTransient<AdministradorApiService>();
            builder.Services.AddTransient<UsuarioApiService>();
            builder.Services.AddTransient<MedicoApiService>();
            builder.Services.AddTransient<PacienteApiService>();
            builder.Services.AddTransient<HorarioApiService>();
            builder.Services.AddTransient<CitaApiService>();

            return builder.Build();
        }
    }
}
