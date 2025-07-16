using AppCitasMedicasMAUI.Services;
using AppCitasMedicasMAUI.ViewModels;

namespace AppCitasMedicasMAUI.Views
{
    public partial class HorariosMedicoPage : ContentPage
    {
        private readonly HorariosMedicoViewModel _viewModel;

        public HorariosMedicoPage()
        {
            InitializeComponent();

            var horarioService = App.Current.Handler.MauiContext.Services.GetService<HorarioApiService>();
            var medicoService = App.Current.Handler.MauiContext.Services.GetService<MedicoApiService>();
            var logService = App.Current.Handler.MauiContext.Services.GetService<LogService>();

            _viewModel = new HorariosMedicoViewModel(horarioService, medicoService, logService);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.InicializarAsync();
        }
    }
}
