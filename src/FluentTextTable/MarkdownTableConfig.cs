using System.Collections.Generic;
using System.Linq;

namespace FluentTextTable
{
    public class MarkdownTableConfig<TItem> : TextTableConfigBase<TItem>, IMarkdownTableConfig<TItem>
    {

        public ITextTable<TItem> Build()
            => TextTable<TItem>.NewMarkdownTable(Padding, BuildHeader(), Borders.MarkdownTableBorders);
    }
}