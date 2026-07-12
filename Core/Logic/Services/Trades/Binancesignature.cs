using System;
using System.Security.Cryptography;
using System.Text;

namespace TradingJournal.Core.Logic.Services.Exchange
{
    /// <summary>HMAC-SHA256 request signing for Binance (same scheme for spot and futures).</summary>
    internal static class BinanceSignature
    {
        public static string Sign(string query, string apiSecret)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(apiSecret));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(query));
            return Convert.ToHexString(hash).ToLowerInvariant();
        }

        public static long Timestamp() => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }
}