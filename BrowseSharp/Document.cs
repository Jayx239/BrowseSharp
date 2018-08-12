using System.Collections.Generic;
using AngleSharp.Dom.Html;
using BrowseSharp.Style;
using RestSharp;

namespace BrowseSharp
{
    /// <summary>
    /// Documents that contain data about each http request
    /// </summary>
    public class Document : IDocument
    {
        /// <summary>
        /// Default constructor generating new Document
        /// </summary>
        public Document()
        {
            Scripts = new List<Javascript.Javascript>();
            Styles = new List<StyleSheet>();
        }
        
        /// <summary>
        /// Constructor generating new Document
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public Document(IRestRequest request, IRestResponse response)
        {
            Request = request;
            Response = response;
            Scripts = new List<Javascript.Javascript>();
            Styles = new List<StyleSheet>();
        }
        
        /// <summary>
        /// Constructor generating new Document
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="htmlDocument"></param>
        public Document(IRestRequest request, IRestResponse response, IHtmlDocument htmlDocument)
        {
            Request = request;
            Response = response;
            HtmlDocument = htmlDocument;
            Scripts = new List<Javascript.Javascript>();
            Styles = new List<StyleSheet>();
        }
        
        /// <summary>
        /// Response returned by http request
        /// </summary>
        public IRestResponse Response { get; set; }
        
        /// <summary>
        /// Request made by client
        /// </summary>
        public IRestRequest Request { get; set; }
        
        /// <summary>
        /// Anglesharp document parsed from response
        /// </summary>
        public IHtmlDocument HtmlDocument { get ; set; }
        
        /// <summary>
        /// Javascripts parsed from response
        /// </summary>
        public List<Javascript.Javascript> Scripts { get; set; }
        
        /// <summary>
        /// Stylesheets parsed from response
        /// </summary>
        public List<StyleSheet> Styles { get; set; }

    }
}