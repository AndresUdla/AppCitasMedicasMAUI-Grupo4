using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Services;
using AppCitasMedicasMAUI.ViewModels;

namespace AppCitasMedicasMAUI.Views
{
    [QueryProperty(nameof(Medico), "Medico")]
    public partial class EditarMedicoPage : ContentPage
    {
        public Medico Medico { get; set; }

        public EditarMedicoPage()
        {
            InitializeComponent();

            var medicoService = App.Current.Handler.MauiContext.Services.GetService<MedicoApiService>();
            var logService = App.Current.Handler.MauiContext.Services.GetService<LogService>();

            BindingContext = new EditarMedicoViewModel(medicoService, logService);
        }
    }
}
