using System.Collections.Generic;
using AngleSharp.Dom.Html;
using RestSharp;

namespace BrowseSharp
{
    /// <summary>
    /// BrowseSharp Document containing info about an http request and result
    /// </summary>
    public interface IDocument
    {
        /// <summary>
        /// Response returned from http request
        /// </summary>
        IRestResponse Response { get; set; }
        
        /// <summary>
        /// Http request object
        /// </summary>
        IRestRequest Request { get; set; }
        
        /// <summary>
        /// AngleSharp html document scraped from response
        /// </summary>
        IHtmlDocument HtmlDocument { get ; set; }
        
        /// <summary>
        /// Javascripts scraped from http response
        /// </summary>
        List<Javascript.Javascript> Scripts { get; set; }
        
        /// <summary>
        /// Styles parsed from http response 
        /// </summary>
        List<Style.StyleSheet> Styles { get; set; }

    }
}