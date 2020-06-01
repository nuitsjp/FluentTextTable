using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentTextTable
{
    public class TextTableConfig<TItem> : ITextTableConfig<TItem>
    {
        private readonly BordersConfig _borders = new BordersConfig();
        internal List<ColumnConfig<TItem>> Columns { get; } = new List<ColumnConfig<TItem>>();

        public bool AutoGenerateColumns { get; set; } = false;

        public IBordersConfig Borders => _borders;

        public IColumnConfig AddColumn(Expression<Func<TItem, object>> getMemberExpression)
        {
            var memberAccessor = new MemberAccessor<TItem>(getMemberExpression);
            var column = new ColumnConfig<TItem>(memberAccessor);
            Columns.Add(column);

            return column;
        }

        public IColumnConfig AddColumn(MemberInfo memberInfo)
        {
            var memberAccessor = new MemberAccessor<TItem>(memberInfo);
            var column = new ColumnConfig<TItem>(memberAccessor);
            Columns.Add(column);

            return column;
        }

        internal List<Column<TItem>> FixColumnSpecs() => Columns.Select(x => x.Build()).ToList();
        internal Borders BuildBorders() => _borders.Build();

    }
}