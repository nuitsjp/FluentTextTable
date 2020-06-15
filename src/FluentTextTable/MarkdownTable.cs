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
        internal MarkdownTable(int padding, List<IColumn<TItem>> columns)
        {
            Padding = padding;
            Columns = columns;
        }

        public int Padding { get; }
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
                    this, 
                    items
                        .Select(item => new Row<TItem>(this, item))
                        .Cast<IRow<TItem>>()
                        .ToList());

            this.WriteHeader(textWriter, rowSet);
            rowSet.Write(textWriter, this);
        }

        public static ITable<TItem> Build()
        {
            var config = new MarkdownTableConfig<TItem>();
            config.GenerateColumns();
            return config.Build();
        }

        public static ITable<TItem> Build(Action<ITableConfig<TItem>> configure)
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
}
