using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class MarkdownTable<TItem> : Table<TItem>
    {
        internal MarkdownTable(int padding, List<IColumn<TItem>> columns) : base(padding, columns)
        {
        }

        public override void Write(TextWriter textWriter, IEnumerable<TItem> items)
        {
            var tableInstance = 
                new MarkdownTableInstance<TItem>(
                    this,
                    items
                        .Select(item => new Row<TItem>(Columns, Padding, item))
                        .Cast<IRow<TItem>>()
                        .ToList());
            tableInstance.Write(textWriter);
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
