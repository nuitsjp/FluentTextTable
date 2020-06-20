using System.Collections.Generic;
using System.IO;

namespace FluentTextTable
{
    public interface IRow
    {
        int GetColumnWidth(IColumn column);
        void Write(TextWriter writer, ITextTableLayout textTableLayout);
    }
}