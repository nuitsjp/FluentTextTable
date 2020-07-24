using System;
using System.IO;
using Xunit;
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable NotAccessedField.Local

namespace FluentTextTable.Test.Borders
{
    public class BordersTest
    {
        [Fact]
        public void WhenDisable()
        {

            var table = Build.TextTable<User>(builder =>
            {
                builder.Borders.AsDisable();
            });
            var text = table.ToString(new[]
            {
                new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
            });

            Assert.Equal(
                @"
  Id  Name         Parents  Occupations  Birthday           
  1   ビル ゲイツ                        1955/10/28 0:00:00 
  2   Steven Jobs                        1955/02/24 0:00:00 
", $"{Environment.NewLine}{text}");
        }

        [Fact]
        public void WhenAllStyles()
        {

            var table = Build.TextTable<User>(builder =>
            {
                builder.Borders.AllStylesAs("・");
            });
            var text = table.ToString(new[]
            {
                new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
            });

            Assert.Equal(
                @"
 ・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・
 ・ Id ・ Name         ・ Parents  ・ Occupations  ・ Birthday           ・
 ・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・
 ・ 1  ・ ビル ゲイツ  ・          ・              ・ 1955/10/28 0:00:00 ・
 ・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・
 ・ 2  ・ Steven Jobs  ・          ・              ・ 1955/02/24 0:00:00 ・
 ・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・・
", $"{Environment.NewLine}{text}");
        }

        [Fact]
        public void WhenFullWidth()
        {
            var table = Build.TextTable<User>(builder =>
            {
                builder
                    .Borders.AsFullWidthStyle()
                    .Columns.Add(x => x.Id).NameAs("ID").HorizontalAlignmentAs(HorizontalAlignment.Right)
                    .Columns.Add(x => x.Name).NameAs("氏名")
                    .Columns.Add(x => x.Birthday).FormatAs("{0:yyyy/MM/dd}");
            });
            var text = table.ToString(new[]
            {
                new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
            });

            Assert.Equal(
                @"
 ┌──┬───────┬──────┐
 │ ID │ 氏名         │ Birthday   │
 ┝━━┿━━━━━━━┿━━━━━━┥
 │  1 │ ビル ゲイツ  │ 1955/10/28 │
 ├──┼───────┼──────┤
 │  2 │ Steven Jobs  │ 1955/02/24 │
 └──┴───────┴──────┘
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
