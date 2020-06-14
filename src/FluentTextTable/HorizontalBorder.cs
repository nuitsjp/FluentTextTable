using System.Collections.Generic;
using System.IO;

namespace FluentTextTable
{
    public class HorizontalBorder
    {
        public char LeftStyle { get; }
        public char IntersectionStyle { get; }
        public char RightStyle { get; }
        public VerticalBorder LeftVerticalBorder { get; }
        public VerticalBorder InsideVerticalBorder { get; }
        public VerticalBorder RightVerticalBorder { get; }
        public bool IsEnable { get; }
        public char LineStyle { get; }

        internal HorizontalBorder(
            bool isEnable, 
            char lineStyle,
            char leftStyle,
            char intersectionStyle,
            char rightStyle,
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
    }
}