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
            var valid = double.TryParse(s, out result);
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
    }
}