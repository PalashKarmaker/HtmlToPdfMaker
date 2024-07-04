using DinkToPdf;
using System.Text;
using Utility;

namespace HtmlToPdfMaker;
/// <summary>
/// Class to convert html to Pdf
/// </summary>
/// <seealso cref="Utility.Disposable" />
public class Convert(IReadOnlyList<ContentSet> contents, Orientation orientation = Orientation.Portrait, PaperKind paperKind = PaperKind.A3) : Disposable
{
    GlobalSettings globalSettings = new()
    {
        ColorMode = ColorMode.Color,
        Orientation = orientation,
        PaperSize = paperKind,
    };
    static readonly string tempRootFolder = $"{AppDomain.CurrentDomain.BaseDirectory}\\Pdf\\";
    static SynchronizedConverter cvt = new(new PdfTools());
    /// <summary>
    /// The tempFolder
    /// </summary>
    protected readonly string tempFolder = $"{tempRootFolder}\\{Ulid.NewUlid()}";
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
            var h = MakeTempFileAsync(dir, ContentType.Header, cs.Header.Html, cs.Header.Css, token);
            var f = MakeTempFileAsync(dir, ContentType.Footer, cs.Footer.Html, cs.Footer.Css, token);
            var b = MakeTempFileAsync(dir, ContentType.Body, cs.Body.Html, cs.Body.Css, token);
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
            StringBuilder sb = new("<!doctype html><html>");
            sb.Append($"<head>");
            sb.Append($"<link rel=\"stylesheet\" href=\"{cssPath}\">");
            sb.Append($"</head>");
            sb.Append(content);
            sb.Append("</html>");
            var path = $"{dir}\\{contentType}.html";
            await File.WriteAllTextAsync(path, sb.ToString(), token);
            return path;
        }
    }
    protected byte[] GeneratePdf(List<ObjectSettings> objSettings)
    {
        var doc = new HtmlToPdfDocument()
        {
            GlobalSettings = globalSettings
        };
        doc.Objects.AddRange(objSettings);
        return cvt.Convert(doc);
    }
}