using System;
using Xunit;

namespace FluentTextTable.Test.Borders
{
    public class LeftTest
    {
        [Fact]
        public void WhenDisable()
        {

            var table = TextTable.Build<User>(config =>
            {
                config.EnableAutoGenerateColumns();
                config.Borders.Left.Disable();
            });
            var text = table.ToString(new[]
            {
                new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
            });

            Assert.Equal(
                @"
----+-------------+---------+-------------+--------------------+
 Id | Name        | Parents | Occupations | Birthday           |
----+-------------+---------+-------------+--------------------+
 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 |
----+-------------+---------+-------------+--------------------+
 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 |
----+-------------+---------+-------------+--------------------+
", $"{Environment.NewLine}{text}");
        }
            
        [Fact]
        public void WhenChangeDecorations()
        {

            var table = TextTable.Build<User>(config =>
            {
                config.EnableAutoGenerateColumns();
                config.Borders.Left.LineIs("\\\\");
                config.Borders.Top.LeftEndIs("12");
                config.Borders.HeaderHorizontal.LeftEndIs("34");
                config.Borders.InsideHorizontal.LeftEndIs("56");
                config.Borders.Bottom.LeftEndIs("78");
            });
            var text = table.ToString(new[]
            {
                new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
            });

            Assert.Equal(
                @"
12----+-------------+---------+-------------+--------------------+
\\ Id | Name        | Parents | Occupations | Birthday           |
34----+-------------+---------+-------------+--------------------+
\\ 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 |
56----+-------------+---------+-------------+--------------------+
\\ 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 |
78----+-------------+---------+-------------+--------------------+
", $"{Environment.NewLine}{text}");
        }
        
        [Fact]
        public void WhenVerticalLineStyleWidthIsUnmatched()
        {
            Assert.Throws<InvalidOperationException>(() => 
                TextTable.Build<User>(config =>
                {
                    config.EnableAutoGenerateColumns();
                    config.Borders.Left.LineIs("12");
                }));
        }

        [Fact]
        public void WhenTopLeftWidthIsUnmatched()
        {
            Assert.Throws<InvalidOperationException>(() => 
                TextTable.Build<User>(config =>
                {
                    config.EnableAutoGenerateColumns();
                    config.Borders.Top.LeftEndIs("12");
                }));
        }
            
        [Fact]
        public void WhenHeaderHorizontalLeftWidthIsUnmatched()
        {
            Assert.Throws<InvalidOperationException>(() => 
                TextTable.Build<User>(config =>
                {
                    config.EnableAutoGenerateColumns();
                    config.Borders.HeaderHorizontal.LeftEndIs("12");
                }));
        }
            
        [Fact]
        public void WhenInsideHorizontalLeftWidthIsUnmatched()
        {
            Assert.Throws<InvalidOperationException>(() => 
                TextTable.Build<User>(config =>
                {
                    config.EnableAutoGenerateColumns();
                    config.Borders.InsideHorizontal.LeftEndIs("12");
                }));
        }
            
        [Fact]
        public void WhenBottomLeftWidthIsUnmatched()
        {
            Assert.Throws<InvalidOperationException>(() => 
                TextTable.Build<User>(config =>
                {
                    config.EnableAutoGenerateColumns();
                    config.Borders.Bottom.LeftEndIs("12");
                }));
        }
    }
}