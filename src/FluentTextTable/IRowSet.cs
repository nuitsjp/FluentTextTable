using System.IO;

namespace FluentTextTable
{
    public interface IRowSet
    {
        int GetMaxCellWidth(IColumn column);
        void WriteRows(TextWriter textWriter, ITextTableLayout textTableLayout);
    }
}