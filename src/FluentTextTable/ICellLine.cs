using System.IO;

namespace FluentTextTable
{
    public interface ICellLine
    {
        int Width { get; }

        void Write(
            TextWriter textWriter,
            ITextTableLayout textTableLayout,
            IColumn column);
    }
}