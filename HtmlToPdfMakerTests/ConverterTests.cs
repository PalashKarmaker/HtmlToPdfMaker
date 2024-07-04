namespace HtmlToPdfMaker.Tests;

[TestClass()]
public class ConverterTests
{
    [TestMethod()]
    public void ToPdfTest()
    {
        List<ContentSet> contentSets = [];
        contentSets.Add(SetContents("<div><h1>Palash J Karmaker</h1></div>", "<h3><u>Header1</u></h3>"));
        contentSets.Add(SetContents("<div><b>Hello world</b></div>", "<h3><u>Header2</u></h3>"));
        using Convert cvt = new(contentSets);
        var data = cvt.ToPdfAsync(CancellationToken.None).Result;
        File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\test2.pdf", data);
        Assert.IsTrue(data.Length > 0);

        static ContentSet SetContents(string bodyHtml, string headerHtml)
        {
            var header = Content.CreateDefaultStyledHeader(headerHtml);
            var footer = Content.CreateDefaultStyledFooter(string.Empty);
            var body = Content.CreateDefaultStyledBody(bodyHtml);
            return new(body, header, footer);
        }
    }
}