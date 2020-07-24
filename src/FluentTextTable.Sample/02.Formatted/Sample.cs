using System;

namespace FluentTextTable.Sample._02.Formatted
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday;
        public string[] Occupations { get; set; }

    }

    public class Sample
    {
        public static void WriteConsole()
        {
            var users = new[]
            {
                new User
                {
                    Id = 1,
                    Name = "Bill Gates",
                    Birthday = DateTime.Parse("1955/10/28"),
                    Occupations = new []{"Software developer", "Investor", "Entrepreneur", "Philanthropist"}
                },
                new User
                {
                    Id = 2,
                    Name = "Steven Jobs",
                    Birthday = DateTime.Parse("1955/2/24"),
                    Occupations = new []{ "Entrepreneur", "Industrial designer", "Investor", "Media proprietor" }
                }
            };
            Build
                .TextTable<User>(builder =>
                {
                    builder
                        .Borders.Horizontals.AllStylesAs("-")
                        .Borders.HeaderHorizontal.AllStylesAs("=")
                        .Columns.Add(x => x.Id).HorizontalAlignmentAs(HorizontalAlignment.Right)
                        .Columns.Add(x => x.Name).VerticalAlignmentAs(VerticalAlignment.Center)
                        .Columns.Add(x => x.Birthday).VerticalAlignmentAs(VerticalAlignment.Bottom).FormatAs("{0:yyyy/MM/dd}")
                        .Columns.Add(x => x.Occupations).FormatAs("- {0}");
                })
                .WriteLine(users);
        }
    }


}