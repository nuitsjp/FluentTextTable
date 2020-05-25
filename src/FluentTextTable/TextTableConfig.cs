using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentTextTable
{
    public class TextTableConfig<TItem> : ITextTableConfig<TItem>
    {
        private readonly List<TextTableColumn<TItem>> _columns = new List<TextTableColumn<TItem>>();

        internal IReadOnlyList<TextTableColumn<TItem>> Columns => _columns;

        public TextTableColumn<TItem> AddColumn(Expression<Func<TItem, object>> getMemberExpression)
        {
            var column = new TextTableColumn<TItem>(getMemberExpression);
            _columns.Add(column);
            return column;
        }
    }
}