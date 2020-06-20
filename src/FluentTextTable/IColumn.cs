using System.Collections.Generic;
using System.IO;

namespace FluentTextTable
{
    public interface IColumn
    {
        string Name { get; }
        string Format { get; }
        int HeaderWidth { get; }
        HorizontalAlignment HorizontalAlignment { get; }
        VerticalAlignment VerticalAlignment { get; }

        void WriteHeader(TextWriter writer, ITextTableLayout textTableLayout);
    }
    public interface IColumn<in TItem> : IColumn
    {
        IEnumerable<object> GetValues(TItem item);
    }
}