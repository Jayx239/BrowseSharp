using AngleSharp.Dom.Html;
using BrowseSharp.BOM.Navigator;
using BrowseSharp.Types;
using System;

namespace BrowseSharp.BOM.Window
{
    public class Window : IWindow
    {
        #region Constructor

        public Window(IDocument document)
        {
            _document = document;
        }
        public Window()
        {
            DevicePixelRatio = 23;
        }
        #endregion
        #region Public
        #region Attributes
        public bool Closed { get; }
        public int DevicePixelRatio { get; }
        public IHtmlDocument document { get { return _document.HtmlDocument; } }
        public bool FullScreen { get; }
        public int InnerHeight { get; }
        public int InnerWidth { get; }
        public bool IsSecureContext { get; }
        public int Length { get; }
        public string Location { get; }
        public int MozAnimationStartTime { get; }
        public int MozInnerScreenX { get; }
        public int MozInnerScreenY { get; }
        public int MozPaintCount { get; }
        public string Name { get; set; }
        public INavigator Navigator { get; }
        public int Orientation { get; }
        public int OuterHeight { get; }
        public int OuterWidth { get; }
        public int PageXOffset { get; }
        public int PageYOffset { get; }
        public int ScreenX { get; }
        public int ScreenLeft { get; }
        public int ScreenY { get; }
        public int ScreenTop { get; }
        public int ScrollMaxX { get; }
        public int ScrollMaxY { get; }
        public int ScrollX { get; }
        public int ScrollY { get; }
        public Window Self { get; }
        public Window Sidebar { get; }
        public string Status { get; set; }
        public Window Top { get; }
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
        #endregion
        #region Event Listeners
        public Action<object> onappinstalled { get; set; }
        #endregion
        #endregion
        #region Private

        private IDocument _document;

        #endregion
    }
}