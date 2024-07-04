[# HtmlToPdfMaker](https://github.com/PalashKarmaker/HtmlToPdfMaker/wiki)
<a name='assembly'></a>
# HtmlToPdfMaker

## Contents

- [Convert](#T-HtmlToPdfMaker-Convert 'HtmlToPdfMaker.Convert')
  - [#ctor()](#M-HtmlToPdfMaker-Convert-#ctor-System-Collections-Generic-IReadOnlyList{HtmlToPdfMaker-ContentSet},System-String,DinkToPdf-Orientation,DinkToPdf-PaperKind- 'HtmlToPdfMaker.Convert.#ctor(System.Collections.Generic.IReadOnlyList{HtmlToPdfMaker.ContentSet},System.String,DinkToPdf.Orientation,DinkToPdf.PaperKind)')
  - [tempFolder](#F-HtmlToPdfMaker-Convert-tempFolder 'HtmlToPdfMaker.Convert.tempFolder')
  - [GeneratePdf(objSettings)](#M-HtmlToPdfMaker-Convert-GeneratePdf-System-Collections-Generic-List{DinkToPdf-ObjectSettings}- 'HtmlToPdfMaker.Convert.GeneratePdf(System.Collections.Generic.List{DinkToPdf.ObjectSettings})')
  - [ReleaseResources()](#M-HtmlToPdfMaker-Convert-ReleaseResources 'HtmlToPdfMaker.Convert.ReleaseResources')
  - [ToPdfAsync(token)](#M-HtmlToPdfMaker-Convert-ToPdfAsync-System-Threading-CancellationToken- 'HtmlToPdfMaker.Convert.ToPdfAsync(System.Threading.CancellationToken)')

<a name='T-HtmlToPdfMaker-Convert'></a>
## Convert `type`

##### Namespace

HtmlToPdfMaker

##### Summary

Class to convert html to Pdf

##### Example

Usage:

```
 [TestMethod()]
 public void ToPdfTest()
 {
     List&lt;ContentSet&gt; contentSets = [];
     contentSets.Add(SetContents("&lt;body&gt;&lt;h3&gt;Спокойной ночи&lt;/h3&gt;&lt;p&gt;शुभ रात्रि&lt;/p&gt;&lt;p&gt;Português para principiantes&lt;/p&gt;&lt;hr /&gt;&lt;p&gt;আমি &lt;/p&gt;&lt;/body&gt;", "&lt;body&gt;&lt;div&gt;&lt;b&gt;Спокойной ночи&lt;/b&gt;&lt;/div&gt;&lt;/body&gt;", "Test Page"));
     contentSets.Add(SetContents("&lt;body&gt;&lt;div&gt;&lt;h1&gt;Palash J Karmaker&lt;/h1&gt;&lt;/div&gt;&lt;/body&gt;", "&lt;body&gt;&lt;h3&gt;&lt;u&gt;Header1&lt;/u&gt;&lt;/h3&gt;", "My page"));
     using Convert cvt = new(contentSets);
     var data = cvt.ToPdfAsync(CancellationToken.None).Result;
     File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\Pdf\\test2.pdf", data);
     Assert.IsTrue(data.Length &gt; 0);
     static ContentSet SetContents(string bodyHtml, string headerHtml, string footerHtml)
     {
         var header = Content.CreateDefaultStyledHeader(headerHtml);
         var footer = Content.CreateDefaultStyledFooter(footerHtml);
         var body = Content.CreateDefaultStyledBody(bodyHtml);
         return new(body, header, footer);
     }
 }
```

##### See Also

- [Utility.Disposable](#T-Utility-Disposable 'Utility.Disposable')

<a name='M-HtmlToPdfMaker-Convert-#ctor-System-Collections-Generic-IReadOnlyList{HtmlToPdfMaker-ContentSet},System-String,DinkToPdf-Orientation,DinkToPdf-PaperKind-'></a>
### #ctor() `constructor`

##### Summary

Class to convert html to Pdf

##### Parameters

This constructor has no parameters.

##### Example

Usage:

```
 [TestMethod()]
 public void ToPdfTest()
 {
     List&lt;ContentSet&gt; contentSets = [];
     contentSets.Add(SetContents("&lt;body&gt;&lt;h3&gt;Спокойной ночи&lt;/h3&gt;&lt;p&gt;शुभ रात्रि&lt;/p&gt;&lt;p&gt;Português para principiantes&lt;/p&gt;&lt;hr /&gt;&lt;p&gt;আমি &lt;/p&gt;&lt;/body&gt;", "&lt;body&gt;&lt;div&gt;&lt;b&gt;Спокойной ночи&lt;/b&gt;&lt;/div&gt;&lt;/body&gt;", "Test Page"));
     contentSets.Add(SetContents("&lt;body&gt;&lt;div&gt;&lt;h1&gt;Palash J Karmaker&lt;/h1&gt;&lt;/div&gt;&lt;/body&gt;", "&lt;body&gt;&lt;h3&gt;&lt;u&gt;Header1&lt;/u&gt;&lt;/h3&gt;", "My page"));
     using Convert cvt = new(contentSets);
     var data = cvt.ToPdfAsync(CancellationToken.None).Result;
     File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\Pdf\\test2.pdf", data);
     Assert.IsTrue(data.Length &gt; 0);
     static ContentSet SetContents(string bodyHtml, string headerHtml, string footerHtml)
     {
         var header = Content.CreateDefaultStyledHeader(headerHtml);
         var footer = Content.CreateDefaultStyledFooter(footerHtml);
         var body = Content.CreateDefaultStyledBody(bodyHtml);
         return new(body, header, footer);
     }
 }
```

##### See Also

- [Utility.Disposable](#T-Utility-Disposable 'Utility.Disposable')

<a name='F-HtmlToPdfMaker-Convert-tempFolder'></a>
### tempFolder `constants`

##### Summary

The tempFolder

<a name='M-HtmlToPdfMaker-Convert-GeneratePdf-System-Collections-Generic-List{DinkToPdf-ObjectSettings}-'></a>
### GeneratePdf(objSettings) `method`

##### Summary

Generates the PDF.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| objSettings | [System.Collections.Generic.List{DinkToPdf.ObjectSettings}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{DinkToPdf.ObjectSettings}') | The object settings. |

<a name='M-HtmlToPdfMaker-Convert-ReleaseResources'></a>
### ReleaseResources() `method`

##### Summary

Releases the resources.

##### Parameters

This method has no parameters.

<a name='M-HtmlToPdfMaker-Convert-ToPdfAsync-System-Threading-CancellationToken-'></a>
### ToPdfAsync(token) `method`

##### Summary

Converts to pdf.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The token. |
