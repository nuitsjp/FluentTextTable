using System;
using System.Collections.Generic;

namespace FluentTextTable
{
    public class MarkdownTableBuilder<TItem> : TextTableBuilderBase<TItem>, IMarkdownTableBuilder<TItem>
    {
        public override IBordersBuilder<TItem> Borders => throw new NotSupportedException();

        protected override IBorders BuildBorders() => FluentTextTable.Borders.MarkdownTableBorders;

        public MarkdownTableBuilder() : base(TextTable.CreateMarkdownCellLines)
        {
            Margins.As(0);
        }
    }
}