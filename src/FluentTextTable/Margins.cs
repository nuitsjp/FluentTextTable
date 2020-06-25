using System.IO;

namespace FluentTextTable
{
    public class Margins : IMargins
    {
        public Margins(IMargin left, IMargin right)
        {
            Left = left;
            Right = right;
        }

        public IMargin Left { get; }
        public IMargin Right { get; }
    }
}