using System;
using System.Collections.Generic;

namespace FluentTextTable
{
    public class MarkdownTableBuilder<TItem> : TextTableBuilderBase<TItem>, IMarkdownTableBuilder<TItem>
    {

        protected override IBorders BuildBorders() => Borders.MarkdownTableBorders;

        public MarkdownTableBuilder() : base(TextTable.CreateMarkdownCellLines)
        {
            Margins.As(0);
        }
    }
}