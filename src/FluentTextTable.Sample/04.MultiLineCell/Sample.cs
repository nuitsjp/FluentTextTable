using System;

namespace FluentTextTable.Sample._04.MultiLineCell
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday;
        public string Parents { get; set; }
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
                    Parents = $"Bill Gates Sr.{Environment.NewLine}Mary Maxwell Gates",
                    Occupations = new []{"Software developer", "Investor", "Entrepreneur", "Philanthropist"}
                }
            };
            Build
                .TextTable<User>(builder =>
                {
                    builder
                        .Columns.Add(x => x.Id).NameAs("ID").HorizontalAlignmentAs(HorizontalAlignment.Right)
                        .Columns.Add(x => x.Name).VerticalAlignmentAs(VerticalAlignment.Center)
                        .Columns.Add(x => x.Birthday).VerticalAlignmentAs(VerticalAlignment.Bottom)
                        .FormatAs("{0:yyyy/MM/dd}")
                        .Columns.Add(x => x.Parents).VerticalAlignmentAs(VerticalAlignment.Center).FormatAs("- {0}")
                        .Columns.Add(x => x.Occupations).HorizontalAlignmentAs(HorizontalAlignment.Center);
                })
                .WriteLine(users);

        }
    }


}