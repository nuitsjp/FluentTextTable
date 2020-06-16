using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class TextTable<TItem> : Table<TItem>, ITextTable<TItem>
    {
        internal TextTable(int padding, List<IColumn<TItem>> columns, Borders borders)
            : base(padding, columns)
        {
            Borders = borders;
        }

        internal Borders Borders { get; }

        public override void Write(TextWriter writer, IEnumerable<TItem> items)
        {
            var tableInstance = 
                new TextTableInstance<TItem>(
                    this,
                    items
                        .Select(item => new Row<TItem>(Columns, Padding, item))
                        .Cast<IRow<TItem>>()
                        .ToList());
            tableInstance.Write(writer);
        }
        
        public static TextTable<TItem> Build()
        {
            var config = new TextTableConfig<TItem>();
            config.GenerateColumns();
            return config.Build();
        }

        public static TextTable<TItem> Build(Action<ITextTableConfig<TItem>> configure)
        {
            var config = new TextTableConfig<TItem>();
            configure(config);
            if (config.IsEnableGenerateColumns)
            {
                config.GenerateColumns();
            }
            return config.Build();
        }
    }
}