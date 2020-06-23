using System;
using System.IO;
using Xunit;
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable NotAccessedField.Local

namespace FluentTextTable.Test
{
    public class PlainTextTest
    {
        [Fact]
        public void WhenBasic()
        {
            var table = TextTable.Build<User>(config =>
            {
                config.AddColumn(x => x.Id)
                    .HasName("ID")
                    .AlignHorizontal(HorizontalAlignment.Right);
                config.AddColumn(x => x.Name)
                    .HasName("氏名");
                config.AddColumn(x => x.Birthday)
                    .HasFormat("{0:yyyy/MM/dd}");
            });
            var text = table.ToString(new[]
            {
                new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
            });

            Assert.Equal(
                @"
+----+-------------+------------+
| ID | 氏名        | Birthday   |
+----+-------------+------------+
|  1 | ビル ゲイツ | 1955/10/28 |
+----+-------------+------------+
|  2 | Steven Jobs | 1955/02/24 |
+----+-------------+------------+
", $"{Environment.NewLine}{text}");
        }

        [Fact]
        public void WhenAutoFormat()
        {
            using var writer = new StringWriter();
            var table = TextTable.Build<User>();
            table.Write(writer, new[]
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
+----+-------------+---------+-------------+--------------------+
", $"{Environment.NewLine}{writer}");
        }

        [Fact]
        public void WhenAutoGenerateColumnsIsTrue()
        {

            var table = TextTable.Build<User>(config =>
            {
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
+----+-------------+---------+-------------+--------------------+
", $"{Environment.NewLine}{text}");
        }

        [Fact]
        public void WhenSpecifyPadding()
        {
            var table = TextTable.Build<User>(config =>
            {
                config.HasPadding(2);
                config.AddColumn(x => x.Id)
                    .HasName("ID")
                    .AlignHorizontal(HorizontalAlignment.Right);
                config.AddColumn(x => x.Name)
                    .HasName("氏名");
                config.AddColumn(x => x.Birthday)
                    .HasFormat("{0:yyyy/MM/dd}");
            });
            var text = table.ToString(new[]
            {
                new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
            });

            Assert.Equal(
                @"
+------+---------------+--------------+
|  ID  |  氏名         |  Birthday    |
+------+---------------+--------------+
|   1  |  ビル ゲイツ  |  1955/10/28  |
+------+---------------+--------------+
|   2  |  Steven Jobs  |  1955/02/24  |
+------+---------------+--------------+
", $"{Environment.NewLine}{text}");
        }

        [Fact]
        public void WhenMultipleLines()
        {

            var table = TextTable.Build<User>(config =>
            {
                config.AddColumn(x => x.Id)
                    .HasName("ID")
                    .AlignHorizontal(HorizontalAlignment.Right);
                config.AddColumn(x => x.Name)
                    .AlignVertical(VerticalAlignment.Center);
                config.AddColumn(x => x.Birthday)
                    .AlignVertical(VerticalAlignment.Center)
                    .HasFormat("{0:yyyy/MM/dd}")
                    .AlignVertical(VerticalAlignment.Bottom);
                config.AddColumn(x => x.Parents)
                    .AlignVertical(VerticalAlignment.Center)
                    .HasFormat("- {0}");
                config.AddColumn(x => x.Occupations)
                    .AlignHorizontal(HorizontalAlignment.Center);
            });
            var text = table.ToString(new[]
            {
                new User
                {
                    Id = 1, 
                    Name = "Bill Gates", 
                    Birthday = DateTime.Parse("1955/10/28"),
                    Parents = $"Bill Gates Sr.{Environment.NewLine}Mary Maxwell Gates",
                    Occupations = new []{"Software developer", "Investor", "Entrepreneur", "Philanthropist"}
                }
            });

            Assert.Equal(
                @"
+----+------------+------------+----------------------+--------------------+
| ID | Name       | Birthday   | Parents              | Occupations        |
+----+------------+------------+----------------------+--------------------+
|  1 |            |            |                      | Software developer |
|    | Bill Gates |            | - Bill Gates Sr.     |      Investor      |
|    |            |            | - Mary Maxwell Gates |    Entrepreneur    |
|    |            | 1955/10/28 |                      |   Philanthropist   |
+----+------------+------------+----------------------+--------------------+
", $"{Environment.NewLine}{text}");
        }

        [Fact]
        public void WithAttribute()
        {

            var table = TextTable.Build<UserWithAttribute>();
            var text = table.ToString(new[]
            {
                new UserWithAttribute
                {
                    Id = 1,
                    Name = "Bill Gates",
                    Birthday = DateTime.Parse("1955/10/28"),
                    Parents = $"Bill Gates Sr.{Environment.NewLine}Mary Maxwell Gates",
                    Occupations = new []{"Software developer", "Investor", "Entrepreneur", "Philanthropist"}
                }
            });

            Assert.Equal(
                @"
+----+------------+------------+----------------------+--------------------+
| ID | Name       | Birthday   | Parents              | Occupations        |
+----+------------+------------+----------------------+--------------------+
|  1 |            |            |                      | Software developer |
|    | Bill Gates |            | - Bill Gates Sr.     |      Investor      |
|    |            |            | - Mary Maxwell Gates |    Entrepreneur    |
|    |            | 1955/10/28 |                      |   Philanthropist   |
+----+------------+------------+----------------------+--------------------+
", $"{Environment.NewLine}{text}");
        }

        private class UserWithAttribute
        {
            [Column(Index = 1, Header = "ID", HorizontalAlignment = HorizontalAlignment.Right)]
            public int Id { get; set; }

            [Column(Index = 2, VerticalAlignment = VerticalAlignment.Center)]
            public string Name { get; set; }

            [Column(Index = 3, VerticalAlignment = VerticalAlignment.Bottom, Format = "{0:yyyy/MM/dd}")]
            public DateTime Birthday;

            [Column(Index = 4, VerticalAlignment = VerticalAlignment.Center, Format = "- {0}")]
            public string Parents { get; set; }

            [Column(Index = 5, HorizontalAlignment = HorizontalAlignment.Center)]
            public string[] Occupations { get; set; }

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
