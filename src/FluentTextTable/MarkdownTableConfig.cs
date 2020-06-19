using System.Collections.Generic;
using System.Linq;

namespace FluentTextTable
{
    public class MarkdownTableConfig<TItem> : TextTableConfig<TItem>
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

        public ITable<TItem> Build()
        {
            return new Table<TItem>(Padding, BuildHeader(), MarkdownTableBorders, ToStrings);
        }

        private static IEnumerable<string> ToStrings(IEnumerable<object> objects, string format)
        {
            yield return string.Join("<br>", objects.Select(x => x.ToString(format)));
        }
        
    }
}