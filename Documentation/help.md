<a name='assembly'></a>
# HtmlToPdfMaker

## Contents

- [Converter](#T-HtmlToPdfMaker-Converter 'HtmlToPdfMaker.Converter')
  - [#ctor(headerRequired,footerRequired,tempRootFolder)](#M-HtmlToPdfMaker-Converter-#ctor-HtmlToPdfMaker-Content,HtmlToPdfMaker-Content,HtmlToPdfMaker-Content,DinkToPdf-Orientation,DinkToPdf-PaperKind- 'HtmlToPdfMaker.Converter.#ctor(HtmlToPdfMaker.Content,HtmlToPdfMaker.Content,HtmlToPdfMaker.Content,DinkToPdf.Orientation,DinkToPdf.PaperKind)')
  - [tempFolder](#F-HtmlToPdfMaker-Converter-tempFolder 'HtmlToPdfMaker.Converter.tempFolder')
  - [ReleaseResources()](#M-HtmlToPdfMaker-Converter-ReleaseResources 'HtmlToPdfMaker.Converter.ReleaseResources')
  - [ToPdfAsync(token)](#M-HtmlToPdfMaker-Converter-ToPdfAsync-System-Threading-CancellationToken- 'HtmlToPdfMaker.Converter.ToPdfAsync(System.Threading.CancellationToken)')

<a name='T-HtmlToPdfMaker-Converter'></a>
## Converter `type`

##### Namespace

HtmlToPdfMaker

##### Summary

Class to convert html to Pdf

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| headerRequired | [T:HtmlToPdfMaker.Converter](#T-T-HtmlToPdfMaker-Converter 'T:HtmlToPdfMaker.Converter') |  |

<a name='M-HtmlToPdfMaker-Converter-#ctor-HtmlToPdfMaker-Content,HtmlToPdfMaker-Content,HtmlToPdfMaker-Content,DinkToPdf-Orientation,DinkToPdf-PaperKind-'></a>
### #ctor(headerRequired,footerRequired,tempRootFolder) `constructor`

##### Summary

Class to convert html to Pdf

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| headerRequired | [HtmlToPdfMaker.Content](#T-HtmlToPdfMaker-Content 'HtmlToPdfMaker.Content') |  |
| footerRequired | [HtmlToPdfMaker.Content](#T-HtmlToPdfMaker-Content 'HtmlToPdfMaker.Content') |  |
| tempRootFolder | [HtmlToPdfMaker.Content](#T-HtmlToPdfMaker-Content 'HtmlToPdfMaker.Content') |  |

<a name='F-HtmlToPdfMaker-Converter-tempFolder'></a>
### tempFolder `constants`

##### Summary

The tempFolder

<a name='M-HtmlToPdfMaker-Converter-ReleaseResources'></a>
### ReleaseResources() `method`

##### Summary

Releases the resources.

##### Parameters

This method has no parameters.

<a name='M-HtmlToPdfMaker-Converter-ToPdfAsync-System-Threading-CancellationToken-'></a>
### ToPdfAsync(token) `method`

##### Summary

Converts to pdf.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The token. |
