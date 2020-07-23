using System;

namespace FluentTextTable.Sample
{
    static class Program
    {
        static void Main()
        {
            var users = new[]
            {
                new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
            };
            Build
                .TextTable<User>()
                .WriteLine(users);

            //var table = Build.TextTable<User>(builder =>
            //{
            //    builder
            //        .Borders.HeaderHorizontal.LineStyleAs("=")
            //        .Columns.Add(x => x.Id).NameAs("ID").HorizontalAlignmentAs(HorizontalAlignment.Right)
            //        .Columns.Add(x => x.Name).NameAs("氏名")
            //        .Columns.Add(x => x.Birthday).FormatAs("{0:yyyy/MM/dd}");
            //});
            //table.Write(Console.Out, users);
            Console.ReadLine();
        }
        
        private class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime Birthday;
        }
    }
}