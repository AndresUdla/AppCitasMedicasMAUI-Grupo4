using AppCitasMedicasMAUI.Services;
using AppCitasMedicasMAUI.ViewModels;

namespace AppCitasMedicasMAUI.Views
{
    public partial class PacientesPage : ContentPage
    {
        private PacientesViewModel _viewModel;

        public PacientesPage()
        {
            InitializeComponent();

            // Obtener los servicios necesarios
            var context = App.Current.Handler.MauiContext.Services;

            var pacienteService = context.GetService<PacienteApiService>();
            var usuarioService = context.GetService<UsuarioApiService>();
            var logService = context.GetService<LogService>(); 

            _viewModel = new PacientesViewModel(pacienteService, usuarioService, logService);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.CargarPacientesAsync();
        }
    }
}
