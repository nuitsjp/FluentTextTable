using System;
using System.IO;
using Xunit;
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable NotAccessedField.Local

namespace FluentTextTable.Test
{
    namespace Borders
    {
        public class Borders
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
        }

        public class Top
        {
            [Fact]
            public void WhenDisable()
            {

                var table = TextTable.Build<User>(config =>
                {
                    config.EnableAutoGenerateColumns();
                    config.Borders.Top.Disable();
                });
                var text = table.ToString(new[]
                {
                    new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                    new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
                });

                Assert.Equal(
                    @"
| Id | Name        | Parents | Occupations | Birthday           |
+----+-------------+---------+-------------+--------------------+
| 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 |
+----+-------------+---------+-------------+--------------------+
| 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 |
+----+-------------+---------+-------------+--------------------+
", $"{Environment.NewLine}{text}");
            }
            
            [Fact]
            public void WhenChangeDecorations()
            {

                var table = TextTable.Build<User>(config =>
                {
                    config.EnableAutoGenerateColumns();
                    config.Borders.Top
                        .LeftEndIs('#')
                        .LineIs('=')
                        .IntersectionIs('$')
                        .RightEndIs('%');
                });
                var text = table.ToString(new[]
                {
                    new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                    new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
                });

                Assert.Equal(
                    @"
#====$=============$=========$=============$====================%
| Id | Name        | Parents | Occupations | Birthday           |
+----+-------------+---------+-------------+--------------------+
| 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 |
+----+-------------+---------+-------------+--------------------+
| 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 |
+----+-------------+---------+-------------+--------------------+
", $"{Environment.NewLine}{text}");
            }
        }

        public class HeaderHorizontal
        {
            [Fact]
            public void WhenDisable()
            {

                var table = TextTable.Build<User>(config =>
                {
                    config.EnableAutoGenerateColumns();
                    config.Borders.HeaderHorizontal.Disable();
                });
                var text = table.ToString(new[]
                {
                    new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                    new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
                });

                Assert.Equal(
                    @"
+----+-------------+---------+-------------+--------------------+
| Id | Name        | Parents | Occupations | Birthday           |
| 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 |
+----+-------------+---------+-------------+--------------------+
| 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 |
+----+-------------+---------+-------------+--------------------+
", $"{Environment.NewLine}{text}");
            }
            
            [Fact]
            public void WhenChangeDecorations()
            {

                var table = TextTable.Build<User>(config =>
                {
                    config.EnableAutoGenerateColumns();
                    config.Borders.HeaderHorizontal
                        .LeftEndIs('#')
                        .LineIs('=')
                        .IntersectionIs('$')
                        .RightEndIs('%');
                });
                var text = table.ToString(new[]
                {
                    new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                    new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
                });

                Assert.Equal(
                    @"
+----+-------------+---------+-------------+--------------------+
| Id | Name        | Parents | Occupations | Birthday           |
#====$=============$=========$=============$====================%
| 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 |
+----+-------------+---------+-------------+--------------------+
| 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 |
+----+-------------+---------+-------------+--------------------+
", $"{Environment.NewLine}{text}");
            }
        }
        
        public class InsideHorizontal
        {
            [Fact]
            public void WhenDisable()
            {

                var table = TextTable.Build<User>(config =>
                {
                    config.EnableAutoGenerateColumns();
                    config.Borders.InsideHorizontal.Disable();
                });
                var text = table.ToString(new[]
                {
                    new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                    new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
                });

                Assert.Equal(
                    @"
+----+-------------+---------+-------------+--------------------+
| Id | Name        | Parents | Occupations | Birthday           |
+----+-------------+---------+-------------+--------------------+
| 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 |
| 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 |
+----+-------------+---------+-------------+--------------------+
", $"{Environment.NewLine}{text}");
            }
            
            [Fact]
            public void WhenChangeDecorations()
            {

                var table = TextTable.Build<User>(config =>
                {
                    config.EnableAutoGenerateColumns();
                    config.Borders.InsideHorizontal
                        .LeftEndIs('#')
                        .LineIs('=')
                        .IntersectionIs('$')
                        .RightEndIs('%');
                });
                var text = table.ToString(new[]
                {
                    new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                    new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
                });

                Assert.Equal(
                    @"
+----+-------------+---------+-------------+--------------------+
| Id | Name        | Parents | Occupations | Birthday           |
+----+-------------+---------+-------------+--------------------+
| 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 |
#====$=============$=========$=============$====================%
| 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 |
+----+-------------+---------+-------------+--------------------+
", $"{Environment.NewLine}{text}");
            }
        }
        
        public class Bottom
        {
            [Fact]
            public void WhenDisable()
            {

                var table = TextTable.Build<User>(config =>
                {
                    config.EnableAutoGenerateColumns();
                    config.Borders.Bottom.Disable();
                });
                var text = table.ToString(new[]
                {
                    new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                    new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
                });

                Assert.Equal(
                    @"
+----+-------------+---------+-------------+--------------------+
| Id | Name        | Parents | Occupations | Birthday           |
+----+-------------+---------+-------------+--------------------+
| 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 |
+----+-------------+---------+-------------+--------------------+
| 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 |
", $"{Environment.NewLine}{text}");
            }
            
            [Fact]
            public void WhenChangeDecorations()
            {

                var table = TextTable.Build<User>(config =>
                {
                    config.EnableAutoGenerateColumns();
                    config.Borders.Bottom
                        .LeftEndIs('#')
                        .LineIs('=')
                        .IntersectionIs('$')
                        .RightEndIs('%');
                });
                var text = table.ToString(new[]
                {
                    new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                    new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
                });

                Assert.Equal(
                    @"
+----+-------------+---------+-------------+--------------------+
| Id | Name        | Parents | Occupations | Birthday           |
+----+-------------+---------+-------------+--------------------+
| 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 |
+----+-------------+---------+-------------+--------------------+
| 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 |
#====$=============$=========$=============$====================%
", $"{Environment.NewLine}{text}");
            }
        }
        
        public class Left
        {
            [Fact]
            public void WhenDisable()
            {

                var table = TextTable.Build<User>(config =>
                {
                    config.EnableAutoGenerateColumns();
                    config.Borders.Left.Disable();
                });
                var text = table.ToString(new[]
                {
                    new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                    new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
                });

                Assert.Equal(
                    @"
----+-------------+---------+-------------+--------------------+
 Id | Name        | Parents | Occupations | Birthday           |
----+-------------+---------+-------------+--------------------+
 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 |
----+-------------+---------+-------------+--------------------+
 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 |
----+-------------+---------+-------------+--------------------+
", $"{Environment.NewLine}{text}");
            }
            
            [Fact]
            public void WhenChangeDecorations()
            {

                var table = TextTable.Build<User>(config =>
                {
                    config.EnableAutoGenerateColumns();
                    config.Borders.Left.LineIs('\\');
                });
                var text = table.ToString(new[]
                {
                    new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                    new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
                });

                Assert.Equal(
                    @"
+----+-------------+---------+-------------+--------------------+
\ Id | Name        | Parents | Occupations | Birthday           |
+----+-------------+---------+-------------+--------------------+
\ 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 |
+----+-------------+---------+-------------+--------------------+
\ 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 |
+----+-------------+---------+-------------+--------------------+
", $"{Environment.NewLine}{text}");
            }
        }
        
        public class InsideVertical
        {
            [Fact]
            public void WhenDisable()
            {

                var table = TextTable.Build<User>(config =>
                {
                    config.EnableAutoGenerateColumns();
                    config.Borders.InsideVertical.Disable();
                });
                var text = table.ToString(new[]
                {
                    new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                    new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
                });

                Assert.Equal(
                    @"
+-----------------------------------------------------------+
| Id  Name         Parents  Occupations  Birthday           |
+-----------------------------------------------------------+
| 1   ビル ゲイツ                        1955/10/28 0:00:00 |
+-----------------------------------------------------------+
| 2   Steven Jobs                        1955/02/24 0:00:00 |
+-----------------------------------------------------------+
", $"{Environment.NewLine}{text}");
            }
            
            [Fact]
            public void WhenChangeDecorations()
            {

                var table = TextTable.Build<User>(config =>
                {
                    config.EnableAutoGenerateColumns();
                    config.Borders.InsideVertical.LineIs('\\');
                });
                var text = table.ToString(new[]
                {
                    new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                    new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
                });

                Assert.Equal(
                    @"
+----+-------------+---------+-------------+--------------------+
| Id \ Name        \ Parents \ Occupations \ Birthday           |
+----+-------------+---------+-------------+--------------------+
| 1  \ ビル ゲイツ \         \             \ 1955/10/28 0:00:00 |
+----+-------------+---------+-------------+--------------------+
| 2  \ Steven Jobs \         \             \ 1955/02/24 0:00:00 |
+----+-------------+---------+-------------+--------------------+
", $"{Environment.NewLine}{text}");
            }
        }

        public class Right
        {
            [Fact]
            public void WhenDisable()
            {

                var table = TextTable.Build<User>(config =>
                {
                    config.EnableAutoGenerateColumns();
                    config.Borders.Right.Disable();
                });
                var text = table.ToString(new[]
                {
                    new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                    new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
                });

                Assert.Equal(
                    @"
+----+-------------+---------+-------------+--------------------
| Id | Name        | Parents | Occupations | Birthday           
+----+-------------+---------+-------------+--------------------
| 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 
+----+-------------+---------+-------------+--------------------
| 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 
+----+-------------+---------+-------------+--------------------
", $"{Environment.NewLine}{text}");
            }
            
            [Fact]
            public void WhenChangeDecorations()
            {

                var table = TextTable.Build<User>(config =>
                {
                    config.EnableAutoGenerateColumns();
                    config.Borders.Right.LineIs('\\');
                });
                var text = table.ToString(new[]
                {
                    new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
                    new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
                });

                Assert.Equal(
                    @"
+----+-------------+---------+-------------+--------------------+
| Id | Name        | Parents | Occupations | Birthday           \
+----+-------------+---------+-------------+--------------------+
| 1  | ビル ゲイツ |         |             | 1955/10/28 0:00:00 \
+----+-------------+---------+-------------+--------------------+
| 2  | Steven Jobs |         |             | 1955/02/24 0:00:00 \
+----+-------------+---------+-------------+--------------------+
", $"{Environment.NewLine}{text}");
            }
        }

        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime Birthday;
            public string Parents { get; set; }
            public string[] Occupations { get; set; }

        }
    }
}
