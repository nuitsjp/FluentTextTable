using System.Collections.Generic;
using System.IO;

namespace FluentTextTable
{
    public interface IColumn
    {
        string Format { get; }
        int Width { get; }
        HorizontalAlignment HorizontalAlignment { get; }
        VerticalAlignment VerticalAlignment { get; }

        void Write(TextWriter textWriter, ITextTableLayout textTableLayout);
    }
    public interface IColumn<in TItem> : IColumn
    {
        IEnumerable<object> GetValues(TItem item);
    }
}