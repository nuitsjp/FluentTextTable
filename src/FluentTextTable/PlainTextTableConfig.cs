using System.Collections.Generic;
using System.Linq;

namespace FluentTextTable
{
    public class PlainTextTableConfig<TItem> : TextTableConfigBase<TItem>, IPlainTextTableConfig<TItem>
    {
        private readonly BordersConfig _borders = new BordersConfig();

        public IBordersConfig Borders => _borders;

        private Borders BuildBorders() => _borders.Build();

        internal ITextTable<TItem> Build()
            => TextTable<TItem>.NewPlainTextTable(Padding, BuildHeader(), BuildBorders());
    }
}