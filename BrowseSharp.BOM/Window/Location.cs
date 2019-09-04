using System;
using System.Collections.Generic;
using System.Text;

namespace BrowseSharp.BOM.Window
{
    public class Location
    {
        public string href { get; set; }
        public string hostname { get; set; }
        public string pathname { get; set; }
        public string protocol { get; set; }
        public string assign { get; set; }

        public Location()
        {
            href = string.Empty;
            hostname = string.Empty;
            pathname = string.Empty;
            protocol = string.Empty;
            assign = string.Empty;
        }
    }
}
