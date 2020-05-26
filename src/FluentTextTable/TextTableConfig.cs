using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentTextTable
{
    public class TextTableConfig<TItem> : ITextTableConfig<TItem>
    {
        private readonly List<Column<TItem>> _columns = new List<Column<TItem>>();

        internal IReadOnlyList<Column<TItem>> Columns => _columns;

        public Column<TItem> AddColumn(Expression<Func<TItem, object>> getMemberExpression)
        {
            var column = new Column<TItem>(getMemberExpression);
            _columns.Add(column);
            return column;
        }
    }
}