using System;
using Microsoft.Win32;

namespace BrowseSharp.Javascript
{
    public class JavascriptResult
    {
        public JavascriptResult()
        {
            
        }
        
        public JavascriptResult(Object result)
        {
            value = result;
        }

        private Object value { get; set; }
    }
}