using System;
using Xunit;

namespace FluentTextTable.Test.Borders
{
    public class InsideVerticalTest
    {
        [Fact]
        public void WhenDisable()
        {

            var table = TextTable.Build<User>(config =>
            {
                config.Borders.InsideVertical.Disable();
            });
            var text = table.ToString(new[]
            {
                new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
            });

            Assert.Equal(
                @"
+-----------------------------------------------------------+
| Id  Name         Parents  Occupations  Birthday           |
+-----------------------------------------------------------+
| 1   ビル ゲイツ                        1955/10/28 0:00:00 |
+-----------------------------------------------------------+
| 2   Steven Jobs                        1955/02/24 0:00:00 |
+-----------------------------------------------------------+
", $"{Environment.NewLine}{text}");
        }
            
        [Fact]
        public void WhenChangeDecorations()
        {

            var table = TextTable.Build<User>(config =>
            {
                config.Borders.InsideVertical.LineIs("\\\\");
                config.Borders.Top.IntersectionIs("12");
                config.Borders.HeaderHorizontal.IntersectionIs("34");
                config.Borders.InsideHorizontal.IntersectionIs("56");
                config.Borders.Bottom.IntersectionIs("78");
            });
            var text = table.ToString(new[]
            {
                new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
            });

            Assert.Equal(
                @"
+----12-------------12---------12-------------12--------------------+
| Id \\ Name        \\ Parents \\ Occupations \\ Birthday           |
+----34-------------34---------34-------------34--------------------+
| 1  \\ ビル ゲイツ \\         \\             \\ 1955/10/28 0:00:00 |
+----56-------------56---------56-------------56--------------------+
| 2  \\ Steven Jobs \\         \\             \\ 1955/02/24 0:00:00 |
+----78-------------78---------78-------------78--------------------+
", $"{Environment.NewLine}{text}");
        }
        
        [Fact]
        public void WhenVerticalLineStyleWidthIsUnmatched()
        {
            Assert.Throws<InvalidOperationException>(() => 
                TextTable.Build<User>(config =>
                {
                    config.Borders.InsideVertical.LineIs("12");
                }));
        }

        [Fact]
        public void WhenTopInsideVerticalWidthIsUnmatched()
        {
            Assert.Throws<InvalidOperationException>(() => 
                TextTable.Build<User>(config =>
                {
                    config.Borders.Top.IntersectionIs("12");
                }));
        }
            
        [Fact]
        public void WhenHeaderHorizontalInsideVerticalWidthIsUnmatched()
        {
            Assert.Throws<InvalidOperationException>(() => 
                TextTable.Build<User>(config =>
                {
                    config.Borders.HeaderHorizontal.IntersectionIs("12");
                }));
        }
            
        [Fact]
        public void WhenInsideHorizontalInsideVerticalWidthIsUnmatched()
        {
            Assert.Throws<InvalidOperationException>(() => 
                TextTable.Build<User>(config =>
                {
                    config.Borders.InsideHorizontal.IntersectionIs("12");
                }));
        }
            
        [Fact]
        public void WhenBottomInsideVerticalWidthIsUnmatched()
        {
            Assert.Throws<InvalidOperationException>(() => 
                TextTable.Build<User>(config =>
                {
                    config.Borders.Bottom.IntersectionIs("12");
                }));
        }
    }
}