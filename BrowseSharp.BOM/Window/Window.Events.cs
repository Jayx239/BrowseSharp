using System;
using System.Collections.Generic;
using System.Text;

namespace BrowseSharp.BOM.Window
{
    public partial class Window
    {
        #region Event Listeners
        public delegate void VariableParams(dynamic args);
        public void LogArray(string[] values)
        {
            foreach(var val in values)
            {
                Console.Write(val);
            }
        }
        public VariableParams onappinstalled { get; set; }
        private string _eventsJavaScript = @"
        var onbeforeinstallprompt;
var ondevicelight;
var ondevicemotion;
var ondeviceorientation;
var ondeviceorientationabsolute;
var ondeviceproximity;
var ongamepadconnected;
var ongamepaddisconnected;
var onmozbeforepaint;
var onpaint;
var onrejectionhandled;
var onuserproximity;
var onvrdisplayconnect;
var onvrdisplaydisconnect;
var onvrdisplayactivate;
var onvrdisplaydeactivate;
var onvrdisplayblur;
var onvrdisplayfocus;
var onvrdisplaypresentchange;";
        #endregion
    }
}
