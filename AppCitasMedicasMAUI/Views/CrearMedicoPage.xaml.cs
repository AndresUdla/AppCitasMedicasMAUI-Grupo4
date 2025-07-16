using AppCitasMedicasMAUI.Services;
using AppCitasMedicasMAUI.ViewModels;

namespace AppCitasMedicasMAUI.Views
{
    public partial class CrearMedicoPage : ContentPage
    {
        public CrearMedicoPage()
        {
            InitializeComponent();

            var medicoService = App.Current.Handler.MauiContext.Services.GetService<MedicoApiService>();
            var logService = App.Current.Handler.MauiContext.Services.GetService<LogService>();

            BindingContext = new CrearMedicoViewModel(medicoService, logService);
        }
    }
}
