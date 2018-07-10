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
                scriptUri = new Uri(concatPath(responseUri.Scheme + "://" + responseUri.Host, scriptSource));
            else 
                scriptUri = new Uri(concatPath(responseUri.AbsoluteUri, scriptSource));
            
            return scriptUri;
        }
        
        private static string concatPath(string baseString, string path)
        {
            string basTrimmed = baseString;
            if (baseString.EndsWith("/") && path.StartsWith("/"))
                baseString = baseString.Substring(0, baseString.Length - 1);
            else if (!baseString.EndsWith("/") && !path.StartsWith("/"))
                baseString += "/";
            return baseString + path;
        }
    }
}