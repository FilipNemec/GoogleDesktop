using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static string Truncate(this string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }

        public static IEnumerable<string> SplitInParts(this string s, int partLength)
        {
            var splitString = s.Split(' ','\n');
            string output = string.Empty;
            foreach (var part in splitString)
            {
                output += part + " ";
                if (output.Length >= partLength)
                {
                    yield return output;
                    output = string.Empty;
                }
            }
        }
    }
}
