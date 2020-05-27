﻿using System;

namespace FluentTextTable
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class ColumnFormatAttribute : Attribute
    {
        public int Index { get; set; }
        public string Header { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Left;
        public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Top;
        public string Format { get; set; }
    }
}