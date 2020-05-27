using System;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;

namespace FluentTextTable.Test
{
    public class TextTableTest
    {
        [Fact]
        public void ToPlanTextWhenBasic()
        {

            var table = TextTableBuilder.Build<User>(config =>
            {
                config.AddColumn(x => x.Id)
                    .HeaderIs("ID")
                    .AlignHorizontalTo(HorizontalAlignment.Right);
                config.AddColumn(x => x.Name);
                config.AddColumn(x => x.Birthday)
                    .FormatTo("{0:yyyy/MM/dd}");
            });
            table.DataSource = new[]
            {
                new User {Id = 1, Name = "Bill Gates", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")},
            };

            var text = table.ToPlanText();

            Assert.Equal(
                @"
+----+-------------+------------+
| ID | Name        | Birthday   |
+----+-------------+------------+
|  1 | Bill Gates  | 1955/10/28 |
+----+-------------+------------+
|  2 | Steven Jobs | 1955/02/24 |
+----+-------------+------------+
", Environment.NewLine + text);
        }

        [Fact]
        public void ToPlanTextWhenMultipleLines()
        {

            var table = TextTableBuilder.Build<User>(config =>
            {
                config.AddColumn(x => x.Id)
                    .HeaderIs("ID")
                    .AlignHorizontalTo(HorizontalAlignment.Right);
                config.AddColumn(x => x.Name)
                    .AlignVerticalTo(VerticalAlignment.Center);
                config.AddColumn(x => x.Birthday)
                    .AlignVerticalTo(VerticalAlignment.Center)
                    .FormatTo("{0:yyyy/MM/dd}")
                    .AlignVerticalTo(VerticalAlignment.Bottom);
                config.AddColumn(x => x.Parents)
                    .AlignVerticalTo(VerticalAlignment.Center)
                    .FormatTo("- {0}");
                config.AddColumn(x => x.Occupations)
                    .AlignHorizontalTo(HorizontalAlignment.Center);
            });
            table.DataSource = new[]
            {
                new User
                {
                    Id = 1, 
                    Name = "Bill Gates", 
                    Birthday = DateTime.Parse("1955/10/28"),
                    Parents = $"Bill Gates Sr.{Environment.NewLine}Mary Maxwell Gates",
                    Occupations = new []{"Software developer", "Investor", "Entrepreneur", "Philanthropist"}
                },
            };

            var text = table.ToPlanText();

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
", Environment.NewLine + text);
        }

        [Fact]
        public void ToPlanTextWhenWideCharactersAreMixed()
        {

            var table = TextTableBuilder.Build<User>(config =>
            {
                config.AddColumn(x => x.Id)
                    .HeaderIs("ID")
                    .AlignHorizontalTo(HorizontalAlignment.Right);
                config.AddColumn(x => x.Name)
                    .HeaderIs("氏名");
                config.AddColumn(x => x.Birthday)
                    .FormatTo("{0:yyyy/MM/dd}");
            });
            table.DataSource = new[]
            {
                new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")},
            };

            var text = table.ToPlanText();

            Assert.Equal(
                @"
+----+-------------+------------+
| ID | 氏名        | Birthday   |
+----+-------------+------------+
|  1 | ビル ゲイツ | 1955/10/28 |
+----+-------------+------------+
|  2 | Steven Jobs | 1955/02/24 |
+----+-------------+------------+
", Environment.NewLine + text);
        }


        class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime Birthday;
            public string Parents { get; set; }
            public string[] Occupations { get; set; }

        }
    }
}
