using System.Text.Json;

namespace ProductWinForm
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            LoadConfigs("appsettings.json");
            Application.Run(new Form1());
        }

        internal static AppConfigs Configuration { get; set; } = default!;
        private static void LoadConfigs(string file)
        {
            if (File.Exists(file)) { }
            var json = File.ReadAllText(file);
            Configuration = JsonSerializer.Deserialize<AppConfigs>(json) ?? new();
        }
    }
}