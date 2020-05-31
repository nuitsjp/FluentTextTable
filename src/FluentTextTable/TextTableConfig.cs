using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentTextTable
{
    public class TextTableConfig<TItem> : ITextTableConfig<TItem>
    {
        private readonly BordersConfig _borders = new BordersConfig();
        internal List<ColumnConfig> Columns { get; } = new List<ColumnConfig>();
        internal Dictionary<ColumnConfig, MemberAccessor<TItem>> MemberAccessors { get; } = new Dictionary<ColumnConfig, MemberAccessor<TItem>>();

        public bool AutoGenerateColumns { get; set; } = false;

        public IBordersConfig Borders => _borders;

        public IColumnConfig AddColumn(Expression<Func<TItem, object>> getMemberExpression)
        {
            var memberAccessor = new MemberAccessor<TItem>(getMemberExpression);
            var column = new ColumnConfig(memberAccessor.Name);
            Columns.Add(column);
            MemberAccessors[column] = memberAccessor;

            return column;
        }

        public IColumnConfig AddColumn(MemberInfo memberInfo)
        {
            var memberAccessor = new MemberAccessor<TItem>(memberInfo);
            var column = new ColumnConfig(memberAccessor.Name);
            Columns.Add(column);
            MemberAccessors[column] = memberAccessor;

            return column;
        }

        internal Borders BuildBorders() => _borders.Build();

    }
}