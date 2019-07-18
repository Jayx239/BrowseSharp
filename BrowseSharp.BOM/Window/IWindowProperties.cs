
using BrowseSharp.BOM.Navigator;

namespace BrowseSharp.BOM.Window
{
    public interface IWindowProperties
    {
        #region Properties
        bool Closed { get; }
// TODO: Console console { get; }
//Window.content
// TODO: XULController controllers { get; }
// TODO: CustomElementRegistry customElements { get; }

        // TODO: Crypto crypto { get; }

//Window.defaultStatus
int DevicePixelRatio { get; }
// TODO: nsIArray dialogArguments { get; }
// TODO: PersonalBar Window.directories { get; }
        //IHtmlDocument document { get;}
// TODO: DOMMatrix DOMMatrix { get; }
// TODO: DOMMatrixReadOnly DOMMatrixReadOnly { get; }
// TODO: DOMPoint DOMPoint { get; }
// TODO: DOMPointReadOnly DOMPointReadOnly { get; }
// TODO: DOMQuad DOMQuad { get; }
// TODO: DOMRect DOMRect { get; }
// TODO: DOMRectReadOnly DOMRectReadOnly { get; }
// TODO: Event event { get; }
// TODO: Element frameElement { get; }
// TODO: List<Element> frames { get; }
bool FullScreen { get; }
//Window.globalStorage
// TODO: History Window.history { get; }
int InnerHeight { get; }
int InnerWidth { get; }
bool IsSecureContext { get; }
int Length { get; }
string Location { get; }
// TODO: LocationBar locationbar { get; }
// TODO: LocalStorage localStorage { get; }
// TODO: MenuBar menubar { get;  }
// TODO: MessageManager messageManager { get; set; } // May be deprecated
int MozAnimationStartTime { get; }
int MozInnerScreenX { get; }
int MozInnerScreenY { get; }
int MozPaintCount { get; }
string Name { get; set; }
INavigator Navigator { get; }
// TODO: Opener opener { get; set; } 
int Orientation { get; }
int OuterHeight { get; }
int OuterWidth { get; }
int PageXOffset { get; }
int PageYOffset { get; }
// TODO: Parent parent { get; }
// TODO: Performance performance { get; }
// TODO: PersonalBar personalbar { get; }
//Window.pkcs11
//Window.returnValue
// TODO: Screen screen { get; }
int ScreenX { get; }
int ScreenLeft { get; }
int ScreenY { get; }
int ScreenTop { get; }
// TODO: ScrollBars scrollbars { get; }
int ScrollMaxX { get; }
int ScrollMaxY { get; }
int ScrollX { get; }
int ScrollY { get; }
Window Self { get; }
// TODO: SessionStorage sessionStorage { get; set; }
Window Sidebar { get; }
// TODO: SpeechSynthesis speechSynthesis { get; }
string Status { get; set; }
// TODO: StatusBar statusbar { get; }
// TODO: ToolBar toolbar { get; }
Window Top { get; }
// TODO: VisualViewport visualViewport { get; }
Window window { get; }
//List<Window> window[0]
 //window[1]
        // TODO: WindowOrWorkerGlobalScope WindowOrWorkerGlobalScop { get; }
/*WindowOrWorkerGlobalScope.caches
WindowOrWorkerGlobalScope.indexedDB
WindowOrWorkerGlobalScope.isSecureContext
WindowOrWorkerGlobalScope.origin*/
        #endregion
    }
}