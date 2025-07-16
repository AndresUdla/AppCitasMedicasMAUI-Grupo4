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

            // Obtener servicios e inicializar ViewModel
            var pacienteService = App.Current.Handler.MauiContext.Services.GetService<PacienteApiService>();
            var usuarioService = App.Current.Handler.MauiContext.Services.GetService<UsuarioApiService>();

            _viewModel = new PacientesViewModel(pacienteService, usuarioService);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.CargarPacientesAsync();
        }
    }
}
