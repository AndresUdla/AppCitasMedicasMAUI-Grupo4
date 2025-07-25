using AppCitasMedicasMAUI.Services;
using AppCitasMedicasMAUI.ViewModels;

namespace AppCitasMedicasMAUI.Views
{
    public partial class CrearMedicoPage : ContentPage
    {
        private CrearMedicoViewModel _viewModel;

        public CrearMedicoPage()
        {
            InitializeComponent();

            var medicoService = App.Current.Handler.MauiContext.Services.GetService<MedicoApiService>();
            var usuarioService = App.Current.Handler.MauiContext.Services.GetService<UsuarioApiService>();
            var logService = App.Current.Handler.MauiContext.Services.GetService<LogService>();

            _viewModel = new CrearMedicoViewModel(medicoService, usuarioService, logService);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.InicializarAsync();
        }
    }
}
