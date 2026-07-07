using System.Globalization;

namespace TradingJournal.Core.Logic.Helpers
{
    /// <summary>
    /// Parsing for free-text numeric inputs (prices, %, leverage).
    ///
    /// Why this exists: <c>decimal.Parse(text)</c> throws on bad input, and plain
    /// <c>TryParse</c> uses the current culture only. On a machine set to a comma-decimal
    /// locale (e.g. sq-AL), "63320.3" would then fail to parse — a real, silent trap in a
    /// trading tool. Trading values conventionally use '.' as the decimal separator, so we
    /// try invariant first and fall back to the current culture.
    ///
    /// Deliberate limitation: thousands separators are NOT accepted. That removes the
    /// classic "1,5" -> 15 ambiguity (which is 1.5 for a European user) at the cost of not
    /// supporting grouped input like "63,320.3" — acceptable for single-value input boxes.
    /// </summary>
    public static class NumericInput
    {
        private const NumberStyles Styles =
            NumberStyles.AllowDecimalPoint |
            NumberStyles.AllowLeadingSign |
            NumberStyles.AllowLeadingWhite |
            NumberStyles.AllowTrailingWhite;

        public static bool TryParseDecimal(string text, out decimal value)
        {
            value = 0m;
            if (string.IsNullOrWhiteSpace(text))
                return false;

            text = text.Trim();

            if (decimal.TryParse(text, Styles, CultureInfo.InvariantCulture, out value))
                return true;

            return decimal.TryParse(text, Styles, CultureInfo.CurrentCulture, out value);
        }
    }
}