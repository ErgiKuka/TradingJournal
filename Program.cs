using TradingJournal.Core.Data;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();


            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.SetCompatibleTextRenderingDefault(false);


            using (var db = new AppDbContext())
            {
                db.Database.EnsureCreated();
            }



            Application.Run(new FrmLoading());


        }
    }
}