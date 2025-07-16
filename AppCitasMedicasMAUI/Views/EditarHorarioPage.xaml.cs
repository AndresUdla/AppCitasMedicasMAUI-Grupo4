using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Services;
using AppCitasMedicasMAUI.ViewModels;

namespace AppCitasMedicasMAUI.Views
{
    [QueryProperty(nameof(Horario), "Horario")]
    public partial class EditarHorarioPage : ContentPage
    {
        private readonly EditarHorarioViewModel _viewModel;

        public EditarHorarioPage()
        {
            InitializeComponent();

            var horarioService = App.Current.Handler.MauiContext.Services.GetService<HorarioApiService>();
            var logService = App.Current.Handler.MauiContext.Services.GetService<LogService>();

            _viewModel = new EditarHorarioViewModel(horarioService, logService);
            BindingContext = _viewModel;
        }

        public Horario Horario
        {
            set
            {
                if (value != null)
                    _viewModel.InicializarConHorario(value);
            }
        }
    }
}
