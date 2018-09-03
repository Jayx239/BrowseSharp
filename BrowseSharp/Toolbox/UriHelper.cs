using System;

namespace BrowseSharp.Toolbox
{
    /// <summary>
    /// Helper class for Uri's
    /// </summary>
    public class UriHelper
    {
        /// <summary>
        /// Method for constructing uris when parsing document attributes
        /// </summary>
        /// <param name="responseUri"></param>
        /// <param name="scriptSource"></param>
        /// <returns></returns>
        public static Uri GetUri(Uri responseUri, string scriptSource) { 
            
            Uri scriptUri;
            if(scriptSource.StartsWith("http"))
                scriptUri = new Uri(scriptSource);
            else if(scriptSource.StartsWith("www"))
                scriptUri = new Uri("http://" + scriptSource);
            else if (scriptSource.StartsWith("/"))
                scriptUri = new Uri(ConcatPath(responseUri.Scheme + "://" + responseUri.Host, scriptSource));
            else {
                string scriptRelativePath = responseUri.Scheme + "://" + responseUri.Host;
                for (int i = 0; i < responseUri.Segments.Length-1; i++)
                {
                    scriptRelativePath += responseUri.Segments[i];
                }
                scriptUri = new Uri(ConcatPath(scriptRelativePath,scriptSource));
            }
            return scriptUri;
        }
        
        /// <summary>
        /// Method for concatinating url components
        /// </summary>
        /// <param name="baseString"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string ConcatPath(string baseString, string path)
        {
            string baseTrimmed = baseString;
            if (baseTrimmed.EndsWith("/") && path.StartsWith("/"))
                baseTrimmed = baseString.Substring(0, baseTrimmed.Length - 1);
            else if (!baseTrimmed.EndsWith("/") && !path.StartsWith("/"))
                baseTrimmed += "/";
            return baseTrimmed + path;
        }
    }
}