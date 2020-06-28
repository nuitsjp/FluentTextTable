using System.IO;

namespace FluentTextTable
{
    public class Padding : IPadding
    {
        public const int DefaultWidth = 1;

        public Padding(int width)
        {
            Width = width;
        }

        public int Width { get; }
    }
}