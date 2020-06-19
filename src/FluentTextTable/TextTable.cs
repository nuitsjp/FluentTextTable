using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class TextTable<TItem> : Table<TItem>, ITextTable<TItem>
    {
        internal TextTable(int padding, List<IColumn<TItem>> columns, Borders borders)
            : base(padding, columns, borders, ToStrings)
        {
        }

        private static IEnumerable<string> ToStrings(IEnumerable<object> objects, string format)
        {
            return objects.Select(x => x.ToString(format));
        }

        public static TextTable<TItem> Build()
            => Build(x => x.EnableGenerateColumns());
        
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