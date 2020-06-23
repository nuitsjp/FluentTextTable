using System.IO;

namespace FluentTextTable
{
    public interface IHorizontalBorder
    {
        int LineStyleWidth { get; }
        void Write(TextWriter textWriter, ITextTableLayout textTableLayout);
    }
}