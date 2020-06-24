using System;
using Xunit;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace FluentTextTable.Test.Borders
{
    public class RightTest
    {
        [Fact]
        public void WhenDisable()
        {

            var table = Build.TextTable<User>(builder =>
            {
                builder.Borders.Right.AsDisable();
            });
            var text = table.ToString(new[]
            {
                new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
            });

            Assert.Equal(
                @"
+----+-------------+---------+-------------+--------------------
| Id | Name        | Parents | Occupations | Birthday           
+----+-------------+---------+-------------+--------------------
| 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 
+----+-------------+---------+-------------+--------------------
| 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 
+----+-------------+---------+-------------+--------------------
", $"{Environment.NewLine}{text}");
        }
            
        [Fact]
        public void WhenChangeDecorations()
        {

            var table = Build.TextTable<User>(builder =>
            {
                builder.Borders.Right.LeftStyleAs("\\\\");
                builder.Borders.Top.RightStyleAs("12");
                builder.Borders.HeaderHorizontal.RightStyleAs("34");
                builder.Borders.InsideHorizontal.RightStyleAs("56");
                builder.Borders.Bottom.RightStyleAs("78");
            });
            var text = table.ToString(new[]
            {
                new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
            });

            Assert.Equal(
                @"
+----+-------------+---------+-------------+--------------------12
| Id | Name        | Parents | Occupations | Birthday           \\
+----+-------------+---------+-------------+--------------------34
| 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 \\
+----+-------------+---------+-------------+--------------------56
| 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 \\
+----+-------------+---------+-------------+--------------------78
", $"{Environment.NewLine}{text}");
        }
        
        [Fact]
        public void WhenVerticalLineStyleWidthIsUnmatched()
        {
            Assert.Throws<InvalidOperationException>(() => 
                Build.TextTable<User>(builder =>
                {
                    builder.Borders.Right.LeftStyleAs("12");
                }));
        }

        [Fact]
        public void WhenTopRightWidthIsUnmatched()
        {
            Assert.Throws<InvalidOperationException>(() => 
                Build.TextTable<User>(builder =>
                {
                    builder.Borders.Top.RightStyleAs("12");
                }));
        }
            
        [Fact]
        public void WhenHeaderHorizontalRightWidthIsUnmatched()
        {
            Assert.Throws<InvalidOperationException>(() => 
                Build.TextTable<User>(builder =>
                {
                    builder.Borders.HeaderHorizontal.RightStyleAs("12");
                }));
        }
            
        [Fact]
        public void WhenInsideHorizontalRightWidthIsUnmatched()
        {
            Assert.Throws<InvalidOperationException>(() => 
                Build.TextTable<User>(builder =>
                {
                    builder.Borders.InsideHorizontal.RightStyleAs("12");
                }));
        }
            
        [Fact]
        public void WhenBottomRightWidthIsUnmatched()
        {
            Assert.Throws<InvalidOperationException>(() => 
                Build.TextTable<User>(builder =>
                {
                    builder.Borders.Bottom.RightStyleAs("12");
                }));
        }
        
        private class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime Birthday;
            public string Parents { get; set; }
            public string[] Occupations { get; set; }

        }
    }
}