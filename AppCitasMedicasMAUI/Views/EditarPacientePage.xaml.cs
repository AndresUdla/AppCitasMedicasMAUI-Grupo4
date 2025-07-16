using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.ViewModels;

namespace AppCitasMedicasMAUI.Views
{
    public partial class EditarPacientePage : ContentPage, IQueryAttributable
    {
        private readonly EditarPacienteViewModel _viewModel;

        public EditarPacientePage(EditarPacienteViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("Paciente", out var pacienteObj) && pacienteObj is Paciente paciente)
            {
                _viewModel.Inicializar(paciente);
            }
        }
    }
}
