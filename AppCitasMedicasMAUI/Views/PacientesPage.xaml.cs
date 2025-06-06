using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Service;
using AppCitasMedicasMAUI.Services;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppCitasMedicasMAUI.Views
{
    public partial class PacientesPage : ContentPage
    {
        private readonly PacienteService _pacienteService;

        public PacientesPage()
        {
            InitializeComponent();


            _pacienteService = App.Current.Handler.MauiContext.Services.GetService<PacienteService>();

 
            CargarPacientes();
        }

        private async void CargarPacientes()
        {
            try
            {
                var pacientes = await _pacienteService.GetPacientesAsync();
                PacientesCollectionView.ItemsSource = pacientes;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudieron cargar los pacientes: {ex.Message}", "OK");
            }
        }

        private async void OnCrearPacienteClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CrearPacientePage());
        }

        private async void OnEditarPacienteClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var paciente = button?.CommandParameter as Paciente;
            if (paciente != null)
            {
                await Navigation.PushAsync(new EditarPacientePage(paciente));
            }
        }

        private async void OnEliminarPacienteClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var paciente = button?.CommandParameter as Paciente;

            if (paciente != null)
            {
                var confirmar = await DisplayAlert("Confirmar", $"¿Estás seguro de eliminar a {paciente.Nombres}?", "Sí", "No");

                if (confirmar)
                {
                    var exito = await _pacienteService.DeletePacienteAsync(paciente.PacienteId);
                    if (exito)
                    {
                        await DisplayAlert("Éxito", "Paciente eliminado correctamente.", "OK");
                        CargarPacientes(); 
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo eliminar el paciente.", "OK");
                    }
                }
            }
        }
    }
}
