using AngleSharp.Attributes;
using AngleSharp.Html.Dom;
using BrowseSharp.BOM.Navigator;
using BrowseSharp.Common;
using Jint;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace BrowseSharp.BOM.Window
{
    public partial class Window : DynamicObject//, IWindow
    {
        Engine _jintEngine;

        // The inner dictionary.
        Dictionary<string, dynamic> dictionary;
            
        // If you try to get a value of a property 
        // not defined in the class, this method is called.
        public override bool TryGetMember(
            GetMemberBinder binder, out dynamic result)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            string name = binder.Name;

            // If the property name is found in a dictionary,
            // set the result parameter to the property value and return true.
            // Otherwise, return false.
            return dictionary.TryGetValue(name, out result);
        }

        // If you try to set a value of a property that is
        // not defined in the class, this method is called.
        public override bool TrySetMember(
            SetMemberBinder binder, dynamic value)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            dictionary[binder.Name] = value;

            // You can always add a value to a dictionary,
            // so this method always returns true.
            return true;
        }
        #region Constructor

        public Window(IDocument document) : this()
        {
            _document = document;
        }
        public Window()
        {
            dictionary = new Dictionary<string, dynamic>();
            DevicePixelRatio = 23;
        }

        public Window(Engine jintEngine) : this()
        {
            _jintEngine = jintEngine;
        }

        public Window(Engine jintEngine, IDocument document)
        {
            _jintEngine = jintEngine;
            _document = document;
        }

        #endregion
        #region Public Attributes
        public bool Closed { get; set; }
        public int DevicePixelRatio { get; set; }
        public IHtmlDocument document { get; set; }
        public bool FullScreen { get; set; }
        public int InnerHeight { get; set; }
        public int InnerWidth { get; set; }
        public bool IsSecureContext { get; set; }
        public int Length { get; set; }
        //public string Location { get; set; }
        public int MozAnimationStartTime { get; set; }
        public int MozInnerScreenX { get; set; }
        public int MozInnerScreenY { get; set; }
        public int MozPaintCount { get; set; }
        public string Name { get; set; }
        public INavigator Navigator { get; set; }
        public int Orientation { get; set; }
        public int OuterHeight { get; set; }
        public int OuterWidth { get; set; }
        public int PageXOffset { get; set; }
        public int PageYOffset { get; set; }
        public int ScreenX { get; set; }
        public int ScreenLeft { get; set; }
        public int ScreenY { get; set; }
        public int ScreenTop { get; set; }
        public int ScrollMaxX { get; set; }
        public int ScrollMaxY { get; set; }
        public int ScrollX { get; set; }
        public int ScrollY { get; set; }
        public Window Self { get; set; }
        public Window Sidebar { get; set; }
        public string Status { get; set; }
        public Window Top { get; set; }
        public Window window { get { return this; } } // lowercase to since same as class name
        public Location location { get; set; }
        #endregion

        private dynamic _jquery;

        [DomName("jQuery")]
        public dynamic jQuery { get { return _jquery;  } set { _jquery = value; if (dictionary.ContainsKey("$")) { dictionary["$"] = value; } else { dictionary.Add("$", value); } } }

        /*
        [DomName("$")]
        public dynamic dollarsign { get; set; }*/
        public void InitializeEngine()
        {
            Func<object, int> setTimeout = (func) => { return 0; };
            this.setTimeout = setTimeout;
            location = new Location();
            dynamic dynamicWindow = this;
            _jintEngine.SetValue("window", dynamicWindow);
            _jintEngine.Execute("window.jQuery = {};");
            _jintEngine.Execute("window.$ = {};");
            //dictionary.Add("jQuery");
            /*_jintEngine.Execute("var window = {document: {}};");
            _jintEngine.Execute("window.window = window;");
            _jintEngine.SetValue("window.DevicePixelRatio", DevicePixelRatio);
            _jintEngine.SetValue("window.document", document);
            _jintEngine.SetValue("document", document);
            _jintEngine.SetValue("window.FullScreen", FullScreen);
            _jintEngine.SetValue("window.InnerHeight", InnerHeight);
            _jintEngine.SetValue("window.InnerWidth", InnerWidth);
            _jintEngine.SetValue("window.IsSecureContext", IsSecureContext);
            _jintEngine.SetValue("window.Length", Length);
            _jintEngine.SetValue("window.MozAnimationStartTime", MozAnimationStartTime);
            _jintEngine.SetValue("window.MozInnerScreenX", MozInnerScreenX);
            _jintEngine.SetValue("window.MozInnerScreenY", MozInnerScreenY);
            _jintEngine.SetValue("window.MozPaintCount", MozPaintCount);
            _jintEngine.SetValue("window.Name", Name);
            _jintEngine.SetValue("window.Navigator", Navigator);
            _jintEngine.SetValue("window.Orientation", Orientation);
            _jintEngine.SetValue("window.OuterHeight", OuterHeight);
            _jintEngine.SetValue("window.OuterWidth", OuterWidth);
            _jintEngine.SetValue("window.PageXOffset", PageXOffset);
            _jintEngine.SetValue("window.PageYOffset", PageYOffset);
            _jintEngine.SetValue("window.ScreenX", ScreenX);
            _jintEngine.SetValue("window.ScreenLeft", ScreenLeft);
            _jintEngine.SetValue("window.ScreenY", ScreenY);
            _jintEngine.SetValue("window.ScreenTop", ScreenTop);
            _jintEngine.SetValue("window.ScrollMaxX", ScrollMaxX);
            _jintEngine.SetValue("window.ScrollMaxY", ScrollMaxY);
            _jintEngine.SetValue("window.ScrollX", ScrollX);
            _jintEngine.SetValue("window.ScrollY", ScrollY);
            _jintEngine.SetValue("window.Self", Self);
            _jintEngine.SetValue("window.Sidebar", Sidebar);
            _jintEngine.SetValue("window.Status", Status);
            _jintEngine.SetValue("window.Top", Top);
            _jintEngine.SetValue("window.location", location);
            */
        }

        #region Private
        private IDocument _document;
        #endregion
    }
}