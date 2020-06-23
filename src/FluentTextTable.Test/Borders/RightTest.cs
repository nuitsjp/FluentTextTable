using System;
using Xunit;

namespace FluentTextTable.Test.Borders
{
    public class RightTest
    {
        [Fact]
        public void WhenDisable()
        {

            var table = TextTable.Build<User>(config =>
            {
                config.Borders.Right.Disable();
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

            var table = TextTable.Build<User>(config =>
            {
                config.Borders.Right.LineIs("\\\\");
                config.Borders.Top.RightEndIs("12");
                config.Borders.HeaderHorizontal.RightEndIs("34");
                config.Borders.InsideHorizontal.RightEndIs("56");
                config.Borders.Bottom.RightEndIs("78");
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
                TextTable.Build<User>(config =>
                {
                    config.Borders.Right.LineIs("12");
                }));
        }

        [Fact]
        public void WhenTopRightWidthIsUnmatched()
        {
            Assert.Throws<InvalidOperationException>(() => 
                TextTable.Build<User>(config =>
                {
                    config.Borders.Top.RightEndIs("12");
                }));
        }
            
        [Fact]
        public void WhenHeaderHorizontalRightWidthIsUnmatched()
        {
            Assert.Throws<InvalidOperationException>(() => 
                TextTable.Build<User>(config =>
                {
                    config.Borders.HeaderHorizontal.RightEndIs("12");
                }));
        }
            
        [Fact]
        public void WhenInsideHorizontalRightWidthIsUnmatched()
        {
            Assert.Throws<InvalidOperationException>(() => 
                TextTable.Build<User>(config =>
                {
                    config.Borders.InsideHorizontal.RightEndIs("12");
                }));
        }
            
        [Fact]
        public void WhenBottomRightWidthIsUnmatched()
        {
            Assert.Throws<InvalidOperationException>(() => 
                TextTable.Build<User>(config =>
                {
                    config.Borders.Bottom.RightEndIs("12");
                }));
        }
    }
}