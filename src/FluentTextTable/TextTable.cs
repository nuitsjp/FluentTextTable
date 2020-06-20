using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public abstract class TextTable
    {
        
        public static ITextTable<TItem> Build<TItem>()
            => Build<TItem>(x => x.EnableAutoGenerateColumns());
        
        public static ITextTable<TItem> Build<TItem>(Action<IPlainTextTableConfig<TItem>> configure)
        {
            var config = new PlainTextTableConfig<TItem>();
            configure(config);
            if (config.IsEnableGenerateColumns)
            {
                config.GenerateColumns();
            }
            return config.Build();
        }

        public static ITextTable<TItem> BuildMarkdown<TItem>()
            => BuildMarkdown<TItem>(x => x.EnableAutoGenerateColumns());

        public static ITextTable<TItem> BuildMarkdown<TItem>(Action<IMarkdownTableConfig<TItem>> configure)
        {
            var config = new MarkdownTableConfig<TItem>();
            configure(config);
            if (config.IsEnableGenerateColumns)
            {
                config.GenerateColumns();
            }
            return config.Build();
        }

        protected static IEnumerable<CellLine> NewPlainTextCellLines<TItem>(TItem item, IColumn<TItem> column)
        {
            return column
                .GetValues(item)
                .Select(x => x.ToString(column.Format))
                .Select(x => new CellLine(x));
        }
        
        protected static IEnumerable<CellLine> NewMarkdownCellLines<TItem>(TItem item, IColumn<TItem> column)
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
        private readonly Borders _borders;
        private readonly int _padding;
        private readonly Func<TItem, IColumn<TItem>, IEnumerable<CellLine>> _newCellLines;
        private TextTable(int padding, IHeader header, Borders borders, Func<TItem, IColumn<TItem>, IEnumerable<CellLine>> newCellLines)
        {
            _header = header;
            _padding = padding;
            _borders = borders;
            _newCellLines = newCellLines;
        }

        internal static ITextTable<TItem> NewPlainTextTable(int padding, IHeader header, Borders borders)
            => new TextTable<TItem>(padding, header, borders, NewPlainTextCellLines);

        internal static ITextTable<TItem> NewMarkdownTable(int padding, IHeader header, Borders borders)
            => new TextTable<TItem>(padding, header, borders, NewMarkdownCellLines);

        public string ToString(IEnumerable<TItem> items)
        {
            using var writer = new StringWriter();
            Write(writer, items);
            return writer.ToString();
        }

        public void Write(TextWriter writer, IEnumerable<TItem> items)
        {
            var rowSet = NewRowSet(items);
            var tableLayout = new TextTableLayout(_header.Columns, _borders, _padding, rowSet);

            _borders.Top.Write(writer, tableLayout);
            _header.Write(writer, tableLayout);
            _borders.HeaderHorizontal.Write(writer, tableLayout);
            rowSet.WriteRows(writer, tableLayout);
            _borders.Bottom.Write(writer, tableLayout);
        }
        
        private IRowSet NewRowSet(IEnumerable<TItem> items) =>
            new RowSet(items.Select(NewRow).ToList());
        
        private Row NewRow(TItem item)
        {
            return new Row(_header.Columns.Select(column => NewCell(item, (IColumn<TItem>)column)));
        }

        private Cell NewCell(TItem item, IColumn<TItem> column)
        {
            return new Cell(column, _newCellLines(item, column));
        }

    }
}