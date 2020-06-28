using System.IO;

namespace FluentTextTable
{
    public class Margin : IMargin
    {
        public const int LeftDefaultWidth = 1;

        public const int RightDefaultWidth = 0;

        private readonly int _width;

        public Margin(int width)
        {
            _width = width;
        }

        public void Write(TextWriter textWriter) => textWriter.Write(new string(' ', _width));
    }
}