using AppCitasMedicasMAUI.Models;

namespace AppCitasMedicasMAUI.Views;

public partial class AdminDashboardPage : ContentPage
{
    private readonly Usuario _usuario;

    public AdminDashboardPage(Usuario usuario)
    {
        InitializeComponent();
        _usuario = usuario;

    }

    private async void OnUsuariosClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UsuariosPage());
    }

    private async void OnMedicosClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MedicosPage());
    }

    private async void OnPacientesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PacientesPage());
    }

    private async void OnCitasClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CitasPage());
    }
}
