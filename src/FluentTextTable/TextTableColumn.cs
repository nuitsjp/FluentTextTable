using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using EastAsianWidthDotNet;

namespace FluentTextTable
{
    public class TextTableColumn<TItem> : ITextTableColumn
    {
        
        internal TextTableColumn(Expression<Func<TItem, object>> getValue)
        {
            GetValue = getValue.Compile();
            HeaderIs(GetMemberInfo(getValue).Name);
        }

        internal string Header { get; private set; }

        internal int HeaderWidth { get; private set; }

        internal HorizontalAlignment HorizontalAlignment { get; private set; } = HorizontalAlignment.Left;
        public VerticalAlignment VerticalAlignment { get; private set; } = VerticalAlignment.Top;

        public string Format { get; private set; }

        private Func<TItem, object> GetValue { get; }

        internal int Width { get; private set; }

        public ITextTableColumn HeaderIs(string header)
        {
            Header = header;
            HeaderWidth = header.GetWidth() + 2;
            return this;
        }

        public ITextTableColumn AlignHorizontalTo(HorizontalAlignment horizontalAlignment)
        {
            HorizontalAlignment = horizontalAlignment;
            return this;
        }

        public ITextTableColumn AlignVerticalTo(VerticalAlignment verticalAlignment)
        {
            VerticalAlignment = verticalAlignment;
            return this;
        }

        public ITextTableColumn FormatTo(string format)
        {
            Format = "{0:" + format + "}";
            return this;
        }

        internal TextTableCell ToCell(TItem item)
        {
            return new TextTableCell(
                this, 
                Format is null 
                    ? GetValue(item).ToString()
                    : string.Format(Format, GetValue(item)));
        }

        internal void UpdateWidth(IEnumerable<TextTableRow> rows)
        {
            Width = Math.Max(HeaderWidth, rows.Select(x => x.Cells[this].Width).Max());
        }

        private static MemberInfo GetMemberInfo(LambdaExpression lambda)
        {
            Expression expr = lambda;
            while (true)
            {
                switch (expr.NodeType)
                {
                    case ExpressionType.Lambda:
                        expr = ((LambdaExpression)expr).Body;
                        break;

                    case ExpressionType.Convert:
                        expr = ((UnaryExpression)expr).Operand;
                        break;

                    case ExpressionType.MemberAccess:
                        var memberExpression = (MemberExpression)expr;
                        var member = memberExpression.Member;
                        Type paramType;

                        while (memberExpression != null)
                        {
                            paramType = memberExpression.Type;

                            // Find the member on the base type of the member type
                            // E.g. EmailAddress.Value
                            var baseMember = paramType.GetMembers().FirstOrDefault(m => m.Name == member.Name);
                            if (baseMember != null)
                            {
                                // Don't use the base type if it's just the nullable type of the derived type
                                // or when the same member exists on a different type
                                // E.g. Nullable<decimal> -> decimal
                                // or:  SomeType { string Length; } -> string.Length
                                if (baseMember is PropertyInfo baseProperty && member is PropertyInfo property)
                                {
                                    if (baseProperty.DeclaringType == property.DeclaringType &&
                                        baseProperty.PropertyType != Nullable.GetUnderlyingType(property.PropertyType))
                                    {
                                        return baseMember;
                                    }
                                }
                                else
                                {
                                    return baseMember;
                                }
                            }

                            memberExpression = memberExpression.Expression as MemberExpression;
                        }

                        // Make sure we get the property from the derived type.
                        paramType = lambda.Parameters[0].Type;
                        return paramType.GetMember(member.Name)[0];

                    default:
                        return null;
                }
            }
        }
    }
}