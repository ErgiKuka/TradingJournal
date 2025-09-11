using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingJournal.Core.Data
{
    public static class DataAccess
    {
        public static string GetDatabasePath()
        {
            // 1. Get the path to the user's local AppData folder (e.g., C:\Users\YourName\AppData\Local)
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            // 2. Create a subfolder for your application to keep things tidy (e.g., C:\Users\YourName\AppData\Local\TradingJournal)
            // Using the publisher name from your installer is good practice.
            string appSpecificFolder = Path.Combine(appDataFolder, "TradingJournal");

            // 3. Ensure this folder exists.
            Directory.CreateDirectory(appSpecificFolder);

            // 4. Return the full, safe path to your database file.
            return Path.Combine(appSpecificFolder, "tradingjournal.db");
        }
    }
}
