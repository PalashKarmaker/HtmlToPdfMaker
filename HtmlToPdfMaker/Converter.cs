using DinkToPdf;

namespace HtmlToPdfMaker;

public class Converter(bool headerRequired, bool footerRequired) : SynchronizedConverter(new PdfTools())
{
    protected readonly string directory = ".\\Pdf\\";
    public byte[] ToPdf(string body, string header = "", string footer = "")
    {
        var id = Ulid.NewUlid().ToString();
        (string body, string header, string footer) tempPaths = 
            ($"{directory}b_{id}.html", 
                headerRequired? $"{directory}h_{id}.html": string.Empty, 
                footerRequired? 
                    footer.Length == 0? $"{directory}Footer.html": $"{directory}f_{id}.html"
                        : string.Empty);
        File.WriteAllText(tempPaths.header, GetHtmlSnippet("header.css", header));
        File.WriteAllText(tempPaths.body, GetHtmlSnippet("body.css", body));
        return GeneratePdf(tempPaths);

        string GetHtmlHeadSnippet(string css) => $"<!doctype html><html><head><link rel=\"stylesheet\" href=\"{css}\"></head>";
        string GetHtmlSnippet(string css, string content) => $"{GetHtmlHeadSnippet(css)}{content}</html>";
    }

    protected byte[] GeneratePdf((string header, string body, string footer) tempPaths)
    {
        var doc = new HtmlToPdfDocument()
        {
            GlobalSettings =
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A3,
            },
            Objects =
            {
                new ObjectSettings() {
                    PagesCount = true,
                    Page = tempPaths.body,
                    WebSettings = { DefaultEncoding = "utf-8", PrintMediaType = true },
                    HeaderSettings = { HtmUrl = tempPaths.header },
                    FooterSettings = { HtmUrl = tempPaths.footer, Right = "Page [page] of [toPage]", FontSize = 9 }
                }
            }
        };
        return Convert(doc);        
    }
}