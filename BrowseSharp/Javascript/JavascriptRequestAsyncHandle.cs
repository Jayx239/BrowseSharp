using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using RestSharp;

namespace BrowseSharp.Javascript
{
    public class JavascriptRequestAsyncHandle
    {
        public JavascriptRequestAsyncHandle(Task<IRestResponse> requestAsyncHandle, Javascript script)
        {
            RequestAsyncHandle = requestAsyncHandle;
            Script = script;
        }

        public Task<IRestResponse> RequestAsyncHandle { get; set; }
        public Javascript Script { get; set; }
    }
}