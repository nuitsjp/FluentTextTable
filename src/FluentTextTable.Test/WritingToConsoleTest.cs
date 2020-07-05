using System;
using System.IO;
using Xunit;

namespace FluentTextTable.Test
{
    public class WritingToConsoleTest
    {
        private readonly StringWriter _stringWriter = new StringWriter();

        public WritingToConsoleTest()
        {
            Console.SetOut(_stringWriter);
        }

        [Fact]
        public void WhenMarkdown()
        {

            var table = Build.MarkdownTable<User>(builder =>
            {
                builder
                    .Columns.Add(x => x.Id).NameAs("ID").HorizontalAlignmentAs(HorizontalAlignment.Center)
                    .Columns.Add(x => x.Name).NameAs("氏名").HorizontalAlignmentAs(HorizontalAlignment.Left)
                    .Columns.Add(x => x.Birthday).FormatAs("{0:yyyy/MM/dd}");
            });
            table.WriteLine(new[]
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
", $"{Environment.NewLine}{_stringWriter}");
        }

        [Fact]
        public void WhenPlaneText()
        {
            var table = Build.TextTable<User>(builder =>
            {
                builder
                    .Columns.Add(x => x.Id).NameAs("ID").HorizontalAlignmentAs(HorizontalAlignment.Right)
                    .Columns.Add(x => x.Name).NameAs("氏名")
                    .Columns.Add(x => x.Birthday).FormatAs("{0:yyyy/MM/dd}");
            });
            table.WriteLine(new[]
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
", $"{Environment.NewLine}{_stringWriter}");
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