using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentTextTable
{
    public abstract class TextTableConfigBase<TItem> : ITextTableConfig<TItem>
    {
        private const int DefaultPadding = 1;
        
        private readonly List<ColumnConfig<TItem>> _columns  = new List<ColumnConfig<TItem>>();

        internal int Padding { get; private set; } = DefaultPadding;

        internal bool IsEnableGenerateColumns { get; private set; }

        public ITextTableConfig<TItem> HasPadding(int padding)
        {
            Padding = padding;
            return this;
        }

        public ITextTableConfig<TItem> EnableAutoGenerateColumns( )
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
                var column = AddColumn(memberInfo);
                if (columnFormat == null) continue;
                
                if (columnFormat.Header != null) column.HasName(columnFormat.Header);
                
                column
                    .AlignHorizontal(columnFormat.HorizontalAlignment)
                    .AlignVertical(columnFormat.VerticalAlignment)
                    .HasFormat(columnFormat.Format);
            }
        }
        
        internal IHeader BuildHeader() => new Header(_columns.Select(x => x.Build()));
    }
}