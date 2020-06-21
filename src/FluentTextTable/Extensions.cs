using System.Collections.Generic;
using System.IO;

namespace FluentTextTable
{
    internal static class Extensions
    {
        internal static IEnumerable<object> SplitOnNewLine(this string value)
        {
            using var reader = new StringReader(value);
            for (var line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                yield return line;
            }
        }

        internal static string ToString(this object value, string format) =>
            format is null
                ? value is null
                    ? string.Empty
                    : value.ToString()
                : string.Format(format, value);

        internal static int GetWidth(this string value) =>
            EastAsianWidthDotNet.StringExtensions.GetWidth(value);

    }
}