using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace FluentTextTable
{
    public abstract class Table<TItem> : ITable<TItem>
    {
        internal Table(int padding, List<IColumn<TItem>> columns)
        {
            Padding = padding;
            Columns = columns;
        }

        internal int Padding { get; }
        internal IReadOnlyList<IColumn<TItem>> Columns { get; }

        public string ToString(IEnumerable<TItem> items)
        {
            using var writer = new StringWriter();
            Write(writer, items);
            return writer.ToString();
        }
        public abstract void Write(TextWriter writer, IEnumerable<TItem> items);
    }
}