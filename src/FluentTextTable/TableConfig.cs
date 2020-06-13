using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentTextTable
{
    public class TableConfig<TItem> : ITableConfig<TItem>
    {
        private readonly List<ColumnConfig<TItem>> _columns  = new List<ColumnConfig<TItem>>();

        public bool AutoGenerateColumns { get; set; } = false;

        public IColumnConfig AddColumn(Expression<Func<TItem, object>> getMemberExpression)
        {
            var memberAccessor = new MemberAccessor<TItem>(getMemberExpression);
            var column = new ColumnConfig<TItem>(memberAccessor);
            _columns.Add(column);

            return column;
        }

        internal IColumnConfig AddColumn(MemberInfo memberInfo)
        {
            var memberAccessor = new MemberAccessor<TItem>(memberInfo);
            var column = new ColumnConfig<TItem>(memberAccessor);
            _columns.Add(column);

            return column;
        }

        internal List<Column<TItem>> FixColumnSpecs() => _columns.Select(x => x.Build()).ToList();
    }
}