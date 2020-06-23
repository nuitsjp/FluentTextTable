using System;
using Xunit;

namespace FluentTextTable.Test.Borders
{
    public class BottomTest
    {
        [Fact]
        public void WhenDisable()
        {

            var table = TextTable.Build<User>(config =>
            {
                config.Borders.Bottom.Disable();
            });
            var text = table.ToString(new[]
            {
                new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
            });

            Assert.Equal(
                @"
+----+-------------+---------+-------------+--------------------+
| Id | Name        | Parents | Occupations | Birthday           |
+----+-------------+---------+-------------+--------------------+
| 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 |
+----+-------------+---------+-------------+--------------------+
| 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 |
", $"{Environment.NewLine}{text}");
        }
            
        [Fact]
        public void WhenChangeDecorations()
        {

            var table = TextTable.Build<User>(config =>
            {
                config.Borders.Bottom
                    .LeftEndIs("#")
                    .LineIs("abc")
                    .IntersectionIs("$")
                    .RightEndIs("%");
            });
            var text = table.ToString(new[]
            {
                new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
            });

            Assert.Equal(
                @"
+------+---------------+---------+---------------+---------------------+
| Id   | Name          | Parents | Occupations   | Birthday            |
+------+---------------+---------+---------------+---------------------+
| 1    | ビル ゲイツ   |         |               | 1955/10/28 0:00:00  |
+------+---------------+---------+---------------+---------------------+
| 2    | Steven Jobs   |         |               | 1955/02/24 0:00:00  |
#abcabc$abcabcabcabcabc$abcabcabc$abcabcabcabcabc$abcabcabcabcabcabcabc%
", $"{Environment.NewLine}{text}");
        }
    }
}