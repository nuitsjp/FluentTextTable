using System.IO;

namespace FluentTextTable
{
    public interface IVerticalBorder
    {
        bool IsEnable { get; }
        void Write(TextWriter textWriter);
    }
}