# FluentTextTable

「オブジェクトを手軽にコンソールに出力したい」

そう思ったことはありませんか？

FluentTextTableを利用すると、全角にも対応したテキストテーブルを簡単に利用できます！

```csharp
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





![](images/borders.JPG)


![](images/horizontalBorder.JPG)

![](images/verticalBorder.JPG)
