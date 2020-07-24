using System;

namespace FluentTextTable.Sample._12.Paddings
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday;
    }

    public class Sample
    {
        public static void WriteConsole()
        {
            var users = new[]
            {
                new User {Id = 1, Name = "Bill Gates", Birthday = DateTime.Parse("1955/10/28")},
                new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
            };
            Build
                .TextTable<User>(builder =>
                {
                    builder
                        .Paddings.Left.As(4)
                        .Paddings.Right.As(2);
                })
                .WriteLine(users);
        }
    }


}