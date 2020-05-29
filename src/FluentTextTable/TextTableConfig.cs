using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentTextTable
{
    public class TextTableConfig<TItem> : ITextTableConfig<TItem>
    {
        internal List<Column> Columns { get; } = new List<Column>();
        internal Dictionary<Column, MemberAccessor<TItem>> MemberAccessors { get; } = new Dictionary<Column, MemberAccessor<TItem>>();

        public bool AutoGenerateColumns { get; set; } = false;
        
        public IBorderConfig TopBorder { get; } = new BorderConfig();

        public IColumn AddColumn(Expression<Func<TItem, object>> getMemberExpression)
        {
            var memberAccessor = new MemberAccessor<TItem>(getMemberExpression);
            var column = new Column(memberAccessor.Name);
            Columns.Add(column);
            MemberAccessors[column] = memberAccessor;

            return column;
        }

        public IColumn AddColumn(MemberInfo memberInfo)
        {
            var memberAccessor = new MemberAccessor<TItem>(memberInfo);
            var column = new Column(memberAccessor.Name);
            Columns.Add(column);
            MemberAccessors[column] = memberAccessor;

            return column;
        }
    }
}