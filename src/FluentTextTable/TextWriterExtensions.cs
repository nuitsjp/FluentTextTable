using System.IO;

namespace FluentTextTable
{
    internal static class TextWriterExtensions
    {
        internal static void Write(this TextWriter writer, string s, int count)
        {
            for (int i = 0; i < count; i++)
            {
                writer.Write(s);
            }
        }
    }
}