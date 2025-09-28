using System;
using System.IO;
using System.Text.Json;
using TradingJournal.Core.Logic.Helpers;

namespace TradingJournal.Core.Logic.Manager
{
    public class SettingsManager
    {
        private static readonly string SettingsFolder =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TradingJournal");

        private static readonly string SettingsFile =
            Path.Combine(SettingsFolder, "user_settings.json");

        public decimal AccountBalance { get; set; }
        public AppTheme Theme { get; set; } = AppTheme.Dark;

        public void Save()
        {
            try
            {
                // Make sure the folder exists
                if (!Directory.Exists(SettingsFolder))
                    Directory.CreateDirectory(SettingsFolder);

                var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(SettingsFile, json);
            }
            catch (Exception ex)
            {
                // Optional: log error, but don’t crash the app
                Console.WriteLine($"Error saving settings: {ex.Message}");
            }
        }

        public static SettingsManager Load()
        {
            try
            {
                if (File.Exists(SettingsFile))
                {
                    var json = File.ReadAllText(SettingsFile);
                    return JsonSerializer.Deserialize<SettingsManager>(json) ?? new SettingsManager();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading settings: {ex.Message}");
            }

            return new SettingsManager();
        }
    }
}
