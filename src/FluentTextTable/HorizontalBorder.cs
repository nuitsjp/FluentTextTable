using System.Collections.Generic;
using System.IO;

namespace FluentTextTable
{
    internal class HorizontalBorder : BorderBase
    {
        internal char LeftStyle { get; }
        internal char IntersectionStyle { get; }
        internal char RightStyle { get; }
        private readonly VerticalBorder _leftVerticalBorder;
        private readonly VerticalBorder _insideVerticalBorder;
        private readonly VerticalBorder _rightVerticalBorder;

        internal HorizontalBorder(
            bool isEnable, 
            char lineStyle,
            char leftStyle,
            char intersectionStyle,
            char rightStyle,
            VerticalBorder leftVerticalBorder,
            VerticalBorder insideVerticalBorder,
            VerticalBorder rightVerticalBorder) 
            : base(isEnable, lineStyle)
        {
            LeftStyle = leftStyle;
            IntersectionStyle = intersectionStyle;
            RightStyle = rightStyle;
            _leftVerticalBorder = leftVerticalBorder;
            _insideVerticalBorder = insideVerticalBorder;
            _rightVerticalBorder = rightVerticalBorder;
        }

        internal void Write<TItem>(TextWriter textWriter, ITextTable<TItem> table, IEnumerable<Column<TItem>> columns)
        {
            if(!IsEnable) return;
            
            if(_leftVerticalBorder.IsEnable) textWriter.Write(LeftStyle);
            var items = new List<string>();
            foreach (var column in columns)
            {
                items.Add(new string(LineStyle, table.GetColumnWidth(column)));
            }

            textWriter.Write(_insideVerticalBorder.IsEnable
                ? string.Join(IntersectionStyle.ToString(), items)
                : string.Join(string.Empty, items));

            if(_rightVerticalBorder.IsEnable) textWriter.Write(RightStyle);
            
            textWriter.WriteLine();
        }

    }
}