namespace HtmlToPdfMaker;

public class Content(string htmlContent, Uri css)
{
    public string Html { get; set; } = string.IsNullOrWhiteSpace(htmlContent) ? "<body></body>" : htmlContent;
    public Uri Css { get; set; } = css;

    protected Content(string htmlContent, ContentType contentType) : this(htmlContent, new Uri(AppDomain.CurrentDomain.BaseDirectory + $"\\{contentType}.css"))
    {
    }

    public static Content CreateDefaultStyledHeader(string htmlContent) => new(htmlContent, ContentType.Header);

    public static Content CreateDefaultStyledFooter(string htmlContent) => new(htmlContent, ContentType.Footer);

    public static Content CreateDefaultStyledBody(string htmlContent) => new(htmlContent, ContentType.Body);
}