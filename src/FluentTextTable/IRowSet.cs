using System.Collections.Generic;
using System.IO;

namespace FluentTextTable
{
    public interface IRowSet
    {
        int GetWidthOf(IColumn column);
        void WriteRows(TextWriter writer, ITextTableLayout textTableLayout);
    }
}