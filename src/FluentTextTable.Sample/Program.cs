using System;

namespace FluentTextTable.Sample
{
    static class Program
    {
        static void Main()
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
            table.WriteLine(new[]
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

            //var table = Build.TextTable<User>();
            //    .WriteLine(users);
            //var table = Build.TextTable<User>(builder =>
            //{
            //    builder
            //        .Borders.Top
            //            .LeftStyleAs("-")
            //            .IntersectionStyleAs("-")
            //            .RightStyleAs("-")
            //        .Borders.HeaderHorizontal
            //            .LineStyleAs("=")
            //        .Borders.InsideHorizontal
            //            .AsDisable()
            //        .Borders.Bottom
            //            .LeftStyleAs("*")
            //            .IntersectionStyleAs("*")
            //            .RightStyleAs("*");
            //});
            //var users = new[]
            //{
            //    new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
            //    new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
            //};
            //table.WriteLine(users);

            //Build
            //    .MarkdownTable<User>()
            //    .WriteLine(users);

            //Build
            //    .TextTable<User>(builder =>
            //    {
            //        builder
            //            .Columns.Add(x => x.Id).HorizontalAlignmentAs(HorizontalAlignment.Right)
            //            .Columns.Add(x => x.Name).NameAs("NAME")
            //            .Columns.Add(x => x.Birthday).FormatAs("{0:yyyy/MM/dd}");
            //    })
            //    .WriteLine(users);
            //Build
            //    .TextTable<User>(builder =>
            //    {
            //        builder
            //            .Borders.InsideHorizontal.AsDisable()
            //            .Columns.Add(x => x.Id).HorizontalAlignmentAs(HorizontalAlignment.Right)
            //            .Columns.Add(x => x.Name).NameAs("氏名")
            //            .Columns.Add(x => x.Birthday).FormatAs("{0:yyyy/MM/dd}");
            //    })
            //    .WriteLine(users);

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
        
        //private class User
        //{
        //    public int Id { get; set; }
        //    public string Name { get; set; }
        //    public DateTime Birthday;
        //}

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