using AngleSharp.Dom.Html;
using BrowseSharp.BOM.Navigator;
using BrowseSharp.Types;
using Jint;
using System;
using System.Runtime.Serialization;

namespace BrowseSharp.BOM.Window
{
    public class Window : IWindow, IExtensibleDataObject
    {
        Engine _jintEngine;
        #region Constructor

        public Window(IDocument document)
        {
            _document = document;
        }
        public Window()
        {
            DevicePixelRatio = 23;
        }

        public Window(Engine jintEngine)
        {
            _jintEngine = jintEngine;
        }

        public Window(Engine jintEngine, IDocument document)
        {
            _jintEngine = jintEngine;
            _document = document;
        }

        #endregion
        #region Public
        #region Attributes
        public bool Closed { get; set; }
        public int DevicePixelRatio { get; set; }
        public IHtmlDocument document { get; set; }
        public bool FullScreen { get; set; }
        public int InnerHeight { get; set; }
        public int InnerWidth { get; set; }
        public bool IsSecureContext { get; set; }
        public int Length { get; set; }
        public string Location { get; set; }
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
        #endregion
        #region Methods
        public void Alert()
        {
            throw new System.NotImplementedException();
        }

        public void Blur()
        {
            throw new System.NotImplementedException();
        }

        public void CancelAnimationFrame()
        {
            throw new System.NotImplementedException();
        }

        public void CancelIdleCallback()
        {
            throw new System.NotImplementedException();
        }

        public void CaptureEvents()
        {
            throw new System.NotImplementedException();
        }

        public void ClearImmediate()
        {
            throw new System.NotImplementedException();
        }

        public void Close()
        {
            throw new System.NotImplementedException();
        }

        public void Confirm()
        {
            throw new System.NotImplementedException();
        }

        public void DispatchEvent()
        {
            throw new System.NotImplementedException();
        }

        public void Dump()
        {
            throw new System.NotImplementedException();
        }

        public void Find()
        {
            throw new System.NotImplementedException();
        }

        public void Focus()
        {
            throw new System.NotImplementedException();
        }

        public void Forward()
        {
            throw new System.NotImplementedException();
        }

        public void GetAttention()
        {
            throw new System.NotImplementedException();
        }

        public void Home()
        {
            throw new System.NotImplementedException();
        }

        public void Maximize()
        {
            throw new System.NotImplementedException();
        }

        public void Minimize()
        {
            throw new System.NotImplementedException();
        }

        public void MoveBy()
        {
            throw new System.NotImplementedException();
        }

        public void MoveTo()
        {
            throw new System.NotImplementedException();
        }

        public void Open()
        {
            throw new System.NotImplementedException();
        }

        public void OpenDialog()
        {
            throw new System.NotImplementedException();
        }

        public void PostMessage()
        {
            throw new System.NotImplementedException();
        }

        public void Print()
        {
            throw new System.NotImplementedException();
        }

        public void Prompt()
        {
            throw new System.NotImplementedException();
        }

        public void ReleaseEvents()
        {
            throw new System.NotImplementedException();
        }

        public void RequestAnimationFrame()
        {
            throw new System.NotImplementedException();
        }

        public void RequestIdleCallback()
        {
            throw new System.NotImplementedException();
        }

        public void ResizeBy()
        {
            throw new System.NotImplementedException();
        }

        public void ResizeTo()
        {
            throw new System.NotImplementedException();
        }

        public void Restore()
        {
            throw new System.NotImplementedException();
        }

        public void RouteEvent()
        {
            throw new System.NotImplementedException();
        }

        public void Scroll()
        {
            throw new System.NotImplementedException();
        }

        public void ScrollBy()
        {
            throw new System.NotImplementedException();
        }

        public void ScrollByLines()
        {
            throw new System.NotImplementedException();
        }

        public void ScrollByPages()
        {
            throw new System.NotImplementedException();
        }

        public void ScrollTo()
        {
            throw new System.NotImplementedException();
        }

        public void SetCursor()
        {
            throw new System.NotImplementedException();
        }

        public void SetImmediate()
        {
            throw new System.NotImplementedException();
        }

        public void SetResizable()
        {
            throw new System.NotImplementedException();
        }

        public void SizeToContent()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommands()
        {
            throw new System.NotImplementedException();
        }

        public void AddEventListener()
        {
            throw new System.NotImplementedException();
        }

        public void InitializeEngine()
        {
            _jintEngine.SetValue("window", this);
        }
        #endregion
        #region Event Listeners
        public Action<object> onappinstalled { get; set; }
        #endregion
        #endregion
        #region Private

        private IDocument _document;
        private ExtensionDataObject extensionDataObject_value;
        public ExtensionDataObject ExtensionData
        {
            get
            {
                return extensionDataObject_value;
            }
            set
            {
                extensionDataObject_value = value;
            }
        }
        #endregion
    }
}