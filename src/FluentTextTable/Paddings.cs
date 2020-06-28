namespace FluentTextTable
{
    public class Paddings : IPaddings
    {
        public Paddings(IPadding left, IPadding right)
        {
            Left = left;
            Right = right;
        }

        public IPadding Left { get; }
        public IPadding Right { get; }
    }
}