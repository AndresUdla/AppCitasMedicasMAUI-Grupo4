using AppCitasMedicasMAUI.Services;
using AppCitasMedicasMAUI.ViewModels;

namespace AppCitasMedicasMAUI.Views
{
    public partial class CrearHorarioPage : ContentPage
    {
        private readonly CrearHorarioViewModel _viewModel;

        public CrearHorarioPage()
        {
            InitializeComponent();

            var horarioService = App.Current.Handler.MauiContext.Services.GetService<HorarioApiService>();
            var medicoService = App.Current.Handler.MauiContext.Services.GetService<MedicoApiService>();
            var logService = App.Current.Handler.MauiContext.Services.GetService<LogService>();

            _viewModel = new CrearHorarioViewModel(horarioService, medicoService, logService);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.InicializarAsync();
        }
    }
}
