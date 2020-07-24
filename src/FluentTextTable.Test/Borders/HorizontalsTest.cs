using System;
using System.IO;
using Xunit;
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable NotAccessedField.Local

namespace FluentTextTable.Test.Borders
{
    public class HorizontalsTest
    {
        [Fact]
        public void WhenDisable()
        {

            var table = Build.TextTable<User>(builder =>
            {
                builder.Borders.Horizontals.AsDisable();
            });
            var text = table.ToString(new[]
            {
                new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
            });

            Assert.Equal(
                @"
 | Id | Name        | Parents | Occupations | Birthday           |
 | 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 |
 | 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 |
", $"{Environment.NewLine}{text}");
        }

        [Fact]
        public void WhenAllStyles()
        {

            var table = Build.TextTable<User>(builder =>
            {
                builder.Borders.Horizontals.AllStylesAs(".");
            });
            var text = table.ToString(new[]
            {
                new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
            });

            Assert.Equal(
                @"
 .................................................................
 | Id | Name        | Parents | Occupations | Birthday           |
 .................................................................
 | 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 |
 .................................................................
 | 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 |
 .................................................................
", $"{Environment.NewLine}{text}");
        }

        [Fact]
        public void WhenLeftStyle()
        {

            var table = Build.TextTable<User>(builder =>
            {
                builder.Borders.Horizontals.LeftStyleAs(".");
            });
            var text = table.ToString(new[]
            {
                new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
            });

            Assert.Equal(
                @"
 .----+-------------+---------+-------------+--------------------+
 | Id | Name        | Parents | Occupations | Birthday           |
 .----+-------------+---------+-------------+--------------------+
 | 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 |
 .----+-------------+---------+-------------+--------------------+
 | 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 |
 .----+-------------+---------+-------------+--------------------+
", $"{Environment.NewLine}{text}");
        }

        [Fact]
        public void WhenLineStyle()
        {

            var table = Build.TextTable<User>(builder =>
            {
                builder.Borders.Horizontals.LineStyleAs(".");
            });
            var text = table.ToString(new[]
            {
                new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
            });

            Assert.Equal(
                @"
 +....+.............+.........+.............+....................+
 | Id | Name        | Parents | Occupations | Birthday           |
 +....+.............+.........+.............+....................+
 | 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 |
 +....+.............+.........+.............+....................+
 | 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 |
 +....+.............+.........+.............+....................+
", $"{Environment.NewLine}{text}");
        }

        [Fact]
        public void WhenIntersectionStyle()
        {

            var table = Build.TextTable<User>(builder =>
            {
                builder.Borders.Horizontals.IntersectionStyleAs(".");
            });
            var text = table.ToString(new[]
            {
                new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
            });

            Assert.Equal(
                @"
 +----.-------------.---------.-------------.--------------------+
 | Id | Name        | Parents | Occupations | Birthday           |
 +----.-------------.---------.-------------.--------------------+
 | 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 |
 +----.-------------.---------.-------------.--------------------+
 | 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 |
 +----.-------------.---------.-------------.--------------------+
", $"{Environment.NewLine}{text}");
        }

        [Fact]
        public void WhenRightStyleAs()
        {

            var table = Build.TextTable<User>(builder =>
            {
                builder.Borders.Horizontals.RightStyleAs(".");
            });
            var text = table.ToString(new[]
            {
                new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
            });

            Assert.Equal(
                @"
 +----+-------------+---------+-------------+--------------------.
 | Id | Name        | Parents | Occupations | Birthday           |
 +----+-------------+---------+-------------+--------------------.
 | 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 |
 +----+-------------+---------+-------------+--------------------.
 | 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 |
 +----+-------------+---------+-------------+--------------------.
", $"{Environment.NewLine}{text}");
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
