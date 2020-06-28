using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentTextTable
{
    public class ColumnsBuilder<TItem> : CompositeTextTableBuilder<TItem>, IColumnsBuilder<TItem>
    {
        private readonly List<ColumnBuilder<TItem>> _columns = new List<ColumnBuilder<TItem>>();

        public ColumnsBuilder(ITextTableBuilder<TItem> textTableBuilder) : base(textTableBuilder)
        {
        }

        public IColumnBuilder<TItem> Add(Expression<Func<TItem, object>> expression)
        {
            var memberAccessor = new MemberAccessor<TItem>(expression);
            var column = new ColumnBuilder<TItem>(this, memberAccessor);
            _columns.Add(column);

            return column;
        }

        internal IEnumerable<IColumn<TItem>> Build()
        {
            if(!_columns.Any()) GenerateColumns();

            return _columns.Select(x => x.Build());
        }

        private void GenerateColumns()
        {
            var memberInfos =
                typeof(TItem).GetMembers(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => x.MemberType == MemberTypes.Field || x.MemberType == MemberTypes.Property);
            var members = new List<(int index, MemberInfo memberInfo, ColumnAttribute columnFormat)>();
            foreach (var memberInfo in memberInfos)
            {
                var columnFormat = memberInfo.GetCustomAttribute<ColumnAttribute>();
                if (columnFormat is null)
                {
                    members.Add((0, memberInfo, null));
                }

                if (columnFormat != null)
                {
                    members.Add((columnFormat.Index, memberInfo, columnFormat));
                }
            }

            foreach (var (_, memberInfo, columnFormat) in members.OrderBy(x => x.index))
            {
                var memberAccessor = new MemberAccessor<TItem>(memberInfo);
                var column = new ColumnBuilder<TItem>(this, memberAccessor);
                _columns.Add(column);

                if (columnFormat == null) continue;

                if (columnFormat.Header != null) column.NameAs(columnFormat.Header);

                column
                    .HorizontalAlignmentAs(columnFormat.HorizontalAlignment)
                    .VerticalAlignmentAs(columnFormat.VerticalAlignment)
                    .FormatAs(columnFormat.Format);
            }
        }

    }
}