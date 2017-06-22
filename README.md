[<img src="http://tcavs2015.cloudapp.net/app/rest/builds/buildType:(id:DevTeam_TestTool_Build)/statusIcon"/>](http://tcavs2015.cloudapp.net/viewType.html?buildTypeId=DevTeam_TestTool_Build) [<img src="https://www.nuget.org/Content/Logos/nugetlogo.png" height="18">](https://github.com/DevTeam/TestTool/wiki/NuGet-packages)

# Simple and flexibly test framework for .NET

Supports:
* [Visual Studio Test Platform](https://github.com/Microsoft/vstest)
* .NET 3.5+
* .NET Core 1.0+
* .NET Standard 1.0+

For example this test class contains 30 tests for all specified variants.

```csharp
    using DevTeam.TestFramework;
    
    [Test.Types(typeof(BriefReport))]
    [Test.Types(typeof(DetailedReport))]
    [Test.Args("ru-RU")]
    [Test.Args("en-US")]
    [Test.Args("en-GB")]
    class ReportTests<TReport> where TReport : IReport, new()
    {
        private readonly CultureInfo _cultureInfo;

        public ReportTests(string cultureInfoName) { _cultureInfo = new CultureInfo(cultureInfoName); }

        [Test.Args(new[] { 1, 2, 3 }, "Totals: 6")]
        [Test.Args(new[] { 1 }, "Totals: 1")]
        [Test.Args(new[] { 0 }, "Totals: 0")]
        [Test.Args(new int[0], "Totals: 0")]
        [Test.Args(new[] { 1, 2, -3 }, "Totals: 0")]
        public void ShouldRepresentTotals(IEnumerable<int> prices, string expectedTotals)
        {
            // Given
            var report = new TReport();

            // When
            var text = report.Create(_cultureInfo, prices);

            // Then
            text.ShouldContain(expectedTotals);
        }
    }    
```

<details>
  <summary>Testing targets</summary>

```csharp 
    interface IReport
    {
        IEnumerable<string> Create(CultureInfo cultureInfo, IEnumerable<int> prices);
    }

    class BriefReport : IReport
    {
        public IEnumerable<string> Create(CultureInfo cultureInfo, IEnumerable<int> prices)
        {
            yield return $"Totals: {prices.Sum().ToString(cultureInfo)}";
        }
    }

    class DetailedReport : IReport
    {
        public IEnumerable<string> Create(CultureInfo cultureInfo, IEnumerable<int> prices)
        {
            var totals = 0;
            foreach (var price in prices)
            {
                yield return price.ToString(cultureInfo);
                totals += price;
            }

            yield return $"Totals: {totals.ToString(cultureInfo)}";
        }
    }    
```
</details>
