using System.IO;

namespace FluentTextTable
{
    public interface ICell
    {
        int Width { get; }
        int Height { get; }

        void Write(
            TextWriter textWriter,
            int rowHeight,
            int lineNumber,
            int columnWidth,
            int padding);
    }
}