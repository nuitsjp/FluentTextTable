using System.IO;

namespace FluentTextTable
{
    public interface IMargins
    {
        IMargin Left { get; }
        IMargin Right { get; }
    }
}