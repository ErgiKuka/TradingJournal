using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TradingJournal.Core.Logic.Helpers;

namespace TradingJournal.Core.Logic.Manager
{
    public class SettingsManager
    {
        private const string SettingsFile = "user_settings.json";

        public decimal AccountBalance { get; set; }

        public AppTheme Theme { get; set; } = AppTheme.Dark;

        public void Save()
        {
            var json = JsonSerializer.Serialize(this);
            File.WriteAllText(SettingsFile, json);
        }

        public static SettingsManager Load()
        {
            if (File.Exists(SettingsFile))
            {
                try
                {
                    var json = File.ReadAllText(SettingsFile);
                    return JsonSerializer.Deserialize<SettingsManager>(json) ?? new SettingsManager();
                }
                catch
                {
                    return new SettingsManager();
                }
            }
            return new SettingsManager();
        }
    }
}
