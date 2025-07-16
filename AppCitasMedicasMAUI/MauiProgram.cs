using AppCitasMedicasMAUI.Data;
using AppCitasMedicasMAUI.Services;
using AppCitasMedicasMAUI.ViewModels;
using AppCitasMedicasMAUI.Views;
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

            // Registro de HttpClient con BaseAddress para la API
            builder.Services.AddHttpClient<UsuarioApiService>(client =>
            {
                client.BaseAddress = new Uri(AppCitasMedicasMAUI.Models.ApiConstants.BaseUrl);
            });

            builder.Services.AddHttpClient<PacienteApiService>(client =>
            {
                client.BaseAddress = new Uri(AppCitasMedicasMAUI.Models.ApiConstants.BaseUrl);
            });

            builder.Services.AddHttpClient<AdministradorApiService>(client =>
            {
                client.BaseAddress = new Uri(AppCitasMedicasMAUI.Models.ApiConstants.BaseUrl);
            });

            builder.Services.AddHttpClient<MedicoApiService>(client =>
            {
                client.BaseAddress = new Uri(AppCitasMedicasMAUI.Models.ApiConstants.BaseUrl);
            });

            builder.Services.AddHttpClient<HorarioApiService>(client =>
            {
                client.BaseAddress = new Uri(AppCitasMedicasMAUI.Models.ApiConstants.BaseUrl);
            });

            builder.Services.AddHttpClient<CitaApiService>(client =>
            {
                client.BaseAddress = new Uri(AppCitasMedicasMAUI.Models.ApiConstants.BaseUrl);
            });

            // Registro de ViewModels y Views
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<PacientesViewModel>();
            builder.Services.AddTransient<PacientesPage>();
            builder.Services.AddTransient<CrearPacientePage>();
            builder.Services.AddTransient<CrearPacienteViewModel>();
            builder.Services.AddTransient<EditarPacientePage>();
            builder.Services.AddTransient<EditarPacienteViewModel>();

            return builder.Build();
        }
    }
}
