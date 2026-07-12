using System;
using System.Security.Cryptography;
using System.Text;

namespace TradingJournal.Core.Logic.Helpers
{
    /// <summary>
    /// Encrypts/decrypts exchange API credentials with Windows DPAPI (CurrentUser scope).
    ///
    /// The OS binds the ciphertext to the logged-in Windows account, so the encryption key
    /// never exists in this app's code or binary. A copied database/backup is therefore useless
    /// on another account or machine.
    ///
    /// Honest limitation: DPAPI protects against a stolen file/DB — NOT against someone who is
    /// already signed into the same Windows account (they could just open the app anyway). For a
    /// desktop app that's the correct, standard trade-off. The real safety net is the exchange key
    /// itself: create it with withdrawals disabled and an IP allow-list.
    ///
    /// Requires NuGet package: System.Security.Cryptography.ProtectedData (Windows-only).
    /// </summary>
    public static class ApiCredentialProtector
    {
        // Extra entropy. NOT a secret (it ships in the binary); it only scopes this app's
        // ciphertext so an unrelated DPAPI caller can't accidentally decrypt it.
        private static readonly byte[] Entropy = Encoding.UTF8.GetBytes("TradingJournal.ExchangeKeys.v1");

        public static byte[] Protect(string plaintext)
        {
            var data = Encoding.UTF8.GetBytes(plaintext ?? string.Empty);
            return ProtectedData.Protect(data, Entropy, DataProtectionScope.CurrentUser);
        }

        public static string Unprotect(byte[] ciphertext)
        {
            if (ciphertext == null || ciphertext.Length == 0) return string.Empty;
            var data = ProtectedData.Unprotect(ciphertext, Entropy, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(data);
        }

        /// <summary>True if the blob can still be decrypted on this account (e.g. after a profile change it can't).</summary>
        public static bool CanUnprotect(byte[] ciphertext)
        {
            try { Unprotect(ciphertext); return true; }
            catch { return false; }
        }
    }
}