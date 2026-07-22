using System;
using System.Security.Cryptography;
using System.Text;

namespace TradingJournal.Core.Logic.Services.Exchange
{
    /// <summary>
    /// HMAC-SHA256 request signing for the Bybit V5 API.
    /// Signature = hex( HMAC_SHA256( timestamp + apiKey + recvWindow + payload ) ), where payload is
    /// the query string (GET) or the raw JSON body (POST). The payload must be byte-identical to what
    /// is actually sent, so sign the exact string you transmit.
    /// </summary>
    internal static class BybitSignature
    {
        public static string Sign(string timestamp, string apiKey, string recvWindow, string payload, string apiSecret)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(apiSecret));
            var pre = timestamp + apiKey + recvWindow + (payload ?? string.Empty);
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(pre));
            return Convert.ToHexString(hash).ToLowerInvariant();
        }

        public static string Timestamp() => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
    }
}