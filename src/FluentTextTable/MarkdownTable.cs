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
        private MarkdownTable(List<IColumn<TItem>> columns)
        {
            Columns = columns;
        }

        public IReadOnlyList<IColumn<TItem>> Columns { get; }

        public string ToString(IEnumerable<TItem> items)
        {
            var writer = new StringWriter();
            Write(writer, items);
            return writer.ToString();
        }

        public void Write(TextWriter textWriter, IEnumerable<TItem> items)
        {
            var rowSet = 
                new RowSet<TItem>(
                    Columns, 
                    items
                        .Select(item => new Row<TItem>(Columns, item))
                        .Cast<IRow<TItem>>()
                        .ToList());

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
