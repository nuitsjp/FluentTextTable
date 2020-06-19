using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class MarkdownTable<TItem> : Table<TItem>
    {
        private static readonly VerticalBorder MarkdownVerticalBorder = new VerticalBorder(true, '|');
        private static readonly HorizontalBorder DisableHorizontalBorder =
            new HorizontalBorder(false, '-', '-', '|', '-', MarkdownVerticalBorder, MarkdownVerticalBorder, MarkdownVerticalBorder);

        private static readonly Borders MarkdownTableBorders = new Borders(
            DisableHorizontalBorder,
            new MarkdownHeaderHorizontalBorder(),
            DisableHorizontalBorder,
            DisableHorizontalBorder,
            MarkdownVerticalBorder,
            MarkdownVerticalBorder,
            MarkdownVerticalBorder);

        internal MarkdownTable(int padding, List<IColumn<TItem>> columns) : base(padding, columns, MarkdownTableBorders, ToStrings)
        {
        }

        private static IEnumerable<string> ToStrings(IEnumerable<object> objects, string format)
        {
            yield return string.Join("<br>", objects.Select(x => x.ToString(format)));
        }
        
        public static ITable<TItem> Build()
            => Build(x => x.EnableGenerateColumns());

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
