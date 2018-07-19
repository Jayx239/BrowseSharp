using System.Net.Http;
using System.Threading.Tasks;

namespace BrowseSharp.Style
{
    public class StyleSheetRequestAsyncHandle
    {
        public StyleSheetRequestAsyncHandle(Task<HttpResponseMessage> responseAsyncTask, StyleSheet styleSheet)
        {
            ResponseAsyncTask = responseAsyncTask;
            StyleSheet = styleSheet;
        }

        public Task<HttpResponseMessage> ResponseAsyncTask { get; set; }
        public StyleSheet StyleSheet { get; set; }
    }
}