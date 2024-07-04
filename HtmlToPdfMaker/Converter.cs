using DinkToPdf;
using System.Text;
using Utility;

namespace HtmlToPdfMaker;
/// <summary>
/// Class to convert html to Pdf
/// </summary>
/// <param name="headerRequired"></param>
/// <param name="footerRequired"></param>
/// <param name="tempRootFolder"></param>
public class Converter(Content body, Content header, Content footer, Orientation orientation = Orientation.Portrait, PaperKind paperKind = PaperKind.A3) : Disposable
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
        var h = MakeTempFileAsync(ContentType.Header, header.Html, header.Css, token);
        var f = MakeTempFileAsync(ContentType.Footer, footer.Html, header.Css, token);
        var b = MakeTempFileAsync(ContentType.Body, body.Html, header.Css, token);
        return GeneratePdf(new()
        {
            { ContentType.Body, await b },
            { ContentType.Header, await h },
            { ContentType.Footer, await f }
        });
    }
    protected async Task<string> MakeTempFileAsync(ContentType contentType, string content, Uri cssPath, CancellationToken token)
    {
        StringBuilder sb = new("<!doctype html><html>");
        sb.Append($"<head>");
        sb.Append($"<link rel=\"stylesheet\" href=\"{cssPath}\">");
        sb.Append($"</head>");
        sb.Append(content);
        sb.Append("</html>");
        var path = $"{tempFolder}\\{contentType}.html";
        await File.WriteAllTextAsync(path, sb.ToString(), token);
        return path;
    }
    protected byte[] GeneratePdf(Dictionary<ContentType, string> filePaths)
    {
        var doc = new HtmlToPdfDocument()
        {
            GlobalSettings = globalSettings,
            Objects =
            {
                new ObjectSettings() {
                    PagesCount = true,
                    Page = filePaths[ContentType.Body],
                    WebSettings = { DefaultEncoding = "utf-8", PrintMediaType = true },
                    HeaderSettings = { HtmUrl = filePaths[ContentType.Header] },
                    FooterSettings = { HtmUrl = filePaths[ContentType.Footer], Right = "Page [page] of [toPage]", FontSize = 9 }
                }
            }
        };
        return cvt.Convert(doc);
    }
}