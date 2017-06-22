namespace SampleTests
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using DevTeam.TestFramework;
    using Shouldly;

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
}
