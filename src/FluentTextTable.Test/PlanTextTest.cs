using System;
using Xunit;
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable NotAccessedField.Local
// ReSharper disable HeapView.ObjectAllocation.Evident
// ReSharper disable HeapView.BoxingAllocation
// ReSharper disable HeapView.ObjectAllocation

namespace FluentTextTable.Test
{
    namespace PlanTextTest
    {
        public class ToPlanText
        {
            [Fact]
            public void WhenBasic()
            {

                var writer = TextTableWriter<User>.Build(config =>
                {
                    config.AddColumn(x => x.Id)
                        .NameIs("ID")
                        .AlignHorizontalTo(HorizontalAlignment.Right);
                    config.AddColumn(x => x.Name)
                        .NameIs("氏名");
                    config.AddColumn(x => x.Birthday)
                        .FormatTo("{0:yyyy/MM/dd}");
                });
                var text = writer.ToPlanText(new[]
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
", $"{Environment.NewLine}{text}");
            }

            [Fact]
            public void WhenAutoFormat()
            {

                var writer = TextTableWriter<User>.Build();
                var text = writer.ToPlanText(new[]
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
+----+-------------+---------+-------------+--------------------+
", $"{Environment.NewLine}{text}");
            }

            [Fact]
            public void WhenAutoGenerateColumnsIsTrue()
            {

                var writer = TextTableWriter<User>.Build(config =>
                {
                    config.AutoGenerateColumns = true;
                });
                var text = writer.ToPlanText(new[]
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
+----+-------------+---------+-------------+--------------------+
", $"{Environment.NewLine}{text}");
            }

            [Fact]
            public void WhenMultipleLines()
            {

                var table = TextTableWriter<User>.Build(config =>
                {
                    config.AddColumn(x => x.Id)
                        .NameIs("ID")
                        .AlignHorizontalTo(HorizontalAlignment.Right);
                    config.AddColumn(x => x.Name)
                        .AlignVerticalTo(VerticalAlignment.Center);
                    config.AddColumn(x => x.Birthday)
                        .AlignVerticalTo(VerticalAlignment.Center)
                        .FormatTo("{0:yyyy/MM/dd}")
                        .AlignVerticalTo(VerticalAlignment.Bottom);
                    config.AddColumn(x => x.Parents)
                        .AlignVerticalTo(VerticalAlignment.Center)
                        .FormatTo("- {0}");
                    config.AddColumn(x => x.Occupations)
                        .AlignHorizontalTo(HorizontalAlignment.Center);
                });
                var text = table.ToPlanText(new[]
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

                Assert.Equal(
                    @"
+----+------------+------------+----------------------+--------------------+
| ID | Name       | Birthday   | Parents              | Occupations        |
+----+------------+------------+----------------------+--------------------+
|  1 |            |            |                      | Software developer |
|    | Bill Gates |            | - Bill Gates Sr.     |      Investor      |
|    |            |            | - Mary Maxwell Gates |    Entrepreneur    |
|    |            | 1955/10/28 |                      |   Philanthropist   |
+----+------------+------------+----------------------+--------------------+
", $"{Environment.NewLine}{text}");
            }

            [Fact]
            public void WithAttribute()
            {

                var writer = TextTableWriter<UserWithAttribute>.Build();
                var text = writer.ToPlanText(new[]
                {
                    new UserWithAttribute
                    {
                        Id = 1,
                        Name = "Bill Gates",
                        Birthday = DateTime.Parse("1955/10/28"),
                        Parents = $"Bill Gates Sr.{Environment.NewLine}Mary Maxwell Gates",
                        Occupations = new []{"Software developer", "Investor", "Entrepreneur", "Philanthropist"}
                    }
                });

                Assert.Equal(
                    @"
+----+------------+------------+----------------------+--------------------+
| ID | Name       | Birthday   | Parents              | Occupations        |
+----+------------+------------+----------------------+--------------------+
|  1 |            |            |                      | Software developer |
|    | Bill Gates |            | - Bill Gates Sr.     |      Investor      |
|    |            |            | - Mary Maxwell Gates |    Entrepreneur    |
|    |            | 1955/10/28 |                      |   Philanthropist   |
+----+------------+------------+----------------------+--------------------+
", $"{Environment.NewLine}{text}");
            }

            private class UserWithAttribute
            {
                [ColumnFormat(Index = 1, Header = "ID", HorizontalAlignment = HorizontalAlignment.Right)]
                public int Id { get; set; }

                [ColumnFormat(Index = 2, VerticalAlignment = VerticalAlignment.Center)]
                public string Name { get; set; }

                [ColumnFormat(Index = 3, VerticalAlignment = VerticalAlignment.Bottom, Format = "{0:yyyy/MM/dd}")]
                public DateTime Birthday;

                [ColumnFormat(Index = 4, VerticalAlignment = VerticalAlignment.Center, Format = "- {0}")]
                public string Parents { get; set; }

                [ColumnFormat(Index = 5, HorizontalAlignment = HorizontalAlignment.Center)]
                public string[] Occupations { get; set; }

            }
        }

        namespace Borders
        {
            public class Top
            {
                [Fact]
                public void WhenDisable()
                {

                    var writer = TextTableWriter<User>.Build(config =>
                    {
                        config.AutoGenerateColumns = true;
                        config.Borders.Top.Disable();
                    });
                    var text = writer.ToPlanText(new[]
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

                    var writer = TextTableWriter<User>.Build(config =>
                    {
                        config.AutoGenerateColumns = true;
                        config.Borders.Top
                            .LeftEndIs('#')
                            .LineIs('=')
                            .IntersectionIs('$')
                            .RightEndIs('%');
                    });
                    var text = writer.ToPlanText(new[]
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

                    var writer = TextTableWriter<User>.Build(config =>
                    {
                        config.AutoGenerateColumns = true;
                        config.Borders.HeaderHorizontal.Disable();
                    });
                    var text = writer.ToPlanText(new[]
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

                    var writer = TextTableWriter<User>.Build(config =>
                    {
                        config.AutoGenerateColumns = true;
                        config.Borders.HeaderHorizontal
                            .LeftEndIs('#')
                            .LineIs('=')
                            .IntersectionIs('$')
                            .RightEndIs('%');
                    });
                    var text = writer.ToPlanText(new[]
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

                    var writer = TextTableWriter<User>.Build(config =>
                    {
                        config.AutoGenerateColumns = true;
                        config.Borders.InsideHorizontal.Disable();
                    });
                    var text = writer.ToPlanText(new[]
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

                    var writer = TextTableWriter<User>.Build(config =>
                    {
                        config.AutoGenerateColumns = true;
                        config.Borders.InsideHorizontal
                            .LeftEndIs('#')
                            .LineIs('=')
                            .IntersectionIs('$')
                            .RightEndIs('%');
                    });
                    var text = writer.ToPlanText(new[]
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

                    var writer = TextTableWriter<User>.Build(config =>
                    {
                        config.AutoGenerateColumns = true;
                        config.Borders.Bottom.Disable();
                    });
                    var text = writer.ToPlanText(new[]
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

                    var writer = TextTableWriter<User>.Build(config =>
                    {
                        config.AutoGenerateColumns = true;
                        config.Borders.Bottom
                            .LeftEndIs('#')
                            .LineIs('=')
                            .IntersectionIs('$')
                            .RightEndIs('%');
                    });
                    var text = writer.ToPlanText(new[]
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

                    var writer = TextTableWriter<User>.Build(config =>
                    {
                        config.AutoGenerateColumns = true;
                        config.Borders.Left.Disable();
                    });
                    var text = writer.ToPlanText(new[]
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

                    var writer = TextTableWriter<User>.Build(config =>
                    {
                        config.AutoGenerateColumns = true;
                        config.Borders.Left.LineIs('\\');
                    });
                    var text = writer.ToPlanText(new[]
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

                    var writer = TextTableWriter<User>.Build(config =>
                    {
                        config.AutoGenerateColumns = true;
                        config.Borders.InsideVertical.Disable();
                    });
                    var text = writer.ToPlanText(new[]
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

                    var writer = TextTableWriter<User>.Build(config =>
                    {
                        config.AutoGenerateColumns = true;
                        config.Borders.InsideVertical.LineIs('\\');
                    });
                    var text = writer.ToPlanText(new[]
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

                    var writer = TextTableWriter<User>.Build(config =>
                    {
                        config.AutoGenerateColumns = true;
                        config.Borders.Right.Disable();
                    });
                    var text = writer.ToPlanText(new[]
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

                    var writer = TextTableWriter<User>.Build(config =>
                    {
                        config.AutoGenerateColumns = true;
                        config.Borders.Right.LineIs('\\');
                    });
                    var text = writer.ToPlanText(new[]
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
