using System;
using System.IO;
using Xunit;

namespace FluentTextTable.Test
{
    public class MarkdownTest
    {
        [Fact]
        public void WhenBasic()
        {

            var table = Build.MarkdownTable<User>(builder =>
            {
                builder
                    .Columns.Add(x => x.Id).NameAs("ID").HorizontalAlignmentAs(HorizontalAlignment.Center)
                    .Columns.Add(x => x.Name).NameAs("氏名").HorizontalAlignmentAs(HorizontalAlignment.Left)
                    .Columns.Add(x => x.Birthday).FormatAs("{0:yyyy/MM/dd}");
            });
            var text = table.ToString(new[]
                {
                    new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                    new User {Id = 123, Name = "Steven Paul Jobs", Birthday = DateTime.Parse("1955/2/24")}
                });

            Assert.Equal(
                @"
| ID  | 氏名             | Birthday   |
|:---:|:-----------------|------------|
|  1  | ビル ゲイツ      | 1955/10/28 |
| 123 | Steven Paul Jobs | 1955/02/24 |
", $"{Environment.NewLine}{text}");
        }

        [Fact]
        public void WhenTuple()
        {
            var table = Build.MarkdownTable<(int, string, DateTime)>(builder =>
            {
                builder
                    .Columns.Add(x => x.Item1).NameAs("ID").HorizontalAlignmentAs(HorizontalAlignment.Center)
                    .Columns.Add(x => x.Item2).NameAs("氏名").HorizontalAlignmentAs(HorizontalAlignment.Left)
                    .Columns.Add(x => x.Item3).FormatAs("{0:yyyy/MM/dd}");
            });
            var text = table.ToString(new[]
            {
                (1, "ビル ゲイツ", DateTime.Parse("1955/10/28")),
                (123, "Steven Paul Jobs", DateTime.Parse("1955/2/24"))
            });

            Assert.Equal(
                @"
| ID  | 氏名             | Item3      |
|:---:|:-----------------|------------|
|  1  | ビル ゲイツ      | 1955/10/28 |
| 123 | Steven Paul Jobs | 1955/02/24 |
", $"{Environment.NewLine}{text}");
        }

        [Fact]
        public void WhenValueTuple()
        {
            var table = Build.MarkdownTable<(int Id, string Name, DateTime Birthday)>(builder =>
            {
                builder
                    .Columns.Add(x => x.Id).NameAs("ID").HorizontalAlignmentAs(HorizontalAlignment.Center)
                    .Columns.Add(x => x.Name).NameAs("氏名").HorizontalAlignmentAs(HorizontalAlignment.Left)
                    .Columns.Add(x => x.Birthday).FormatAs("{0:yyyy/MM/dd}");
            });
            var text = table.ToString(new[]
            {
                (Id:1, Name:"ビル ゲイツ", Birthday:DateTime.Parse("1955/10/28")),
                (Id:123, Name:"Steven Paul Jobs", Birthday:DateTime.Parse("1955/2/24"))
            });

            Assert.Equal(
                @"
| ID  | 氏名             | Item3      |
|:---:|:-----------------|------------|
|  1  | ビル ゲイツ      | 1955/10/28 |
| 123 | Steven Paul Jobs | 1955/02/24 |
", $"{Environment.NewLine}{text}");
        }

        [Fact]
        public void WhenAutoFormat()
        {
            using var textWriter = new StringWriter();
            var table = Build.MarkdownTable<User>();
            table.Write(textWriter,new[]
                {
                    new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                    new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
                });

            Assert.Equal(
                @"
| Id | Name        | Parents | Occupations | Birthday           |
|----|-------------|---------|-------------|--------------------|
| 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 |
| 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 |
", $"{Environment.NewLine}{textWriter}");
        }

        [Fact]
        public void WhenMultipleLines()
        {

            var table = Build.MarkdownTable<User>(builder =>
            {
                builder
                    .Columns.Add(x => x.Id).NameAs("ID").HorizontalAlignmentAs(HorizontalAlignment.Right)
                    .Columns.Add(x => x.Name).VerticalAlignmentAs(VerticalAlignment.Center)
                    .Columns.Add(x => x.Birthday).VerticalAlignmentAs(VerticalAlignment.Bottom).FormatAs("{0:yyyy/MM/dd}")
                    .Columns.Add(x => x.Parents).VerticalAlignmentAs(VerticalAlignment.Center).FormatAs("- {0}")
                    .Columns.Add(x => x.Occupations).HorizontalAlignmentAs(HorizontalAlignment.Center);
            });
            var text = table.ToString(new[]
            {
                new User
                {
                    Id = 1,
                    Name = "Bill Gates",
                    Birthday = DateTime.Parse("1955/10/28"),
                    Parents = $"Bill Gates Sr.{Environment.NewLine}Mary Maxwell Gates",
                    Occupations = new[] {"Software developer", "Investor", "Entrepreneur", "Philanthropist"}
                }
            });

            Assert.Equal(
                @"
| ID | Name       | Birthday   | Parents                                  | Occupations                                                      |
|---:|------------|------------|------------------------------------------|:----------------------------------------------------------------:|
|  1 | Bill Gates | 1955/10/28 | - Bill Gates Sr.<br>- Mary Maxwell Gates | Software developer<br>Investor<br>Entrepreneur<br>Philanthropist |
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
