using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentTextTable
{
    public class TextTableConfig<TItem> : ITextTableConfig<TItem>
    {
        internal List<Column> Columns { get; } = new List<Column>();
        internal Dictionary<Column, MemberAccessor<TItem>> MemberAccessors { get; } = new Dictionary<Column, MemberAccessor<TItem>>();

        public IColumn AddColumn(Expression<Func<TItem, object>> getMemberExpression)
        {
            var memberAccessor = new MemberAccessor<TItem>(getMemberExpression);
            var column = new Column(memberAccessor.Name);
            Columns.Add(column);
            MemberAccessors[column] = memberAccessor;

            return column;
        }

        public IColumn AddColumn(PropertyInfo propertyInfo)
        {
            var memberAccessor = new MemberAccessor<TItem>(propertyInfo);
            var column = new Column(memberAccessor.Name);
            Columns.Add(column);
            MemberAccessors[column] = memberAccessor;

            return column;
        }
    }
}