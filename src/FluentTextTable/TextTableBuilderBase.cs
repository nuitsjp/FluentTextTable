using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentTextTable
{
    public abstract class TextTableBuilderBase<TItem> : ITextTableBuilder<TItem>
    {
        private int _padding  = TextTable.DefaultPadding;
        private readonly List<ColumnBuilder<TItem>> _columns  = new List<ColumnBuilder<TItem>>();
        private readonly Func<TItem, IColumn<TItem>, IEnumerable<ICellLine>> _createCellLines;


        protected TextTableBuilderBase(Func<TItem, IColumn<TItem>, IEnumerable<ICellLine>> createCellLines)
        {
            _createCellLines = createCellLines;
        }
        
        public ITextTableBuilder<TItem> PaddingAs(int padding)
        {
            _padding = padding;
            return this;
        }

        public IColumnBuilder<TItem> AddColumn(Expression<Func<TItem, object>> getMemberExpression)
        {
            var memberAccessor = new MemberAccessor<TItem>(getMemberExpression);
            var column = new ColumnBuilder<TItem>(this, memberAccessor);
            _columns.Add(column);

            return column;
        }

        private IColumnBuilder<TItem> AddColumn(MemberInfo memberInfo)
        {
            var memberAccessor = new MemberAccessor<TItem>(memberInfo);
            var column = new ColumnBuilder<TItem>(this, memberAccessor);
            _columns.Add(column);

            return column;
        }

        protected abstract IBorders BuildBorders();

        internal ITextTable<TItem> Build()
        {
            if (!_columns.Any()) GenerateColumns();
            return new TextTable<TItem>(BuildHeader(), BuildBorders(), _padding, _createCellLines);
        }


        private  void GenerateColumns()
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
                
                if (columnFormat.Header != null) column.NameAs(columnFormat.Header);
                
                column
                    .HorizontalAlignmentAs(columnFormat.HorizontalAlignment)
                    .VerticalAlignmentAs(columnFormat.VerticalAlignment)
                    .FormatAs(columnFormat.Format);
            }
        }
        
        internal IHeader BuildHeader() => new Header(_columns.Select(x => x.Build()));
    }
}