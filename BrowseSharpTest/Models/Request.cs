using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseSharpTest.Models
{
    /// <summary>
    /// Request object that is deserialized by RequestTester.com and returned in response.
    /// </summary>
    class Request
    {
        public Request()
        {

        }

        public Request(Dictionary<string,string> headers, Dictionary<string,string> cookies, Dictionary<string,string> formData, Dictionary<string,string> query)
        {
            Headers = headers;
            Cookies = cookies;
            FormData = formData;
            Query = query;
        }

        public Dictionary<string, string> Headers { get; set; } 
        public Dictionary<string,string> Cookies { get; set; }
        public Dictionary<string, string> FormData { get; set; }
        public Dictionary<string,string> Query { get; set; }
    }
}
