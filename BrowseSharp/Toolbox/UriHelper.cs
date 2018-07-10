using System;

namespace BrowseSharp.Toolbox
{
    public class UriHelper
    {
        
        public static Uri GetUri(Uri responseUri, string scriptSource) { 
            
            Uri scriptUri;
            if(scriptSource.StartsWith("http"))
                scriptUri = new Uri(scriptSource);
            else if(scriptSource.StartsWith("www"))
                scriptUri = new Uri("http://" + scriptSource);
            else if (scriptSource.StartsWith("/"))
                scriptUri =  new Uri(responseUri.Scheme + "://" +  responseUri.Host + scriptSource);
            else 
                scriptUri =  new Uri(responseUri.AbsoluteUri + scriptSource);
            
            return scriptUri;
        }
    }
}