using System;
using System.Security.Cryptography;
using System.Text;

namespace TradingJournal.Core.Logic.Services.Exchange
{
    /// <summary>
    /// Request signing for the Kraken Futures v3 REST API. Confirmed against Kraken's live docs
    /// (docs.kraken.com/api/docs/guides/futures-rest, "Authent"):
    ///
    ///   1. Concatenate postData + nonce + endpointPath
    ///   2. SHA-256 that string
    ///   3. Base64-DECODE the api secret
    ///   4. HMAC-SHA-512 the SHA-256 result, keyed with the decoded secret
    ///   5. Base64-ENCODE the HMAC result  -> this is the Authent header
    ///
    /// CRITICAL GOTCHA: endpointPath is the "/api/v3/..." form, NOT the "/derivatives/api/v3/..."
    /// URL you actually POST to. Signing with the /derivatives path is the #1 cause of Kraken
    /// "authenticationError" — if auth fails on the demo env, check this first.
    ///
    /// postData is the request's query string exactly as sent (url-encoded). Kraken's Feb-2024 auth
    /// update requires hashing the url-encoded component as it appears in the request, so sign the
    /// exact string you transmit.
    /// </summary>
    internal static class KrakenFuturesSignature
    {
        public static string Authent(string postData, string nonce, string endpointPath, string apiSecret)
        {
            byte[] sha256 = SHA256.HashData(Encoding.UTF8.GetBytes((postData ?? string.Empty) + nonce + endpointPath));
            byte[] secret = Convert.FromBase64String(apiSecret);
            using var hmac = new HMACSHA512(secret);
            byte[] mac = hmac.ComputeHash(sha256);
            return Convert.ToBase64String(mac);
        }

        public static string Nonce() => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
    }
}