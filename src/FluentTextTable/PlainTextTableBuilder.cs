using System;
using System.Collections.Generic;

namespace FluentTextTable
{
    public class PlainTextTableBuilder<TItem> : TextTableBuilderBase<TItem>, IPlainTextTableBuilder<TItem>
    {
        private readonly BordersBuilder<TItem> _borders;

        public override IBordersBuilder<TItem> Borders => _borders;

        public PlainTextTableBuilder() : base(TextTable.CreatePlainTextCellLines)
        {
            _borders = new BordersBuilder<TItem>(this);
        }
        
        protected override IBorders BuildBorders() => _borders.Build();
    }
}