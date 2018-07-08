using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace BrowseSharp
{
    public class Document
    {
        public Document()
        {
            Scripts = new List<Javascript.Javascript>();
            Styles = new List<Style.Style>();
        }
        
        public Document(IRestRequest request, IRestResponse response)
        {
            Request = request;
            Response = response;
            Scripts = new List<Javascript.Javascript>();
            Styles = new List<Style.Style>();
        }
        
        public Document(IRestRequest request, IRestResponse response, List<Javascript.Javascript> scripts, List<Style.Style> styles)
        {
            Request = request;
            Response = response;
            Scripts = scripts;
            Styles = styles;
        }
        
        public IRestResponse Response { get; set; }
        public IRestRequest Request { get; set; }
        public List<Javascript.Javascript> Scripts { get; set; }
        public List<Style.Style> Styles { get; set; }

        public Javascript.Javascript GetUnifiedScript()
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            foreach (Javascript.Javascript script in Scripts)
            {
                stringBuilder.AppendLine(script.JavascriptString);
            }

            Javascript.Javascript unifiedScript = new Javascript.Javascript(stringBuilder.ToString());
            return unifiedScript;
        }

    }
}