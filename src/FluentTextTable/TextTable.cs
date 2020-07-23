using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public abstract class TextTable
    {
        internal static IEnumerable<ICellLine> CreatePlainTextCellLines<TItem>(TItem item, IColumn<TItem> column)
        {
            return column
                .GetValues(item)
                .Select(x => x.ToString(column.Format))
                .Select(x => new CellLine(x));
        }
        
        internal static IEnumerable<ICellLine> CreateMarkdownCellLines<TItem>(TItem item, IColumn<TItem> column)
        {
            var strings = column
                .GetValues(item)
                .Select(x => x.ToString(column.Format)); 
            yield return new CellLine(string.Join("<br>", strings));
        }
    }
    
    public class TextTable<TItem> : TextTable, ITextTable<TItem>
    {
        private readonly IHeader _header;
        private readonly IBorders _borders;
        private readonly IMargins _margins;
        private readonly IPaddings _paddings;
        private readonly Func<TItem, IColumn<TItem>, IEnumerable<ICellLine>> _createCellLines;
        internal TextTable(IHeader header, IBorders borders, IMargins margins, IPaddings paddings, Func<TItem, IColumn<TItem>, IEnumerable<ICellLine>> createCellLines)
        {
            _header = header;
            _borders = borders;
            _margins = margins;
            _paddings = paddings;
            _createCellLines = createCellLines;
        }

        public string ToString(IEnumerable<TItem> items)
        {
            using var writer = new StringWriter();
            Write(writer, items);
            return writer.ToString();
        }

        public void WriteLine(IEnumerable<TItem> items)
        {
            Console.Write(ToString(items));
        }

        public void Write(TextWriter textWriter, IEnumerable<TItem> items)
        {
            var rowSet = CreateRowSet(items);
            var tableLayout = new TextTableLayout(_header.Columns, _borders, _margins, _paddings, rowSet);

            _borders.Top.Write(textWriter, tableLayout);
            _header.Write(textWriter, tableLayout);
            _borders.HeaderHorizontal.Write(textWriter, tableLayout);
            rowSet.WriteRows(textWriter, tableLayout);
            _borders.Bottom.Write(textWriter, tableLayout);
        }
        
        private IRowSet CreateRowSet(IEnumerable<TItem> items) =>
            new RowSet(items.Select(CreateRow).ToList());
        
        private IRow CreateRow(TItem item)
        {
            var cells = new Dictionary<IColumn, ICell>();
            foreach (var column in _header.Columns)
            {
                cells[column] = CreateCell(item, (IColumn<TItem>) column);
            }
            return new Row(cells);
        }

        private ICell CreateCell(TItem item, IColumn<TItem> column)
        {
            return new Cell(column, _createCellLines(item, column));
        }

    }
}