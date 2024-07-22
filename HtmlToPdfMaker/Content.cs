namespace HtmlToPdfMaker;

public class Content(string htmlContent, Uri css)
{
    public string Html { get; set; } = string.IsNullOrWhiteSpace(htmlContent) ? "<body></body>" : htmlContent;
    public Uri Css { get; set; } = css;

    protected Content(string htmlContent, ContentType contentType) : this(htmlContent, new Uri(AppDomain.CurrentDomain.BaseDirectory + $"\\{contentType}.css"))
    {
    }
    //public static Content CreateHeader(string htmlContent, Uri css) => new(htmlContent, ContentType.Header);

    //public static Content CreateFooter(string htmlContent) => new(htmlContent, ContentType.Footer);

    //public static Content CreateBody(string htmlContent) => new(htmlContent, ContentType.Body);
    public static Content CreateDefaultStyledHeader(string htmlContent) => new(htmlContent, ContentType.Header);

    public static Content CreateDefaultStyledFooter(string htmlContent) => new(htmlContent, ContentType.Footer);

    public static Content CreateDefaultStyledBody(string htmlContent) => new(htmlContent, ContentType.Body);
}
public record ContentSet(Content Body, Content Header, Content Footer);
public record PathSet(string Body, string Header, string Footer);
