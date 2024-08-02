<a name='assembly'></a>
# HtmlToPdfMaker

## Contents

- [Convert](#T-HtmlToPdfMaker-Convert 'HtmlToPdfMaker.Convert')
  - [#ctor()](#M-HtmlToPdfMaker-Convert-#ctor-System-Collections-Generic-IReadOnlyList{HtmlToPdfMaker-ContentSet},System-String,DinkToPdf-Orientation,DinkToPdf-PaperKind- 'HtmlToPdfMaker.Convert.#ctor(System.Collections.Generic.IReadOnlyList{HtmlToPdfMaker.ContentSet},System.String,DinkToPdf.Orientation,DinkToPdf.PaperKind)')
  - [tempFolder](#F-HtmlToPdfMaker-Convert-tempFolder 'HtmlToPdfMaker.Convert.tempFolder')
  - [GeneratePdf(objSettings)](#M-HtmlToPdfMaker-Convert-GeneratePdf-System-Collections-Generic-List{DinkToPdf-ObjectSettings}- 'HtmlToPdfMaker.Convert.GeneratePdf(System.Collections.Generic.List{DinkToPdf.ObjectSettings})')
  - [PngPattern()](#M-HtmlToPdfMaker-Convert-PngPattern 'HtmlToPdfMaker.Convert.PngPattern')
  - [ReleaseResources()](#M-HtmlToPdfMaker-Convert-ReleaseResources 'HtmlToPdfMaker.Convert.ReleaseResources')
  - [ToPdfAsync(token)](#M-HtmlToPdfMaker-Convert-ToPdfAsync-System-Threading-CancellationToken- 'HtmlToPdfMaker.Convert.ToPdfAsync(System.Threading.CancellationToken)')
- [PngPattern_0](#T-System-Text-RegularExpressions-Generated-PngPattern_0 'System.Text.RegularExpressions.Generated.PngPattern_0')
  - [#ctor()](#M-System-Text-RegularExpressions-Generated-PngPattern_0-#ctor 'System.Text.RegularExpressions.Generated.PngPattern_0.#ctor')
  - [Instance](#F-System-Text-RegularExpressions-Generated-PngPattern_0-Instance 'System.Text.RegularExpressions.Generated.PngPattern_0.Instance')
- [Runner](#T-System-Text-RegularExpressions-Generated-PngPattern_0-RunnerFactory-Runner 'System.Text.RegularExpressions.Generated.PngPattern_0.RunnerFactory.Runner')
  - [Scan(inputSpan)](#M-System-Text-RegularExpressions-Generated-PngPattern_0-RunnerFactory-Runner-Scan-System-ReadOnlySpan{System-Char}- 'System.Text.RegularExpressions.Generated.PngPattern_0.RunnerFactory.Runner.Scan(System.ReadOnlySpan{System.Char})')
  - [TryFindNextPossibleStartingPosition(inputSpan)](#M-System-Text-RegularExpressions-Generated-PngPattern_0-RunnerFactory-Runner-TryFindNextPossibleStartingPosition-System-ReadOnlySpan{System-Char}- 'System.Text.RegularExpressions.Generated.PngPattern_0.RunnerFactory.Runner.TryFindNextPossibleStartingPosition(System.ReadOnlySpan{System.Char})')
  - [TryMatchAtCurrentPosition(inputSpan)](#M-System-Text-RegularExpressions-Generated-PngPattern_0-RunnerFactory-Runner-TryMatchAtCurrentPosition-System-ReadOnlySpan{System-Char}- 'System.Text.RegularExpressions.Generated.PngPattern_0.RunnerFactory.Runner.TryMatchAtCurrentPosition(System.ReadOnlySpan{System.Char})')
- [RunnerFactory](#T-System-Text-RegularExpressions-Generated-PngPattern_0-RunnerFactory 'System.Text.RegularExpressions.Generated.PngPattern_0.RunnerFactory')
  - [CreateInstance()](#M-System-Text-RegularExpressions-Generated-PngPattern_0-RunnerFactory-CreateInstance 'System.Text.RegularExpressions.Generated.PngPattern_0.RunnerFactory.CreateInstance')
- [Utilities](#T-System-Text-RegularExpressions-Generated-Utilities 'System.Text.RegularExpressions.Generated.Utilities')
  - [s_ascii_FFFFFFFFFF1F00F801000078010000F8](#F-System-Text-RegularExpressions-Generated-Utilities-s_ascii_FFFFFFFFFF1F00F801000078010000F8 'System.Text.RegularExpressions.Generated.Utilities.s_ascii_FFFFFFFFFF1F00F801000078010000F8')
  - [s_defaultTimeout](#F-System-Text-RegularExpressions-Generated-Utilities-s_defaultTimeout 'System.Text.RegularExpressions.Generated.Utilities.s_defaultTimeout')
  - [s_hasTimeout](#F-System-Text-RegularExpressions-Generated-Utilities-s_hasTimeout 'System.Text.RegularExpressions.Generated.Utilities.s_hasTimeout')
  - [IndexOfNonAsciiOrAny_8894CC54583B80A14EAD0C952FCDE8DDC1F405D966650724E260E131D3AFE36B()](#M-System-Text-RegularExpressions-Generated-Utilities-IndexOfNonAsciiOrAny_8894CC54583B80A14EAD0C952FCDE8DDC1F405D966650724E260E131D3AFE36B-System-ReadOnlySpan{System-Char}- 'System.Text.RegularExpressions.Generated.Utilities.IndexOfNonAsciiOrAny_8894CC54583B80A14EAD0C952FCDE8DDC1F405D966650724E260E131D3AFE36B(System.ReadOnlySpan{System.Char})')

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

<a name='M-HtmlToPdfMaker-Convert-PngPattern'></a>
### PngPattern() `method`

##### Parameters

This method has no parameters.

##### Remarks

Pattern:

```
[\\w\\.\\/\\:\\-]+\\.png
```

Options:

```
RegexOptions.IgnoreCase
```

Explanation:

```
○ Match a character in the set [--/:\w] greedily at least once.
```

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

<a name='T-System-Text-RegularExpressions-Generated-PngPattern_0'></a>
## PngPattern_0 `type`

##### Namespace

System.Text.RegularExpressions.Generated

##### Summary

Custom [Regex](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.RegularExpressions.Regex 'System.Text.RegularExpressions.Regex')-derived type for the PngPattern method.

<a name='M-System-Text-RegularExpressions-Generated-PngPattern_0-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes the instance.

##### Parameters

This constructor has no parameters.

<a name='F-System-Text-RegularExpressions-Generated-PngPattern_0-Instance'></a>
### Instance `constants`

##### Summary

Cached, thread-safe singleton instance.

<a name='T-System-Text-RegularExpressions-Generated-PngPattern_0-RunnerFactory-Runner'></a>
## Runner `type`

##### Namespace

System.Text.RegularExpressions.Generated.PngPattern_0.RunnerFactory

##### Summary

Provides the runner that contains the custom logic implementing the specified regular expression.

<a name='M-System-Text-RegularExpressions-Generated-PngPattern_0-RunnerFactory-Runner-Scan-System-ReadOnlySpan{System-Char}-'></a>
### Scan(inputSpan) `method`

##### Summary

Scan the `inputSpan` starting from base.runtextstart for the next match.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| inputSpan | [System.ReadOnlySpan{System.Char}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ReadOnlySpan 'System.ReadOnlySpan{System.Char}') | The text being scanned by the regular expression. |

<a name='M-System-Text-RegularExpressions-Generated-PngPattern_0-RunnerFactory-Runner-TryFindNextPossibleStartingPosition-System-ReadOnlySpan{System-Char}-'></a>
### TryFindNextPossibleStartingPosition(inputSpan) `method`

##### Summary

Search `inputSpan` starting from base.runtextpos for the next location a match could possibly start.

##### Returns

true if a possible match was found; false if no more matches are possible.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| inputSpan | [System.ReadOnlySpan{System.Char}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ReadOnlySpan 'System.ReadOnlySpan{System.Char}') | The text being scanned by the regular expression. |

<a name='M-System-Text-RegularExpressions-Generated-PngPattern_0-RunnerFactory-Runner-TryMatchAtCurrentPosition-System-ReadOnlySpan{System-Char}-'></a>
### TryMatchAtCurrentPosition(inputSpan) `method`

##### Summary

Determine whether `inputSpan` at base.runtextpos is a match for the regular expression.

##### Returns

true if the regular expression matches at the current position; otherwise, false.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| inputSpan | [System.ReadOnlySpan{System.Char}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ReadOnlySpan 'System.ReadOnlySpan{System.Char}') | The text being scanned by the regular expression. |

<a name='T-System-Text-RegularExpressions-Generated-PngPattern_0-RunnerFactory'></a>
## RunnerFactory `type`

##### Namespace

System.Text.RegularExpressions.Generated.PngPattern_0

##### Summary

Provides a factory for creating [RegexRunner](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.RegularExpressions.RegexRunner 'System.Text.RegularExpressions.RegexRunner') instances to be used by methods on [Regex](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.RegularExpressions.Regex 'System.Text.RegularExpressions.Regex').

<a name='M-System-Text-RegularExpressions-Generated-PngPattern_0-RunnerFactory-CreateInstance'></a>
### CreateInstance() `method`

##### Summary

Creates an instance of a [RegexRunner](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.RegularExpressions.RegexRunner 'System.Text.RegularExpressions.RegexRunner') used by methods on [Regex](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.RegularExpressions.Regex 'System.Text.RegularExpressions.Regex').

##### Parameters

This method has no parameters.

<a name='T-System-Text-RegularExpressions-Generated-Utilities'></a>
## Utilities `type`

##### Namespace

System.Text.RegularExpressions.Generated

##### Summary

Helper methods used by generated [Regex](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.RegularExpressions.Regex 'System.Text.RegularExpressions.Regex')-derived implementations.

<a name='F-System-Text-RegularExpressions-Generated-Utilities-s_ascii_FFFFFFFFFF1F00F801000078010000F8'></a>
### s_ascii_FFFFFFFFFF1F00F801000078010000F8 `constants`

##### Summary

Supports searching for characters in or not in "\0\u0001\u0002\u0003\u0004\u0005\u0006\a\b\t\n\v\f\r\u000e\u000f\u0010\u0011\u0012\u0013\u0014\u0015\u0016\u0017\u0018\u0019\u001a\u001b\u001c\u001d\u001e\u001f !\"#$%&'()*+,;<=>?@[\\]^\`{|}~\u007f".

<a name='F-System-Text-RegularExpressions-Generated-Utilities-s_defaultTimeout'></a>
### s_defaultTimeout `constants`

##### Summary

Default timeout value set in [AppContext](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.AppContext 'System.AppContext'), or [InfiniteMatchTimeout](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout 'System.Text.RegularExpressions.Regex.InfiniteMatchTimeout') if none was set.

<a name='F-System-Text-RegularExpressions-Generated-Utilities-s_hasTimeout'></a>
### s_hasTimeout `constants`

##### Summary

Whether [s_defaultTimeout](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.RegularExpressions.Generated.Utilities.s_defaultTimeout 'System.Text.RegularExpressions.Generated.Utilities.s_defaultTimeout') is non-infinite.

<a name='M-System-Text-RegularExpressions-Generated-Utilities-IndexOfNonAsciiOrAny_8894CC54583B80A14EAD0C952FCDE8DDC1F405D966650724E260E131D3AFE36B-System-ReadOnlySpan{System-Char}-'></a>
### IndexOfNonAsciiOrAny_8894CC54583B80A14EAD0C952FCDE8DDC1F405D966650724E260E131D3AFE36B() `method`

##### Summary

Finds the next index of any character that matches a character in the set [--/:\w].

##### Parameters

This method has no parameters.
