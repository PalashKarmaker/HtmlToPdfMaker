using System.Diagnostics;

namespace HtmlToPdfMaker.Tests;

[TestClass()]
public class ConverterTests
{
    readonly string rootDirectory = $"C:\\Users\\uie37359\\Downloads\\HtmlToPdfSample\\";
    [TestMethod]
    public void ToPdfTest()
    {
        List<ContentSet> contentSets = [];
        contentSets.Add(SetContents("<body><h3>Спокойной ночи</h3><p>शुभ रात्रि</p><p>Português para principiantes</p><hr><p>আমি </p></body>", "<body><div><b>Спокойной ночи</b></div></body>", "Test Page"));
        contentSets.Add(SetContents("<body><div><h1>Palash J Karmaker</h1></div></body>", "<body><h3><u>Header1</u></h3>", "My page"));
        using Convert cvt = new(contentSets);
        var data = cvt.ToPdfAsync().Result;
        File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\Pdf\\test2.pdf", data);
        Assert.IsTrue(data.Length > 0);

        static ContentSet SetContents(string bodyHtml, string headerHtml, string footerHtml)
        {
            var header = Content.CreateDefaultStyledHeader(headerHtml);
            var footer = Content.CreateDefaultStyledFooter(footerHtml);
            var body = Content.CreateDefaultStyledBody(bodyHtml);
            return new(body, header, footer);
        }
    }
    [DataTestMethod]
    [DataRow("")]
    public void ToPdfWithUrlTest(string? language = null)
    {
        List<ContentSet> contentSets = [];
        GetContents(language, out var bodyHtml, out var headerHtml, out var footerHtml);
        contentSets.Add(SetContents(bodyHtml, headerHtml, footerHtml));
        using Convert cvt = new(contentSets);
        var data = cvt.ToPdfAsync().Result;
        File.WriteAllBytes($"{rootDirectory}test_{Ulid.NewUlid()}.pdf", data);
        Assert.IsTrue(data.Length > 0);
    }    
    [DataTestMethod]
    [DataRow("")]
    public void ToDualPdfWithUrlTest(string? language = null)
    {
        List<ContentSet> contentSets = [];
        GetContents(language, out var bodyHtml, out var headerHtml, out var footerHtml);
        contentSets.Add(SetContents(bodyHtml, headerHtml, footerHtml));
        GetContents("Eng", out var bodyEngHtml, out var headerEngHtml, out var footerEngHtml);
        contentSets.Add(SetContents(bodyEngHtml, headerEngHtml, footerEngHtml));
        using Convert cvt = new(contentSets);
        Stopwatch w = Stopwatch.StartNew();
        var data = cvt.ToPdfAsync().Result;
        w.Stop();
        File.WriteAllBytes($"{rootDirectory}dual_test_{Ulid.NewUlid()}.pdf", data);
        Console.WriteLine($"Time taken: {w.Elapsed.TotalSeconds} sec(s)");
        Assert.IsTrue(data.Length > 0);
    }
    ContentSet SetContents(string bodyHtml, string headerHtml, string footerHtml)
    {
        var header = new Content(headerHtml, new($"{rootDirectory}header.css"));
        var footer = new Content(footerHtml, new($"{rootDirectory}footer.css"));
        var body = new Content(bodyHtml, new($"{rootDirectory}body.css"));
        return new(body, header, footer);
    }
    void GetContents(string? language, out string bodyHtml, out string headerHtml, out string footerHtml)
    {
        var suffix = string.IsNullOrWhiteSpace(language) ? string.Empty : $"_{language}";
        bodyHtml = File.ReadAllText($"{rootDirectory}body{suffix}.html");
        headerHtml = File.ReadAllText($"{rootDirectory}header{suffix}.html");
        footerHtml = File.ReadAllText($"{rootDirectory}footer{suffix}.html");
    }
}