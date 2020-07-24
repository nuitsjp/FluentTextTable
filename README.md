# FluentTextTable

Have you ever just wanted to output .NET object out to console?

FluentTextTable makes it easy to use a text table that also supports full-width characters!

```cs
var users = new[]
{
    new User {Id = 1, EnglishName = "Bill Gates", JapaneseName = "ビル・ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
    new User {Id = 2, EnglishName = "Steven Jobs", JapaneseName = "スティーブ・ジョブズ", Birthday = DateTime.Parse("1955/2/24")}
};
Build
    .TextTable<User>()
    .WriteLine(users);
```

![](images/basic.png)

The complex formatting of tables can be changed flexibly and fluently.

```cs
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
```

Horizontal and vertical alignment, multi-line cells, string formatting, border styles, margins, etc.

![](images/formatted.png)

And this supports markdowns as well.

```cs
Build
    .MarkdownTable<User>()
    .WriteLine(users);
```

![](images/markdown.png)

# Table of Contents

- [Quick Start](#quick-start)
- [Markdown format](#markdown-format)
- [Multi-line cell support](#multi-line-cell-support)
- Format
  - [Column format](#Column-format)
  - [Borders](#borders)



# Quick Start

NET Framework 4.0 (or higher) and .NET Standard 2.0 (or higher) are supported. Install and use it from [NuGet](https://www.nuget.org/packages/FluentTextTable).

```console
> Install-Package FluentTextTable
```

Define the class to be output.

```cs
public class User
{
    public int Id { get; set; }
    public string EnglishName { get; set; }
    public string JapaneseName { get; set; }
    public DateTime Birthday;
}
```

Use the Build class to create a table for the output class.

By default, all public properties and fields are included in the output.

```cs
var table = Build.TextTable<User>();
```

Create and output an object corresponding to a row.

```cs
var users = new[]
{
    new User {Id = 1, EnglishName = "Bill Gates", JapaneseName = "ビル・ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
    new User {Id = 2, EnglishName = "Steven Jobs", JapaneseName = "スティーブ・ジョブズ", Birthday = DateTime.Parse("1955/2/24")}
};
Build
    .TextTable<User>()
    .WriteLine(users);
```

![](images/basic.png)

# Markdown format

FluentTextTable supports the popular Markdown format.

```cs
Build
    .MarkdownTable<User>()
    .WriteLine(users);
```

![](images/markdown1.jpg)

You can also align to the center or right. See [Column format](#Column-Format) for details.

# Multi-line cell support

Supports line breaks in a single cell.

A new line is output in one of the following cases

- The string properties and field containing the line feed code
- Properties and fields defined in IEnumerable<object>, such as Array and List

```cs
private class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Birthday;
    public string Parents { get; set; }
    public string[] Occupations { get; set; }
}

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

var table = Build.TextTable<User>(builder =>
{
    builder
        .Columns.Add(x => x.Id).NameAs("ID").HorizontalAlignmentAs(HorizontalAlignment.Right)
        .Columns.Add(x => x.Name).VerticalAlignmentAs(VerticalAlignment.Center)
        .Columns.Add(x => x.Birthday).VerticalAlignmentAs(VerticalAlignment.Bottom).FormatAs("{0:yyyy/MM/dd}")
        .Columns.Add(x => x.Parents).VerticalAlignmentAs(VerticalAlignment.Center).FormatAs("- {0}")
        .Columns.Add(x => x.Occupations).HorizontalAlignmentAs(HorizontalAlignment.Center);
});
table.WriteLine(users);
```

![](images/multilinecell.png)

See [Column Format](#Column-Format) for details of the format.

In the case of markdown, it is output as a br tag.

```cs
var table = Build.MarkdownTable<User>(builder =>
{
    builder
        .Columns.Add(x => x.Id).NameAs("ID").HorizontalAlignmentAs(HorizontalAlignment.Right)
        .Columns.Add(x => x.Name).VerticalAlignmentAs(VerticalAlignment.Center)
        .Columns.Add(x => x.Birthday).VerticalAlignmentAs(VerticalAlignment.Bottom).FormatAs("{0:yyyy/MM/dd}")
        .Columns.Add(x => x.Parents).VerticalAlignmentAs(VerticalAlignment.Center).FormatAs("- {0}")
});
table.WriteLine(users);
```

![](images/multilinecellmarkdown.png)

Here's how it would look like

| ID | Name       | Birthday   | Parents                                  |
|---:|------------|------------|------------------------------------------|
|  1 | Bill Gates | 1955/10/28 | - Bill Gates Sr.<br>- Mary Maxwell Gates |

In Markdown, vertical alignment is not enabled.


# Format

## Column format

By default, all public properties and fields are included in the output.

```cs
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
}
```

![](images/columnformat.png)

Output columns can also be specified.

```cs
Build
    .TextTable<User>(builder =>
    {
        builder
            .Columns.Add(x => x.Name)
            .Columns.Add(x => x.Birthday);
    })
    .WriteLine(users);
```

![](images/columnformatspecifyproperties.png)

The columns can be formatted as follows

- Horizontal Alignment
- Vertical Alignment
- Header name
- Format

```cs
var table = Build.TextTable<User>(builder =>
{
    builder
        .Columns.Add(x => x.Id).NameAs("ID").HorizontalAlignmentAs(HorizontalAlignment.Right)
        .Columns.Add(x => x.Name).VerticalAlignmentAs(VerticalAlignment.Center)
        .Columns.Add(x => x.Birthday).VerticalAlignmentAs(VerticalAlignment.Bottom).FormatAs("{0:yyyy/MM/dd}")
        .Columns.Add(x => x.Parents).VerticalAlignmentAs(VerticalAlignment.Center).FormatAs("- {0}")
        .Columns.Add(x => x.Occupations).HorizontalAlignmentAs(HorizontalAlignment.Center);
});
table.WriteLine(users);
```

Note that the cells are formatted row by row.

![](images/columnformatdetail.png)


## Borders

All borders can be changed to any style (Markdown format is not supported).

```cs
var table = Build.TextTable<User>(builder =>
{
    builder
        .Borders.Top
            .LeftStyleAs("-")
            .IntersectionStyleAs("-")
            .RightStyleAs("-")
        .Borders.HeaderHorizontal
            .LineStyleAs("=")
        .Borders.InsideHorizontal
            .AsDisable()
        .Borders.Bottom
            .LeftStyleAs("*")
            .IntersectionStyleAs("*")
            .RightStyleAs("*");
});
```

![](images/borders.png)

Borders can also be applied collectively as a style.

```cs
Build
    .TextTable<User>(builder =>
    {
        builder
            .Borders.Horizontals.AllStylesAs("-")
            .Borders.InsideHorizontal.AllStylesAs("=")
            .Borders.Verticals.LineStyleAs("$");
    })
    .WriteLine(users);
```

![](images/borderapplyall.png)

The following areas are defined for the borders

- Top
- HeaderHorizontal
- InsideHorizontal
- Bottom
- Left
- InsideVertical
- Right

![](images/borders.JPG)

The horizontal border can be changed in the following styles

- LeftStyle
- IntersectionStyle
- RightStyle
- LineStyle

![](images/horizontalBorder.JPG)

The vertical border can only be changed by LineStyle. Intersections are determined by the horizontal border.

![](images/verticalBorder.JPG)
