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

            var table = TextTable.Build<User>(config =>
            {
                config.EnableAutoGenerateColumns();
                config.Borders.Disable();
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
        
//             [Fact]
//             public void WhenFullWidth()
//             {
//                 var table = TextTable.Build<User>(config =>
//                 {
//                     config.Borders.IsFullWidth();
//                     config.AddColumn(x => x.Id)
//                         .HasName("ID")
//                         .AlignHorizontal(HorizontalAlignment.Right);
//                     config.AddColumn(x => x.Name)
//                         .HasName("氏名");
//                     config.AddColumn(x => x.Birthday)
//                         .HasFormat("{0:yyyy/MM/dd}");
//                 });
//                 var text = table.ToString(new[]
//                 {
//                     new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
//                     new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
//                 });
//
//                 Assert.Equal(
//                     @"
// ┌──┬───────┬──────┐
// │ ID │ 氏名         │ Birthday   │
// ├──┼───────┼──────┤
// │  1 │ ビル ゲイツ  │ 1955/10/28 │
// ├──┼───────┼──────┤
// │  2 │ Steven Jobs  │ 1955/02/24 │
// └──┴───────┴──────┘
// ", $"{Environment.NewLine}{text}");
//             }
    }
}
