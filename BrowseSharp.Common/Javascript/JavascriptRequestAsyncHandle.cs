using System.Net.Http;
using System.Threading.Tasks;

namespace BrowseSharp.Common.Javascript
{
    /// <summary>
    /// Handler for scraping javascripts asynchronously
    /// </summary>
    public class JavascriptRequestAsyncHandle
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="responseAsyncTask"></param>
        /// <param name="script"></param>
        public JavascriptRequestAsyncHandle(Task<HttpResponseMessage> responseAsyncTask, Javascript script)
        {
            ResponseAsyncTask = responseAsyncTask;
            Script = script;
        }

        /// <summary>
        /// Task attribute containing asynchronous call task
        /// </summary>
        public Task<HttpResponseMessage> ResponseAsyncTask { get; set; }
        
        /// <summary>
        /// Javascript scraped from asynchronous http call
        /// </summary>
        public Javascript Script { get; set; }
    }
}