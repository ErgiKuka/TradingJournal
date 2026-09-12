using System;
using System.IO;

namespace TradingJournal.Core.Logic.Services.Exchange
{
    /// <summary>
    /// Append-only trace for the auto-journal path.
    ///
    /// Exists because the reconcile path reports through <c>SetStatus</c>, a label on FrmTrading that
    /// the 3-second positions timer overwrites almost immediately — so every error and every
    /// "nothing found" result has been invisible in practice. A file survives that.
    ///
    /// Deliberately dumb and non-fatal: any IO failure is swallowed, because a diagnostic must never
    /// be able to break the thing it is diagnosing. Set <see cref="Enabled"/> to false to switch off.
    /// </summary>
    public static class AutoJournalTrace
    {
        private static readonly object Gate = new();

        public static readonly string LogPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "TradingJournal", "autojournal.log");

        public static bool Enabled { get; set; } = true;

        public static void Write(string message)
        {
            if (!Enabled) return;

            try
            {
                lock (Gate)
                {
                    var dir = Path.GetDirectoryName(LogPath);
                    if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir)) Directory.CreateDirectory(dir);

                    File.AppendAllText(LogPath,
                        $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}Z  {message}{Environment.NewLine}");
                }
            }
            catch
            {
                // A diagnostic that throws is worse than no diagnostic.
            }
        }
    }
}