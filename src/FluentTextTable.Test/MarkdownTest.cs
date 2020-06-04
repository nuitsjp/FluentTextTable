using System;
using Xunit;

namespace FluentTextTable.Test
{
    namespace MarkdownTest
    {
        public class WriteMarkdown
        {
            [Fact]
            public void WhenBasic()
            {

                var writer = TextTableWriter<User>.Build(config =>
                {
                    config.AddColumn(x => x.Id)
                        .NameIs("ID")
                        .AlignHorizontalTo(HorizontalAlignment.Center);
                    config.AddColumn(x => x.Name)
                        .NameIs("氏名")
                        .AlignHorizontalTo(HorizontalAlignment.Left);
                    config.AddColumn(x => x.Birthday)
                        .FormatTo("{0:yyyy/MM/dd}");
                });
                var text = writer.ToMarkdown(new[]
                    {
                        new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                        new User {Id = 123, Name = "Steven Paul Jobs", Birthday = DateTime.Parse("1955/2/24")},
                    });

                Assert.Equal(
                    @"
| ID  | 氏名             | Birthday   |
|:---:|:-----------------|------------|
|  1  | ビル ゲイツ      | 1955/10/28 |
| 123 | Steven Paul Jobs | 1955/02/24 |
", Environment.NewLine + text);
            }

            [Fact]
            public void WhenAutoFormat()
            {

                var writer = TextTableWriter<User>.Build();
                var text = writer.ToMarkdown(new[]
                    {
                        new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                        new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")},
                    });

                Assert.Equal(
                    @"
| Id | Name        | Parents | Occupations | Birthday           |
|----|-------------|---------|-------------|--------------------|
| 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 |
| 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 |
", Environment.NewLine + text);
            }


            [Fact]
            public void WhenMultipleLines()
            {

                var writer = TextTableWriter<User>.Build(config =>
                {
                    config.AddColumn(x => x.Id)
                        .NameIs("ID")
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
                var text = writer.ToMarkdown(new[]
                {
                    new User
                    {
                        Id = 1,
                        Name = "Bill Gates",
                        Birthday = DateTime.Parse("1955/10/28"),
                        Parents = $"Bill Gates Sr.{Environment.NewLine}Mary Maxwell Gates",
                        Occupations = new[] {"Software developer", "Investor", "Entrepreneur", "Philanthropist"}
                    },
                });

                Assert.Equal(
                    @"
| ID | Name       | Birthday   | Parents              | Occupations        |
|---:|------------|------------|----------------------|:------------------:|
|  1 | Bill Gates | 1955/10/28 | - Bill Gates Sr.<br>- Mary Maxwell Gates | Software developer<br>Investor<br>Entrepreneur<br>Philanthropist |
", Environment.NewLine + text);
            }

            private class User
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public DateTime Birthday;
                public string Parents { get; set; }
                public string[] Occupations { get; set; }

            }

            [Fact]
            public void WithAttribute()
            {

                var writer = TextTableWriter<UserWithAttribute>.Build();
                var text = writer.ToMarkdown(new[]
                {
                    new UserWithAttribute
                    {
                        Id = 1,
                        Name = "Bill Gates",
                        Birthday = DateTime.Parse("1955/10/28"),
                        Parents = $"Bill Gates Sr.{Environment.NewLine}Mary Maxwell Gates",
                        Occupations = new []{"Software developer", "Investor", "Entrepreneur", "Philanthropist"}
                    },
                });

                Assert.Equal(
                    @"
| ID | Name       | Birthday   | Parents              | Occupations        |
|---:|------------|------------|----------------------|:------------------:|
|  1 | Bill Gates | 1955/10/28 | - Bill Gates Sr.<br>- Mary Maxwell Gates | Software developer<br>Investor<br>Entrepreneur<br>Philanthropist |
", Environment.NewLine + text);
            }

            private class UserWithAttribute
            {
                [ColumnFormat(Index = 1, Header = "ID", HorizontalAlignment = HorizontalAlignment.Right)]
                public int Id { get; set; }

                [ColumnFormat(Index = 2, VerticalAlignment = VerticalAlignment.Center)]
                public string Name { get; set; }

                [ColumnFormat(Index = 3, VerticalAlignment = VerticalAlignment.Bottom, Format = "{0:yyyy/MM/dd}")]
                public DateTime Birthday;

                [ColumnFormat(Index = 4, VerticalAlignment = VerticalAlignment.Center, Format = "- {0}")]
                public string Parents { get; set; }

                [ColumnFormat(Index = 5, HorizontalAlignment = HorizontalAlignment.Center)]
                public string[] Occupations { get; set; }

            }
        }
    }
}
