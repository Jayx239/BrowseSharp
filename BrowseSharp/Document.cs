using System.Collections.Generic;
using AngleSharp.Dom.Html;
using BrowseSharp.Style;
using RestSharp;

namespace BrowseSharp
{
    public class Document : IDocument
    {
        public Document()
        {
            Scripts = new List<Javascript.Javascript>();
            Styles = new List<StyleSheet>();
        }
        
        public Document(IRestRequest request, IRestResponse response)
        {
            Request = request;
            Response = response;
            Scripts = new List<Javascript.Javascript>();
            Styles = new List<StyleSheet>();
        }
        
        public Document(IRestRequest request, IRestResponse response, IHtmlDocument htmlDocument)
        {
            Request = request;
            Response = response;
            HtmlDocument = htmlDocument;
            Scripts = new List<Javascript.Javascript>();
            Styles = new List<StyleSheet>();
        }
        
        public IRestResponse Response { get; set; }
        public IRestRequest Request { get; set; }
        public IHtmlDocument HtmlDocument { get ; set; }
        public List<Javascript.Javascript> Scripts { get; set; }
        public List<StyleSheet> Styles { get; set; }

    }
}