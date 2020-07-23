using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentTextTable
{
    public abstract class TextTableBuilderBase<TItem> : ITextTableBuilder<TItem>
    {
        private readonly Func<TItem, IColumn<TItem>, IEnumerable<ICellLine>> _createCellLines;
        private readonly MarginsBuilder<TItem> _margins;
        private readonly PaddingsBuilder<TItem> _paddings;
        private readonly ColumnsBuilder<TItem> _columns;


        protected TextTableBuilderBase(Func<TItem, IColumn<TItem>, IEnumerable<ICellLine>> createCellLines)
        {
            _createCellLines = createCellLines;
            _margins = new MarginsBuilder<TItem>(this);
            _paddings = new PaddingsBuilder<TItem>(this);
            _columns = new ColumnsBuilder<TItem>(this);
        }

        public IMarginsBuilder<TItem> Margins => _margins;

        public IPaddingsBuilder<TItem> Paddings => _paddings;
        public IColumnsBuilder<TItem> Columns => _columns;
        public abstract IBordersBuilder<TItem> Borders { get; }

        protected abstract IBorders BuildBorders();

        internal ITextTable<TItem> Build() =>
            new TextTable<TItem>(new Header(_columns.Build()), BuildBorders(), _margins.Build(), _paddings.Build(), _createCellLines);
    }
}