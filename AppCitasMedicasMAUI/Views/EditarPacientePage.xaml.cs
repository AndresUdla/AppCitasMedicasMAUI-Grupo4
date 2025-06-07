using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Services;

namespace AppCitasMedicasMAUI.Views;

public partial class EditarPacientePage : ContentPage
{
    private readonly PacienteService _pacienteService;
    private readonly Paciente _paciente;

    public EditarPacientePage(Paciente paciente)
    {
        InitializeComponent();
        _pacienteService = App.Current.Handler.MauiContext.Services.GetService<PacienteService>();
        _paciente = paciente;


        NombresEntry.Text = _paciente.Nombres;
        ApellidosEntry.Text = _paciente.Apellidos;
        CedulaEntry.Text = _paciente.Cedula;
        EdadEntry.Text = _paciente.Edad.ToString();
        AlturaEntry.Text = _paciente.Altura?.ToString();
        PesoEntry.Text = _paciente.Peso?.ToString();
        DireccionEntry.Text = _paciente.Direccion;
        TelefonoEntry.Text = _paciente.Telefono;
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {

        _paciente.Nombres = NombresEntry.Text;
        _paciente.Apellidos = ApellidosEntry.Text;
        _paciente.Cedula = CedulaEntry.Text;
        _paciente.Edad = int.TryParse(EdadEntry.Text, out int edad) ? edad : 0;
        _paciente.Altura = double.TryParse(AlturaEntry.Text, out double altura) ? altura : (double?)null;
        _paciente.Peso = double.TryParse(PesoEntry.Text, out double peso) ? peso : (double?)null;
        _paciente.Direccion = DireccionEntry.Text;
        _paciente.Telefono = TelefonoEntry.Text;

        var resultado = await _pacienteService.UpdatePacienteAsync(_paciente.PacienteId, _paciente);

        if (resultado)
        {
            await DisplayAlert("Éxito", "Paciente actualizado correctamente.", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "No se pudo actualizar el paciente.", "OK");
        }
    }
}
