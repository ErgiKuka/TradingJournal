using Microsoft.EntityFrameworkCore;
using TradingJournal.Core.Data;
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Core.Logic.Manager;
using TradingJournal.Pl.Startup;

namespace TradingJournal
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var settings = SettingsManager.Load();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();


            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.SetCompatibleTextRenderingDefault(false);
            ThemeManager.SetTheme(settings.Theme);

            using (var db = new AppDbContext())
            {
                db.Database.Migrate();
            }



            Application.Run(new FrmLoading());


        }
    }
}