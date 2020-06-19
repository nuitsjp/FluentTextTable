using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public abstract class Table<TItem> : ITable<TItem>
    {
        private readonly Func<IEnumerable<object>, string, IEnumerable<string>> _toStrings;
        private readonly List<IColumn<TItem>> _columns;
        internal Table(int padding, List<IColumn<TItem>> columns, Borders borders, Func<IEnumerable<object>, string, IEnumerable<string>> toStrings)
        {
            Padding = padding;
            _columns = columns;
            Borders = borders;
            _toStrings = toStrings;
        }

        public IReadOnlyList<IColumn> Columns => _columns;

        public int Padding { get; }
        internal Borders Borders { get; }

        public string ToString(IEnumerable<TItem> items)
        {
            using var writer = new StringWriter();
            Write(writer, items);
            return writer.ToString();
        }

        public void Write(TextWriter writer, IEnumerable<TItem> items)
        {
            var rows = items.Select(NewRow).ToList();
            var tableInstance = new TableInstance<TItem>(this, rows);
            var columnWidths = new ColumnWidths(this, rows);
            tableInstance.Write(writer);
        }

        // public void Write(TextWriter writer)
        // {
        //     Borders.Top.Write(writer, this);
        //     WriteHeader(writer);
        //     Borders.HeaderHorizontal.Write(writer, this);
        //     WriteRows(writer);
        //     Borders.Bottom.Write(writer, this);
        // }
        //
        // private void WriteHeader(TextWriter writer)
        // {
        //     Borders.Left.Write(writer);
        //
        //     WriteHeaderColumn(writer, Columns[0]);
        //
        //     for (var i = 1; i < Columns.Count; i++)
        //     {
        //         Borders.InsideVertical.Write(writer);
        //         WriteHeaderColumn(writer, Columns[i]);
        //     }
        //     
        //     Borders.Right.Write(writer);
        //
        //     writer.WriteLine();
        // }
        //  
        // private void WriteHeaderColumn(TextWriter writer, IColumn column)
        // {
        //     writer.Write(new string(' ', Padding));
        //     writer.Write(column.Name);
        //     writer.Write(new string(' ', GetColumnWidth(column) - column.HeaderWidth - Padding));
        // }
        //
        // private void WriteRows(TextWriter writer, IList<Row<TItem>> rows)
        // {
        //     if (rows.Any())
        //     {
        //         rows[0].Write(writer, this);
        //         for (var i = 1; i < rows.Count; i++)
        //         {
        //             Borders.InsideHorizontal.Write(writer, this);
        //             rows[i].Write(writer, this);
        //         }
        //     }
        // }
        private Row<TItem> NewRow(TItem item)
        {
            return new Row<TItem>(_columns.Select(column => NewCell(item, column)));
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