using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public abstract class Table : ITable
    {
        
        public static ITable<TItem> Build<TItem>()
            => Build<TItem>(x => x.EnableGenerateColumns());
        
        public static ITable<TItem> Build<TItem>(Action<ITextTableConfig<TItem>> configure)
        {
            var config = new TextTableConfig<TItem>();
            configure(config);
            if (config.IsEnableGenerateColumns)
            {
                config.GenerateColumns();
            }
            return config.Build();
        }

        public static ITable<TItem> BuildMarkdown<TItem>()
            => BuildMarkdown<TItem>(x => x.EnableGenerateColumns());

        public static ITable<TItem> BuildMarkdown<TItem>(Action<ITableConfig<TItem>> configure)
        {
            var config = new MarkdownTableConfig<TItem>();
            configure(config);
            if (config.IsEnableGenerateColumns)
            {
                config.GenerateColumns();
            }
            return config.Build();
        }
    }
    
    public class Table<TItem> : Table, ITable<TItem>
    {
        private readonly IHeader _header;
        private readonly Borders _borders;
        private readonly int _padding;
        private readonly Func<IEnumerable<object>, string, IEnumerable<string>> _toStrings;
        internal Table(int padding, IHeader header, Borders borders, Func<IEnumerable<object>, string, IEnumerable<string>> toStrings)
        {
            _header = header;
            _padding = padding;
            _borders = borders;
            _toStrings = toStrings;
        }


        public string ToString(IEnumerable<TItem> items)
        {
            using var writer = new StringWriter();
            Write(writer, items);
            return writer.ToString();
        }

        public void Write(TextWriter writer, IEnumerable<TItem> items)
        {
            var rowSet = NewRowSet(items);
            var tableLayout = new TableLayout(_header.Columns, _borders, _padding, rowSet);

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
            var value = column.GetValue(item);
            IEnumerable<object> objects;
            if (value is string stringValue)
            {
                objects = stringValue.SplitOnNewLine();
            }
            else if (value is IEnumerable<object> enumerable)
            {
                objects = enumerable;
            }
            else
            {
                objects = new[] {value};
            }

            return new Cell(column, NewCellLines(column, objects));
        }

        private IEnumerable<CellLine> NewCellLines(IColumn<TItem> column, IEnumerable<object> objects)
        {
            return _toStrings(objects, column.Format).Select(x => new CellLine(x));
        }
    }
}