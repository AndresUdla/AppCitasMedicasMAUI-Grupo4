using AppCitasMedicasMAUI.Services;
using AppCitasMedicasMAUI.ViewModels;

namespace AppCitasMedicasMAUI.Views
{
    public partial class MedicosPage : ContentPage
    {
        private MedicosViewModel _viewModel;

        public MedicosPage()
        {
            InitializeComponent();

            var medicoService = App.Current.Handler.MauiContext.Services.GetService<MedicoApiService>();
            var logService = App.Current.Handler.MauiContext.Services.GetService<LogService>();

            _viewModel = new MedicosViewModel(medicoService, logService);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.CargarMedicosAsync();
        }
    }
}
