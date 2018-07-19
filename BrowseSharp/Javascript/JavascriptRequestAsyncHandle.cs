using System.Net.Http;
using System.Threading.Tasks;

namespace BrowseSharp.Javascript
{
    public class JavascriptRequestAsyncHandle
    {
        public JavascriptRequestAsyncHandle(Task<HttpResponseMessage> responseAsyncTask, Javascript script)
        {
            ResponseAsyncTask = responseAsyncTask;
            Script = script;
        }

        public Task<HttpResponseMessage> ResponseAsyncTask { get; set; }
        public Javascript Script { get; set; }
    }
}