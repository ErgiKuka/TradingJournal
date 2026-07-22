using System;
using System.Collections.Generic;
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

        // ---- Existing ----
        public decimal AccountBalance { get; set; }
        public AppTheme Theme { get; set; } = AppTheme.Dark;

        // ---- New ----
        public string CurrencySymbol { get; set; } = "$";

        public TradingSettings Trading { get; set; } = new();
        public NotificationSettings Notifications { get; set; } = new();
        public BackupSettings Backup { get; set; } = new();

        // ---- NEW: Recycle Bin settings ----
        public RecycleBinSettings RecycleBin { get; set; } = new();

        // ---- NEW: Auto-journal (import closed exchange trades into the journal) ----
        public AutoJournalSettings AutoJournal { get; set; } = new();

        public void Save()
        {
            try
            {
                if (!Directory.Exists(SettingsFolder))
                    Directory.CreateDirectory(SettingsFolder);

                var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(SettingsFile, json);
            }
            catch (Exception ex)
            {
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
                    var s = JsonSerializer.Deserialize<SettingsManager>(json);
                    if (s != null)
                    {
                        // Soft-migrate in case file is old and missing new sections
                        s.Trading ??= new TradingSettings();
                        s.Notifications ??= new NotificationSettings();
                        s.Backup ??= new BackupSettings();
                        s.RecycleBin ??= new RecycleBinSettings();

                        s.AutoJournal ??= new AutoJournalSettings();               // <— ADDED
                        s.AutoJournal.WatermarksUtc ??= new Dictionary<string, string>(); // <— ADDED

                        if (string.IsNullOrWhiteSpace(s.CurrencySymbol)) s.CurrencySymbol = "$";
                        return s;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading settings: {ex.Message}");
            }
            return new SettingsManager();
        }
    }

    public sealed class TradingSettings
    {
        /// <summary> "Slash" -> BTC/USDT, "Concat" -> BTCUSDT </summary>
        public string SymbolStyle { get; set; } = "Slash";
        public int PricePrecision { get; set; } = 3;
        public int QtyPrecision { get; set; } = 3;
    }

    public sealed class NotificationSettings
    {
        public bool RecoveryMilestoneEnabled { get; set; } = true;
        public int RecoveryMilestonePct { get; set; } = 60;
        public bool BackupStatusEnabled { get; set; } = true;

        // ---- NEW: used by FrmSettings for the daily summary checkbox ----
        public bool DailySummaryEnabled { get; set; } = false;
    }

    public sealed class BackupSettings
    {
        public bool AutoEnabled { get; set; } = true;
        /// <summary> Stored as 24h HH:mm (e.g. "23:00") </summary>
        public string Time { get; set; } = "23:00";
        /// <summary> "Daily" | "Weekly" | "Monthly" </summary>
        public string Frequency { get; set; } = "Daily";
        public int KeepLast { get; set; } = 15;
    }

    // ---- NEW: Recycle Bin section ----
    public sealed class RecycleBinSettings
    {
        /// <summary>Days before soft-deleted items are permanently deleted.</summary>
        public int RetentionDays { get; set; } = 30;
    }

    // ---- NEW: Auto-journal section ----
    public sealed class AutoJournalSettings
    {
        /// <summary>Master on/off for importing closed exchange trades into the journal.</summary>
        public bool Enabled { get; set; } = false;

        /// <summary>
        /// Per-platform high-water mark: key "{Exchange}:{PlatformId}" -> ISO-8601 UTC time of the
        /// newest close already imported. Only a performance hint — dedup by ExternalId is the real
        /// guard, so an empty/lost value just triggers a redundant pull, never duplicate rows.
        /// </summary>
        public Dictionary<string, string> WatermarksUtc { get; set; } = new();
    }
}