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
            var members = new List<Member>();
            foreach (var memberInfo in memberInfos)
            {
                var columnFormat = memberInfo
                    .GetCustomAttributes(true)
                    .SingleOrDefault(x => x is ColumnAttribute) as ColumnAttribute;
                if (columnFormat is null)
                {
                    members.Add(new Member{Index = 0, MemberInfo = memberInfo, ColumnAttribute = null});
                }

                if (columnFormat != null)
                {
                    members.Add(new Member{Index = columnFormat.Index, MemberInfo = memberInfo, ColumnAttribute = columnFormat});
                }
            }

            foreach (var member in members.OrderBy(x => x.Index))
            {
                var memberAccessor = new MemberAccessor<TItem>(member.MemberInfo);
                var column = new ColumnBuilder<TItem>(this, memberAccessor);
                _columns.Add(column);

                if (member.ColumnAttribute == null) continue;

                if (member.ColumnAttribute.Header != null) column.NameAs(member.ColumnAttribute.Header);

                column
                    .HorizontalAlignmentAs(member.ColumnAttribute.HorizontalAlignment)
                    .VerticalAlignmentAs(member.ColumnAttribute.VerticalAlignment)
                    .FormatAs(member.ColumnAttribute.Format);
            }
        }

        private class Member
        {
            public int Index { get; set; }
            public MemberInfo MemberInfo { get; set; }
            public ColumnAttribute ColumnAttribute { get; set; }
        }
    }
}