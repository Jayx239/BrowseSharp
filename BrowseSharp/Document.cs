using System.Collections.Generic;
using System.Text;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using RestSharp;

namespace BrowseSharp
{
    public class Document
    {
        public Document()
        {
            Styles = new List<Style.Style>();
        }
        
        public Document(IRestRequest request, IRestResponse response)
        {
            Request = request;
            Response = response;
            Styles = new List<Style.Style>();
        }
        
        public Document(IRestRequest request, IRestResponse response, IHtmlDocument htmlDocument)
        {
            Request = request;
            Response = response;
            HtmlDocument = htmlDocument;
            Styles = new List<Style.Style>();
        }
        
        public IRestResponse Response { get; set; }
        public IRestRequest Request { get; set; }

        /*public IHtmlCollection<IHtmlScriptElement> Scripts
        {
            get { return HtmlDocument != null ? HtmlDocument.Scripts : Scripts; }
        }*/

        public IHtmlDocument HtmlDocument { get ; set; }
        public List<Javascript.Javascript> Scripts { get; set; }
        public List<Style.Style> Styles { get; set; }
        /*public Javascript.Javascript GetUnifiedScript()
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            foreach (Javascript.Javascript script in Scripts)
            {
                stringBuilder.AppendLine(script.JavascriptString);
            }

            Javascript.Javascript unifiedScript = new Javascript.Javascript(stringBuilder.ToString());
            return unifiedScript;
        }*/

    }
}