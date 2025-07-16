using AppCitasMedicasMAUI.Services;
using AppCitasMedicasMAUI.ViewModels;

namespace AppCitasMedicasMAUI.Views
{
    public partial class CrearUsuarioPage : ContentPage
    {
        public CrearUsuarioPage()
        {
            InitializeComponent();

            var usuarioService = App.Current.Handler.MauiContext.Services.GetService<UsuarioApiService>();
            var logService = App.Current.Handler.MauiContext.Services.GetService<LogService>();

            BindingContext = new CrearUsuarioViewModel(usuarioService, logService);
        }
    }
}
