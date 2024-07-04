using System.Reflection;

namespace HtmlToPdfMaker.Tests
{
    [TestClass()]
    public class ConverterTests
    {
        [TestMethod()]
        public void ToPdfTest()
        {
            var path = Environment.CurrentDirectory;
            var header = Content.CreateDefaultStyledHeader(string.Empty);
            var footer = Content.CreateDefaultStyledFooter(string.Empty);
            var body = Content.CreateDefaultStyledBody("<div><b>Hello world</b></div>");
            using Converter cvt = new(body, header, footer);
            var data = cvt.ToPdfAsync(CancellationToken.None).Result;
            File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\test2.pdf", data);
            Assert.IsTrue(data.Length > 0);
        }
    }
}