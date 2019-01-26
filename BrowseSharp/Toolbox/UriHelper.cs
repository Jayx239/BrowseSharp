using System;

namespace BrowseSharp.Toolbox
{
    /// <summary>
    /// Helper class for Uri's
    /// </summary>
    public class UriHelper
    {
        /// <summary>
        /// Default protocol that will be applied to uri if passed in uri string does not contain a protcol
        /// </summary>
        public static string DefaultUriProtocol
        {
            get => _defaultUriProtocol;
            set
            {
                if(value == "http" || value == "https")
                    _defaultUriProtocol = value;
            }
        }

        /// <summary>
        /// Default protocol that will be applied to uri if passed in uri string does not contain a protcol
        /// </summary>
        private static string _defaultUriProtocol = "http";
        
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
                for (int i = 0; i < responseUri.Segments.Length; i++)
                {
                    if (!responseUri.Segments[i].EndsWith("/"))
                        break;
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

        /// <summary>
        /// Creates a uri pre-pending the default protocol if the uriString does not contain one
        /// </summary>
        /// <param name="uriString"></param>
        /// <returns>Uri</returns>
        public static Uri Uri(string uriString)
        {
            return Uri(uriString, DefaultUriProtocol);
        }

        /// <summary>
        /// Creates a uri pre-pending the default protocol if the uriString does not contain one
        /// </summary>
        /// <param name="uriString"></param>
        /// <param name="defaultUriProtocol">Default protocol to be used if uriString does not contain a protocol</param>
        /// <returns>Uri</returns>
        public static Uri Uri(string uriString, string defaultUriProtocol)
        {
            if (uriString.Contains("http"))
                return new Uri(uriString);
            else
                return new Uri(defaultUriProtocol + "://" + uriString);
        }
    }
}