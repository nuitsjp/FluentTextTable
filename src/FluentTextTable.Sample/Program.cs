using System;

namespace FluentTextTable.Sample
{
    static class Program
    {
        static void Main(string[] args)
        {
            var table = Build.TextTable<User>(builder =>
            {
                builder
                    .Borders.HeaderHorizontal.LineStyleAs("=")
                    .AddColumn(x => x.Id).NameAs("ID").HorizontalAlignmentAs(HorizontalAlignment.Right)
                    .AddColumn(x => x.Name).NameAs("氏名")
                    .AddColumn(x => x.Birthday).FormatAs("{0:yyyy/MM/dd}");
            });
            table.Write(Console.Out, new[]
            {
                new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
            });
        }
        
        private class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime Birthday;
        }
    }
}