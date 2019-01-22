using AngleSharp.Dom.Html;
using BrowseSharp.Scripting.Navigator;

namespace BrowseSharp.Scripting.Window
{
    public class Window : IWindow
    {
        #region Constructor

        public Window(IDocument document)
        {
            _document = document;
        }
        #endregion
        #region Public
        public bool closed { get; }
        public int devicePixelRatio { get; }
        public IHtmlDocument document { get { return _document.HtmlDocument; } }
        public bool fullScreen { get; }
        public int innerHeight { get; }
        public int innerWidth { get; }
        public bool isSecureContext { get; }
        public int length { get; }
        public string location { get; }
        public int mozAnimationStartTime { get; }
        public int mozInnerScreenX { get; }
        public int mozInnerScreenY { get; }
        public int mozPaintCount { get; }
        public string name { get; set; }
        public INavigator navigator { get; }
        public int orientation { get; }
        public int outerHeight { get; }
        public int outerWidth { get; }
        public int pageXOffset { get; }
        public int pageYOffset { get; }
        public int screenX { get; }
        public int screenLeft { get; }
        public int screenY { get; }
        public int screenTop { get; }
        public int scrollMaxX { get; }
        public int scrollMaxY { get; }
        public int scrollX { get; }
        public int scrollY { get; }
        public Window self { get; }
        public Window sidebar { get; }
        public string status { get; set; }
        public Window top { get; }
        public Window window { get; }
        public void alert()
        {
            throw new System.NotImplementedException();
        }

        public void blur()
        {
            throw new System.NotImplementedException();
        }

        public void cancelAnimationFrame()
        {
            throw new System.NotImplementedException();
        }

        public void cancelIdleCallback()
        {
            throw new System.NotImplementedException();
        }

        public void captureEvents()
        {
            throw new System.NotImplementedException();
        }

        public void clearImmediate()
        {
            throw new System.NotImplementedException();
        }

        public void close()
        {
            throw new System.NotImplementedException();
        }

        public void confirm()
        {
            throw new System.NotImplementedException();
        }

        public void dispatchEvent()
        {
            throw new System.NotImplementedException();
        }

        public void dump()
        {
            throw new System.NotImplementedException();
        }

        public void find()
        {
            throw new System.NotImplementedException();
        }

        public void focus()
        {
            throw new System.NotImplementedException();
        }

        public void forward()
        {
            throw new System.NotImplementedException();
        }

        public void getAttention()
        {
            throw new System.NotImplementedException();
        }

        public void home()
        {
            throw new System.NotImplementedException();
        }

        public void maximize()
        {
            throw new System.NotImplementedException();
        }

        public void minimize()
        {
            throw new System.NotImplementedException();
        }

        public void moveBy()
        {
            throw new System.NotImplementedException();
        }

        public void moveTo()
        {
            throw new System.NotImplementedException();
        }

        public void open()
        {
            throw new System.NotImplementedException();
        }

        public void openDialog()
        {
            throw new System.NotImplementedException();
        }

        public void postMessage()
        {
            throw new System.NotImplementedException();
        }

        public void print()
        {
            throw new System.NotImplementedException();
        }

        public void prompt()
        {
            throw new System.NotImplementedException();
        }

        public void releaseEvents()
        {
            throw new System.NotImplementedException();
        }

        public void requestAnimationFrame()
        {
            throw new System.NotImplementedException();
        }

        public void requestIdleCallback()
        {
            throw new System.NotImplementedException();
        }

        public void resizeBy()
        {
            throw new System.NotImplementedException();
        }

        public void resizeTo()
        {
            throw new System.NotImplementedException();
        }

        public void restore()
        {
            throw new System.NotImplementedException();
        }

        public void routeEvent()
        {
            throw new System.NotImplementedException();
        }

        public void scroll()
        {
            throw new System.NotImplementedException();
        }

        public void scrollBy()
        {
            throw new System.NotImplementedException();
        }

        public void scrollByLines()
        {
            throw new System.NotImplementedException();
        }

        public void scrollByPages()
        {
            throw new System.NotImplementedException();
        }

        public void scrollTo()
        {
            throw new System.NotImplementedException();
        }

        public void setCursor()
        {
            throw new System.NotImplementedException();
        }

        public void setImmediate()
        {
            throw new System.NotImplementedException();
        }

        public void setResizable()
        {
            throw new System.NotImplementedException();
        }

        public void sizeToContent()
        {
            throw new System.NotImplementedException();
        }

        public void stop()
        {
            throw new System.NotImplementedException();
        }

        public void updateCommands()
        {
            throw new System.NotImplementedException();
        }

        public void addEventListener()
        {
            throw new System.NotImplementedException();
        }
        #endregion
        #region Private

        private IDocument _document;

        #endregion
    }
}