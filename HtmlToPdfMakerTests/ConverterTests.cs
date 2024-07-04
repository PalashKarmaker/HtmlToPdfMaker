namespace HtmlToPdfMaker.Tests;

[TestClass()]
public class ConverterTests
{
    [TestMethod()]
    public void ToPdfTest()
    {
        List<ContentSet> contentSets = [];
        contentSets.Add(SetContents("<body><h3>Спокойной ночи</h3><p>शुभ रात्रि</p><p>Português para principiantes</p><hr><p>আমি </p></body>", "<body><div><b>Спокойной ночи</b></div></body>", "Test Page"));
        contentSets.Add(SetContents("<body><div><h1>Palash J Karmaker</h1></div></body>", "<body><h3><u>Header1</u></h3>", "My page"));
        using Convert cvt = new(contentSets);
        var data = cvt.ToPdfAsync(CancellationToken.None).Result;
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
}