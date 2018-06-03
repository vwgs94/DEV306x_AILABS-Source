using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace AIVisionExplorer
{
    public static class CoreExtensions
    {
        public static string ToFirstCharUpper(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static Color GetColorFromHex(this string hexaColor)
        {
            hexaColor = hexaColor.EnsureStartsWith("#");

            return Color.FromArgb(
                    Convert.ToByte(hexaColor.Substring(1, 2), 16),
                    Convert.ToByte(hexaColor.Substring(3, 2), 16),
                    Convert.ToByte(hexaColor.Substring(5, 2), 16),
                    Convert.ToByte(hexaColor.Substring(7, 2), 16));
        }
                 
        public static string EnsureNotEndsWith(this string value, string endsWith)
        {
            return (value.EndsWith(endsWith)) ? value.Substring(0, value.Length - 1) : value;
        }

        public static string EnsureEndsWith(this string value, string endsWith)
        {
            return (value.EndsWith(endsWith)) ? value : value + endsWith;
        }

        public static string EnsureNotStartsWith(this string value, string startsWith)
        {
            return (value.StartsWith(startsWith)) ? value.Substring(1, value.Length - 1) : value;
        }

        public static string EnsureStartsWith(this string value, string startsWith)
        {
            return (value.StartsWith(startsWith)) ? value : startsWith + value;
        }
    }
}
