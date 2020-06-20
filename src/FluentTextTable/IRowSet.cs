using System.Collections.Generic;
using System.IO;

namespace FluentTextTable
{
    public interface IRowSet
    {
        int GetColumnWidth(IColumn column);
        void WriteRows(TextWriter writer, ITextTableLayout textTableLayout);
    }
}