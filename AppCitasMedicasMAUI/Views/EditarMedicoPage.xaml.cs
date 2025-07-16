using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Services;
using AppCitasMedicasMAUI.ViewModels;

namespace AppCitasMedicasMAUI.Views
{
    [QueryProperty(nameof(Medico), "Medico")]
    public partial class EditarMedicoPage : ContentPage
    {
        private readonly EditarMedicoViewModel _viewModel;

        public EditarMedicoPage()
        {
            InitializeComponent();

            var medicoService = App.Current.Handler.MauiContext.Services.GetService<MedicoApiService>();
            var logService = App.Current.Handler.MauiContext.Services.GetService<LogService>();

            _viewModel = new EditarMedicoViewModel(medicoService, logService);
            BindingContext = _viewModel;
        }

        private Medico _medico;
        public Medico Medico
        {
            get => _medico;
            set
            {
                _medico = value;
                _viewModel.Inicializar(_medico);
            }
        }
    }
}
