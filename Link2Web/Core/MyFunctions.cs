using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

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
            double result;
            string output;

            // Check if last seperator==groupSeperator
            string groupSep = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
            if (value.LastIndexOf(groupSep) + 4 == value.Count())
            {
                bool tryParse = double.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out result);
                result = tryParse ? result : defaultValue;
            }
            else
            {
                // Unify string (no spaces, only . )
                output = value.Trim().Replace(" ", string.Empty).Replace(",", ".");

                // Split it on points
                string[] split = output.Split('.');

                if (split.Count() > 1)
                {
                    // Take all parts except last
                    output = string.Join(string.Empty, split.Take(split.Count() - 1).ToArray());

                    // Combine token parts with last part
                    output = string.Format("{0}.{1}", output, split.Last());
                }
                // Parse double invariant
                result = double.Parse(output, CultureInfo.InvariantCulture);
            }
            return result;
        }
    }
}