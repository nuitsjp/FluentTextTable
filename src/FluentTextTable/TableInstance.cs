using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class TableInstance<TItem> : ITableInstance<TItem>
    {
        private readonly Table<TItem> _table;

        private readonly List<Row<TItem>> _rows;

        private readonly IColumnWidths _columnWidths;

        internal TableInstance(Table<TItem> table, IEnumerable<Row<TItem>> rows)
        {
            _table = table;
            _rows = rows.ToList();
            _columnWidths = new ColumnWidths(table, _rows);
        }

        public Borders Borders => _table.Borders;
        public IReadOnlyList<IColumn> Columns => _table.Columns;
        public int Padding => _table.Padding;
        public int GetColumnWidth(IColumn column) => _columnWidths[column];
        

        public void Write(TextWriter writer)
        {
            Borders.Top.Write(writer, this);
            WriteHeader(writer);
            Borders.HeaderHorizontal.Write(writer, this);
            WriteRows(writer);
            Borders.Bottom.Write(writer, this);
        }

        private void WriteHeader(TextWriter writer)
        {
            Borders.Left.Write(writer);

            WriteHeaderColumn(writer, Columns[0]);

            for (var i = 1; i < Columns.Count; i++)
            {
                Borders.InsideVertical.Write(writer);
                WriteHeaderColumn(writer, Columns[i]);
            }
            
            Borders.Right.Write(writer);

            writer.WriteLine();
        }
         
        private void WriteHeaderColumn(TextWriter writer, IColumn column)
        {
            writer.Write(new string(' ', Padding));
            writer.Write(column.Name);
            writer.Write(new string(' ', GetColumnWidth(column) - column.HeaderWidth - Padding));
        }

        private void WriteRows(TextWriter writer)
        {
            if (_rows.Any())
            {
                _rows[0].Write(writer, this);
                for (var i = 1; i < _rows.Count; i++)
                {
                    Borders.InsideHorizontal.Write(writer, this);
                    _rows[i].Write(writer, this);
                }
            }
        }

        public string ToString(IEnumerable<TItem> items)
        {
            throw new NotImplementedException();
        }

        public void Write(TextWriter writer, IEnumerable<TItem> items)
        {
            throw new NotImplementedException();
        }
    }
}