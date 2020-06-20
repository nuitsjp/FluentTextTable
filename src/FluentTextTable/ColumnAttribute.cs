using System;

namespace FluentTextTable
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class ColumnAttribute : Attribute
    {
        public int Index { get; set; }
        public string Header { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Default;
        public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Top;
        public string Format { get; set; }
    }
}