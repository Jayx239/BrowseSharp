namespace BrowseSharp.Scripting.Window
{
    public interface IWindowMethods
    {
        #region Methods

        void alert();
//void back()
        void blur();
        void cancelAnimationFrame();
        void cancelIdleCallback();
        void captureEvents();
        void clearImmediate();
        void close();
        void confirm();
//Window.disableExternalCapture()
        void dispatchEvent();
            void dump();
//void enableExternalCapture();
        void find();
            void focus();
        void forward();
        void getAttention();
        //void getAttentionWithCycleCount();
        // TODO: ComputedStyle getComputedStyle();
        // TODO: ComputedStyle getDefaultComputedStyle();
        // TODO: Selection getSelection();
        void home();
        // TODO: MediaQueryList matchMedia();
        void maximize();
        void minimize();
        void moveBy();
        void moveTo();
        void open(); // may return window
        void openDialog(); // may return
        void postMessage();
        void print();
        void prompt();
        void releaseEvents();
        void requestAnimationFrame();
        void requestIdleCallback();
        void resizeBy();
        void resizeTo();
        void restore(); // may return
        void routeEvent(); // may return
        void scroll();
        void scrollBy();
        void scrollByLines();
        void scrollByPages();
        void scrollTo();
        void setCursor();
        void setImmediate();
        void setResizable();
        void sizeToContent();
        void stop();
        void updateCommands();
        void addEventListener();
        // TODO: WindowOrWorkerGlobalScope WindowOrWorkerGlobalScope { get; }
    
        // TODO: EventTarget EventTarget { get; }
/*WindowOrWorkerGlobalScope.atob()
WindowOrWorkerGlobalScope.btoa()
WindowOrWorkerGlobalScope.clearInterval()
WindowOrWorkerGlobalScope.clearTimeout()
WindowOrWorkerGlobalScope.createImageBitmap()
WindowOrWorkerGlobalScope.fetch()
EventTarget.removeEventListener
WindowOrWorkerGlobalScope.setInterval()
WindowOrWorkerGlobalScope.setTimeout()
Window.showModalDialog() // obsolete*/
            #endregion
    }
}