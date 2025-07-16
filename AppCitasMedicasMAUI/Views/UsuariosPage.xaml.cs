using AppCitasMedicasMAUI.Services;
using AppCitasMedicasMAUI.ViewModels;

namespace AppCitasMedicasMAUI.Views
{
    public partial class UsuariosPage : ContentPage
    {
        private UsuariosViewModel _viewModel;

        public UsuariosPage()
        {
            InitializeComponent();

            var usuarioService = App.Current.Handler.MauiContext.Services.GetService<UsuarioApiService>();
            var logService = App.Current.Handler.MauiContext.Services.GetService<LogService>();

            _viewModel = new UsuariosViewModel(usuarioService, logService);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.CargarUsuariosAsync();
        }
    }
}
