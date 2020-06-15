using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentTextTable
{
    public abstract class TableConfig<TItem> : ITableConfig<TItem>
    {
        private const int DefaultPadding = 1;
        
        private readonly List<ColumnConfig<TItem>> _columns  = new List<ColumnConfig<TItem>>();

        internal int Padding { get; private set; } = DefaultPadding;

        internal bool IsEnableGenerateColumns { get; private set; }

        public ITableConfig<TItem> HasPadding(int padding)
        {
            Padding = padding;
            return this;
        }

        public ITableConfig<TItem> EnableGenerateColumns( )
        {
            IsEnableGenerateColumns = true;
            return this;
        }

        public IColumnConfig AddColumn(Expression<Func<TItem, object>> getMemberExpression)
        {
            var memberAccessor = new MemberAccessor<TItem>(getMemberExpression);
            var column = new ColumnConfig<TItem>(memberAccessor);
            _columns.Add(column);

            return column;
        }

        private IColumnConfig AddColumn(MemberInfo memberInfo)
        {
            var memberAccessor = new MemberAccessor<TItem>(memberInfo);
            var column = new ColumnConfig<TItem>(memberAccessor);
            _columns.Add(column);

            return column;
        }

        internal  void GenerateColumns()
        {
            var memberInfos =
                typeof(TItem).GetMembers(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => x.MemberType == MemberTypes.Field || x.MemberType == MemberTypes.Property);
            var members = new List<(int index, MemberInfo memberInfo, ColumnFormatAttribute columnFormat)>();
            foreach (var memberInfo in memberInfos)
            {
                var columnFormat = memberInfo.GetCustomAttribute<ColumnFormatAttribute>();
                if (columnFormat is null)
                {
                    members.Add((0, memberInfo, null));
                }

                if (columnFormat != null)
                {
                    members.Add((columnFormat.Index, memberInfo, columnFormat));
                }
            }

            foreach (var member in members.OrderBy(x => x.index))
            {
                var column = AddColumn(member.memberInfo);
                if (member.columnFormat != null)
                {
                    if (member.columnFormat.Header != null) column.HasName(member.columnFormat.Header);
                    column
                        .AlignHorizontal(member.columnFormat.HorizontalAlignment)
                        .AlignVertical(member.columnFormat.VerticalAlignment)
                        .HasFormat(member.columnFormat.Format);
                }
            }
        }
        
        internal List<IColumn<TItem>> BuildColumns() => _columns.Select(x => x.Build()).ToList();
    }
}