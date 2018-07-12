using System.Collections.Generic;
using AngleSharp.Dom.Html;
using RestSharp;

namespace BrowseSharp
{
    public interface IDocument
    {
        IRestResponse Response { get; set; }
        IRestRequest Request { get; set; }
        IHtmlDocument HtmlDocument { get ; set; }
        List<Javascript.Javascript> Scripts { get; set; }
        List<Style.StyleSheet> Styles { get; set; }

    }
}