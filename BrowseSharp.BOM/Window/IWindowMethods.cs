namespace BrowseSharp.BOM.Window
{
    public interface IWindowMethods
    {
        #region Methods

        void Alert();
//void back()
        void Blur();
        void CancelAnimationFrame();
        void CancelIdleCallback();
        void CaptureEvents();
        void ClearImmediate();
        void Close();
        void Confirm();
//Window.disableExternalCapture()
        void DispatchEvent();
            void Dump();
//void enableExternalCapture();
        void Find();
            void Focus();
        void Forward();
        void GetAttention();
        //void getAttentionWithCycleCount();
        // TODO: ComputedStyle getComputedStyle();
        // TODO: ComputedStyle getDefaultComputedStyle();
        // TODO: Selection getSelection();
        void Home();
        // TODO: MediaQueryList matchMedia();
        void Maximize();
        void Minimize();
        void MoveBy();
        void MoveTo();
        void Open(); // may return window
        void OpenDialog(); // may return
        void PostMessage();
        void Print();
        void Prompt();
        void ReleaseEvents();
        void RequestAnimationFrame();
        void RequestIdleCallback();
        void ResizeBy();
        void ResizeTo();
        void Restore(); // may return
        void RouteEvent(); // may return
        void Scroll();
        void ScrollBy();
        void ScrollByLines();
        void ScrollByPages();
        void ScrollTo();
        void SetCursor();
        void SetImmediate();
        void SetResizable();
        void SizeToContent();
        void Stop();
        void UpdateCommands();
        void AddEventListener();
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