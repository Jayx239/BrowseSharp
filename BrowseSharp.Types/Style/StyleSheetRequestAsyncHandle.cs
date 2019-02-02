using System.Net.Http;
using System.Threading.Tasks;

namespace BrowseSharp.Types.Style
{
    /// <summary>
    /// Handler for scraping stylesheets asynchronously
    /// </summary>
    public class StyleSheetRequestAsyncHandle
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="responseAsyncTask"></param>
        /// <param name="styleSheet"></param>
        public StyleSheetRequestAsyncHandle(Task<HttpResponseMessage> responseAsyncTask, StyleSheet styleSheet)
        {
            ResponseAsyncTask = responseAsyncTask;
            StyleSheet = styleSheet;
        }

        /// <summary>
        /// Task attribute containing asynchronous call task
        /// </summary>
        public Task<HttpResponseMessage> ResponseAsyncTask { get; set; }
        
        /// <summary>
        /// StyleSheet scraped from asynchronous http call
        /// </summary>
        public StyleSheet StyleSheet { get; set; }
    }
}