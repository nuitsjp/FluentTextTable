using System.Collections.Generic;
using System.IO;

namespace FluentTextTable
{
    public interface IRow
    {
        int GetWidthOf(IColumn column);
        void Write(TextWriter writer, ITextTableLayout textTableLayout);
    }
}