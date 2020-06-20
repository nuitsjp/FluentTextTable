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

            var table = TextTable.BuildMarkdown<User>(config =>
            {
                config.AddColumn(x => x.Id)
                    .HasName("ID")
                    .AlignHorizontal(HorizontalAlignment.Center);
                config.AddColumn(x => x.Name)
                    .HasName("氏名")
                    .AlignHorizontal(HorizontalAlignment.Left);
                config.AddColumn(x => x.Birthday)
                    .HasFormat("{0:yyyy/MM/dd}");
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
        public void WhenAutoFormat()
        {
            using var writer = new StringWriter();
            var table = TextTable.BuildMarkdown<User>();
            table.Write(writer,new[]
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
", $"{Environment.NewLine}{writer}");
        }

        [Fact]
        public void WhenMultipleLines()
        {

            var table = TextTable.BuildMarkdown<User>(config =>
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
