using HtmlAgilityPack;
using System;
using System.Globalization;
using System.Linq;

namespace Link2Web.Core
{
    public static class MyFunctions
    {
        /// <summary>
        /// Convert String to Double
        /// </summary>
        /// <param name="s">string s</param>
        /// <returns>double value</returns>
        public static double StringToDouble(string s)
        {
            double result;
            var valid = double.TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, out result);
            result = Math.Round(result, 2);
            return valid ? result : 0;
        }

        /// <summary>
        /// Convert String to DateTime
        /// </summary>
        /// <param name="s">string s</param>
        /// <param name="format">string format</param>
        /// <returns>DateTime value</returns>
        public static DateTime StringToDateTime(string s, string format)
        {
            DateTime result;
            var valid = DateTime.TryParseExact(s, format, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out result);
            return valid ? result : DateTime.Now;
        }

        public static double GetDouble(string value, double defaultValue)
        {
            string output;
            double result;

            var currentCulture = CultureInfo.InstalledUICulture;
            var numberFormat = (NumberFormatInfo) currentCulture.NumberFormat.Clone();
            numberFormat.NumberDecimalSeparator = ".";

            var tryParse = double.TryParse(value, NumberStyles.Any, numberFormat, out result);
            result = tryParse ? result : defaultValue;

            return result;
        }

        public static bool CheckUrlExists(string url, string anchor)
        {
            var page = new HtmlWeb().Load(url);
            var linkExists = page.DocumentNode.Descendants("a")
                           .Where(a => a.InnerText == url )
                           .Select(a => a.Attributes["href"].Value)
                           .ToList().Count > 0;

            return linkExists;
        }
    }
}