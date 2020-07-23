# FluentTextTable

「オブジェクトを手軽にコンソールに出力したい」そう思ったことはありませんか？

FluentTextTableを利用すると、全角にも対応したテキストテーブルを簡単に利用できます！

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

![](images/sample1.jpg)

テーブルの書式は簡単かつ流暢（Fluent）に変更できます。

行間の罫線を非表示にし、項目値の書式を指定します。

```cs
Build
    .TextTable<User>(builder =>
    {
        builder
            .Borders.InsideHorizontal.AsDisable()
            .Columns.Add(x => x.Id).HorizontalAlignmentAs(HorizontalAlignment.Right)
            .Columns.Add(x => x.Name).NameAs("氏名")
            .Columns.Add(x => x.Birthday).FormatAs("{0:yyyy/MM/dd}");
    })
    .WriteLine(users);
```

![](images/sample2.jpg)

# 目次

- [Quick Start](#quick-start)


# Quick Start

.NET Framework 4.0以上、.NET Standard 2.0以上をサポートしています。[NuGet](https://www.nuget.org/packages/FluentTextTable)からインストールして利用してください。

```console
> Install-Package FluentTextTable
```

出力対象となるクラスを定義します。

```cs
private class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Birthday;
}
```

Buildクラスを利用して出力対象クラス用のテーブルを作成します。

デフォルトではpublicなプロパティ・フィールドのすべてが出力の対象となります。

```cs
var table = Build.TextTable<User>();
```

行に該当するオブジェクトを生成して、出力します。

```cs
var users = new[]
{
    new User {Id = 1, Name = "ビル ゲイツ", Birthday = DateTime.Parse("1955/10/28")},
    new User {Id = 2, Name = "Steven Jobs", Birthday = DateTime.Parse("1955/2/24")}
};
table.WriteLine(users);
```

![](images/sample1.jpg)









![](images/borders.JPG)


![](images/horizontalBorder.JPG)

![](images/verticalBorder.JPG)
