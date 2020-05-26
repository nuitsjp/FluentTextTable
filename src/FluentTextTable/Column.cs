using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using EastAsianWidthDotNet;

namespace FluentTextTable
{
    public class Column<TItem> : IColumn
    {
        
        internal Column(Expression<Func<TItem, object>> getValue)
        {
            GetValue = getValue.Compile();
            HeaderIs(GetMemberInfo(getValue).Name);
        }

        internal string Header { get; private set; }

        internal int HeaderWidth { get; private set; }

        internal HorizontalAlignment HorizontalAlignment { get; private set; } = HorizontalAlignment.Left;
        internal VerticalAlignment VerticalAlignment { get; private set; } = VerticalAlignment.Top;

        internal string Format { get; private set; }

        private Func<TItem, object> GetValue { get; }

        internal int Width { get; private set; }

        public IColumn HeaderIs(string header)
        {
            Header = header;
            HeaderWidth = header.GetWidth() + 2;
            return this;
        }

        public IColumn AlignHorizontalTo(HorizontalAlignment horizontalAlignment)
        {
            HorizontalAlignment = horizontalAlignment;
            return this;
        }

        public IColumn AlignVerticalTo(VerticalAlignment verticalAlignment)
        {
            VerticalAlignment = verticalAlignment;
            return this;
        }

        public IColumn FormatTo(string format)
        {
            Format = format;
            return this;
        }

        internal Cell<TItem> ToCell(TItem item)
        {
            return new Cell<TItem>(GetValue(item), Format);
        }

        internal void UpdateWidth(IEnumerable<Row<TItem>> rows)
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