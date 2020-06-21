using System;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class HorizontalBorder
    {
        internal HorizontalBorder(
            bool isEnable, 
            string lineStyle,
            string leftStyle,
            string intersectionStyle,
            string rightStyle,
            VerticalBorder leftVerticalBorder,
            VerticalBorder insideVerticalBorder,
            VerticalBorder rightVerticalBorder)
        {
            IsEnable = isEnable;
            LineStyle = lineStyle;
            LeftStyle = leftStyle;
            IntersectionStyle = intersectionStyle;
            RightStyle = rightStyle;
            LeftVerticalBorder = leftVerticalBorder;
            InsideVerticalBorder = insideVerticalBorder;
            RightVerticalBorder = rightVerticalBorder;
        }

        protected HorizontalBorder(string lineStyle)
        {
            LineStyle = lineStyle;
        }

        private string LeftStyle { get; }
        private string IntersectionStyle { get; }
        private string RightStyle { get; }
        private VerticalBorder LeftVerticalBorder { get; }
        private VerticalBorder InsideVerticalBorder { get; }
        private VerticalBorder RightVerticalBorder { get; }
        private bool IsEnable { get; }
        internal string LineStyle { get; }
        
        internal virtual void Write(TextWriter textWriter, ITextTableLayout textTableLayout)
        {
            if(!IsEnable) return;
            
            if(LeftVerticalBorder.IsEnable) textWriter.Write(LeftStyle);
            
            var items = textTableLayout
                .Columns
                .Select(column => string.Concat(Enumerable.Repeat(LineStyle, textTableLayout.GetWidthOf(column) / LineStyle.GetWidth())))
                .ToList();

            textWriter.Write(InsideVerticalBorder.IsEnable
                ? string.Join(IntersectionStyle, items)
                : string.Join(string.Empty, items));

            if(RightVerticalBorder.IsEnable) textWriter.Write(RightStyle);
            
            textWriter.WriteLine();
        }
    }
}