using System.IO;

namespace FluentTextTable
{
    public interface ICellLine
    {
        int Width { get; }

        void Write(
            TextWriter textWriter,
            IColumn column,
            int columnWidth,
            int padding);
    }
}