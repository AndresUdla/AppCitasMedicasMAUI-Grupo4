using System.Text;

namespace AppCitasMedicasMAUI.Services
{
    public class LogService
    {
        private readonly string _logPath;

        public LogService()
        {
            var folder = FileSystem.AppDataDirectory;
            _logPath = Path.Combine(folder, "log_admin.txt");
        }

        public async Task RegistrarAccionAsync(string mensaje)
        {
            string entrada = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {mensaje}{Environment.NewLine}";
            try
            {
                await File.AppendAllTextAsync(_logPath, entrada, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar log: {ex.Message}");
            }
        }

        public async Task<string> LeerLogAsync()
        {
            if (!File.Exists(_logPath))
                return "No hay registros.";

            return await File.ReadAllTextAsync(_logPath);
        }

        public void LimpiarLog()
        {
            if (File.Exists(_logPath))
                File.Delete(_logPath);
        }
    }
}
