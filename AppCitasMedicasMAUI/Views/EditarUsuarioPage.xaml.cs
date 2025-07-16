using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Services;
using AppCitasMedicasMAUI.ViewModels;

namespace AppCitasMedicasMAUI.Views
{
    public partial class EditarUsuarioPage : ContentPage, IQueryAttributable
    {
        private readonly EditarUsuarioViewModel _viewModel;

        public EditarUsuarioPage()
        {
            InitializeComponent();

            var usuarioService = App.Current.Handler.MauiContext.Services.GetService<UsuarioApiService>();
            var logService = App.Current.Handler.MauiContext.Services.GetService<LogService>();

            _viewModel = new EditarUsuarioViewModel(usuarioService, logService);
            BindingContext = _viewModel;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("Usuario", out var usuarioObj) && usuarioObj is Usuario usuario)
            {
                _viewModel.Inicializar(usuario);
            }
        }
    }
}
