using System.Collections.Generic;
using System.IO;

namespace FluentTextTable
{
    internal class HorizontalBorder : BorderBase
    {
        internal char LeftStyle { get; }
        internal char IntersectionStyle { get; }
        internal char RightStyle { get; }
        internal VerticalBorder LeftVerticalBorder { get; }
        internal VerticalBorder InsideVerticalBorder { get; }
        internal VerticalBorder RightVerticalBorder { get; }

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
            LeftVerticalBorder = leftVerticalBorder;
            InsideVerticalBorder = insideVerticalBorder;
            RightVerticalBorder = rightVerticalBorder;
        }
    }
}