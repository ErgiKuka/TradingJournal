using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using TradingJournal.Core.Data;
using TradingJournal.Core.Data.Entities;
using TradingJournal.Core.Logic.Manager;

namespace TradingJournal.Core.Logic.Services
{
    public enum AccountTransactionType { Deposit, Withdraw }

    public sealed class AccountTransactionRecord
    {
        public DateTime Date { get; set; }
        public AccountTransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; } = "";
    }

    /// <summary>
    /// Simple, reliable ledger persisted as JSON under AppData\TradingJournal\account_transactions.json
    /// We accept an AppDbContext parameter to match existing callsites, but we do not depend on EF here.
    /// </summary>
    public static class AccountTransactionsService
    {
        private static readonly string Folder =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TradingJournal");
        private static readonly string FilePath = Path.Combine(Folder, "account_transactions.json");

        public static void Append(AppDbContext _dbIgnored, AccountTransactionRecord record)
        {
            var all = ReadAll();
            all.Add(record);
            SaveAll(all);
        }

        public static IEnumerable<AccountTransactionRecord> QuerySince(AppDbContext _dbIgnored, DateTime since)
            => ReadAll().Where(x => x.Date >= since).OrderByDescending(x => x.Date);

        /// <summary>
        /// Deletes a record by matching Date/Type/Amount (the grid binds an anonymous type).
        /// </summary>
        public static bool DeleteMatching(AppDbContext _dbIgnored, object row)
        {
            var dateProp = row.GetType().GetProperty("Date");
            var typeProp = row.GetType().GetProperty("Type");
            var amountProp = row.GetType().GetProperty("Amount");
            if (dateProp == null || typeProp == null || amountProp == null) return false;

            var date = (DateTime)dateProp.GetValue(row)!;
            var type = (AccountTransactionType)typeProp.GetValue(row)!;
            var amount = Convert.ToDecimal(amountProp.GetValue(row)!, CultureInfo.InvariantCulture);

            var all = ReadAll();
            var match = all.FirstOrDefault(x =>
                x.Type == type &&
                x.Amount == amount &&
                Math.Abs((x.Date - date).TotalSeconds) < 1.0);

            if (match == null) return false;

            // --- NEW: send to recycle bin before removal ---
            try
            {
                // Retention days from settings (fallback to service default)
                var s = SettingsManager.Load();
                var retention = s.RecycleBin?.RetentionDays ?? 0;
                if (retention <= 0) retention = RecycleBinService.RetentionDaysDefault;
                if (retention < 1) retention = 1;
                if (retention > 180) retention = 180;

                // Capture payload in DB (AccountTransaction)
                RecycleBinService
                    .CaptureAccountTransactionAsync(
                        _dbIgnored,
                        match.Date,
                        (int)match.Type,
                        match.Amount,
                        match.Note,
                        retention
                    )
                    .GetAwaiter()
                    .GetResult();
            }
            catch
            {
                // If recycle bin write fails, do NOT hard-delete; bail out gracefully.
                return false;
            }

            all.Remove(match);
            SaveAll(all);
            return true;
        }

        private static List<AccountTransactionRecord> ReadAll()
        {
            try
            {
                Directory.CreateDirectory(Folder);
                if (!File.Exists(FilePath)) return new List<AccountTransactionRecord>();
                var text = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<AccountTransactionRecord>>(text) ?? new List<AccountTransactionRecord>();
            }
            catch
            {
                return new List<AccountTransactionRecord>();
            }
        }

        private static void SaveAll(List<AccountTransactionRecord> list)
        {
            Directory.CreateDirectory(Folder);
            var json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }
    }
}
