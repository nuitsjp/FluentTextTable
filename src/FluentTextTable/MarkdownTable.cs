using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentTextTable.Markdown;

namespace FluentTextTable
{
    public class MarkdownTable<TItem> : ITable<TItem>
    {
        public IReadOnlyList<IColumn<TItem>> Columns { get; }

        private MarkdownTable(List<IColumn<TItem>> columns)
        {
            Columns = columns;
        }

        public string ToString(IEnumerable<TItem> items)
        {
            var writer = new StringWriter();
            Write(writer, items);
            return writer.ToString();
        }

        public void Write(TextWriter textWriter, IEnumerable<TItem> items)
        {
            var rows = new List<IRow<TItem>>();
            foreach (var item in items)
            {
                rows.Add(new Row<TItem>(Columns, item));
            }
            var rowSet = new RowSet<TItem>(Columns, rows);

            this.WriteHeader(textWriter, rowSet);
            rowSet.Write(textWriter, this);
        }

        public static ITable<TItem> Build()
        {
            var config = new TableConfig<TItem>();
            config.GenerateColumns();
            return new MarkdownTable<TItem>(config.BuildColumns());
        }

        public static ITable<TItem> Build(Action<ITableConfig<TItem>> configure)
        {
            var config = new TableConfig<TItem>();
            configure(config);
            if (config.IsEnableGenerateColumns)
            {
                config.GenerateColumns();
            }
            return new MarkdownTable<TItem>(config.BuildColumns());
        }
    }
}
