﻿using DinkToPdf;
using System.Text;
using System.Text.RegularExpressions;
using Utility;

namespace HtmlToPdfMaker;
/// <summary>Class to convert html to Pdf</summary>
/// <example>
/// Usage:
/// <code>
/// [TestMethod()]
/// public void ToPdfTest()
/// {
///     List&lt;ContentSet&gt; contentSets = [];
///     contentSets.Add(SetContents("&lt;body&gt;&lt;h3&gt;Спокойной ночи&lt;/h3&gt;&lt;p&gt;शुभ रात्रि&lt;/p&gt;&lt;p&gt;Português para principiantes&lt;/p&gt;&lt;hr /&gt;&lt;p&gt;আমি &lt;/p&gt;&lt;/body&gt;", "&lt;body&gt;&lt;div&gt;&lt;b&gt;Спокойной ночи&lt;/b&gt;&lt;/div&gt;&lt;/body&gt;", "Test Page"));
///     contentSets.Add(SetContents("&lt;body&gt;&lt;div&gt;&lt;h1&gt;Palash J Karmaker&lt;/h1&gt;&lt;/div&gt;&lt;/body&gt;", "&lt;body&gt;&lt;h3&gt;&lt;u&gt;Header1&lt;/u&gt;&lt;/h3&gt;", "My page"));
///     using Convert cvt = new(contentSets);
///     var data = cvt.ToPdfAsync(CancellationToken.None).Result;
///     File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\Pdf\\test2.pdf", data);
///     Assert.IsTrue(data.Length &gt; 0);
///
///     static ContentSet SetContents(string bodyHtml, string headerHtml, string footerHtml)
///     {
///         var header = Content.CreateDefaultStyledHeader(headerHtml);
///         var footer = Content.CreateDefaultStyledFooter(footerHtml);
///         var body = Content.CreateDefaultStyledBody(bodyHtml);
///         return new(body, header, footer);
///     }
/// }</code>
/// </example>
/// <seealso cref="Utility.Disposable" />
public partial class Convert(IReadOnlyList<ContentSet> contents, string? tempRootFolder = null, Orientation orientation = Orientation.Portrait, PaperKind paperKind = PaperKind.A3) : Disposable
{
    readonly GlobalSettings globalSettings = new()
    {
        ColorMode = ColorMode.Color,
        Orientation = orientation,
        PaperSize = paperKind,
    };
    static readonly SynchronizedConverter cvt = new(new PdfTools());
    /// <summary>
    /// The tempFolder
    /// </summary>
    protected readonly string tempFolder = $"{tempRootFolder ?? AppDomain.CurrentDomain.BaseDirectory}\\Pdf\\{Ulid.NewUlid()}";
    /// <summary>
    /// Releases the resources.
    /// </summary>
    public override void ReleaseResources()
    {
        if (Directory.Exists(tempFolder))
            Directory.Delete(tempFolder, true);
    }
    /// <summary>
    /// Converts to pdf.
    /// </summary>
    /// <param name="token">The token.</param>
    /// <returns></returns>
    public async Task<byte[]> ToPdfAsync(CancellationToken token)
    {
        if (!Directory.Exists(tempFolder))
            Directory.CreateDirectory(tempFolder);
        List<ObjectSettings> objSettings = [];
        for (int i = 0; i < contents.Count; i++)
            objSettings.Add(await BuildObjectSettings(i, token));
        return GeneratePdf(objSettings);

        async Task<ObjectSettings> BuildObjectSettings(int counter, CancellationToken token)
        {
            var dir = $"{tempFolder}\\{counter}";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            ContentSet cs = contents[counter];
            var b = MakeTempFileAsync(dir, ContentType.Body, cs.Body.Html, cs.Body.Css, token);
            var h = MakeTempFileAsync(dir, ContentType.Header, cs.Header.Html, cs.Header.Css, token);
            var f = MakeTempFileAsync(dir, ContentType.Footer, cs.Footer.Html, cs.Footer.Css, token);
            //await Task.WhenAll(h, f, b).ConfigureAwait(false);
            return new ObjectSettings()
            {
                PagesCount = true,
                Page = await b,
                WebSettings = { DefaultEncoding = "utf-8", PrintMediaType = true },
                HeaderSettings = { HtmUrl = await h },
                FooterSettings = { HtmUrl = await f, Right = "Page [page] of [toPage]", FontSize = 9 }
            };
        }
        async Task<string> MakeTempFileAsync(string dir, ContentType contentType, string content, Uri cssPath, CancellationToken token)
        {
            var pngPattern = PngPattern();
            using HttpClient client = new();
            var counter = 1;
            content = pngPattern.Replace(content, m =>
            {                
                var imageSavePath = $"{tempFolder}\\{counter}.png";
                if (!File.Exists(imageSavePath))
                {
                    var u = new Uri(m.Value);
                    var data = u.IsFile ? File.ReadAllBytes(m.Value) : client.GetByteArrayAsync(u).Result;
                    if (m.Groups["ext"].Value.Equals("webp", StringComparison.CurrentCultureIgnoreCase))
                        data = ImageConverter.Convert.To(data);
                    File.WriteAllBytes(imageSavePath, data);
                }
                var c = new Uri(imageSavePath).ToString();
                counter++;
                return c;
            });
            StringBuilder sb = new("<!doctype html><html>");
            sb.Append($"<head>");
            var style = "* { font-family: \"Arial Unicode MS\", \"Lucida Sans Unicode\", \"DejaVu Sans\", \"Quivira\", \"Symbola\", \"Code2000\" ; }";
            sb.Append($"<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><style>{style}</style><link rel=\"stylesheet\" href=\"{cssPath}\">");
            sb.Append($"</head>");
            sb.Append(content);
            sb.Append("</html>");
            var path = $"{dir}\\{contentType}.html";
            await File.WriteAllTextAsync(path, sb.ToString(), token);
            return path;
        }
    }
    /// <summary>
    /// Generates the PDF.
    /// </summary>
    /// <param name="objSettings">The object settings.</param>
    /// <returns></returns>
    protected byte[] GeneratePdf(List<ObjectSettings> objSettings)
    {
        var doc = new HtmlToPdfDocument()
        {
            GlobalSettings = globalSettings
        };
        doc.Objects.AddRange(objSettings);
        return cvt.Convert(doc);
    }

    [GeneratedRegex("[\\w\\.\\/\\:\\-]+\\.(?<ext>(png)|(webp))", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture, "en-US")]
    private static partial Regex PngPattern();
}