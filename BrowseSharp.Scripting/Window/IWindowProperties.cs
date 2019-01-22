using AngleSharp.Dom.Html;
using BrowseSharp.Scripting.Navigator;

namespace BrowseSharp.Scripting.Window
{
    public interface IWindowProperties
    {
        #region Properties
        bool closed { get; }
// TODO: Console console { get; }
//Window.content
// TODO: XULController controllers { get; }
// TODO: CustomElementRegistry customElements { get; }

        // TODO: Crypto crypto { get; }

//Window.defaultStatus
int devicePixelRatio { get; }
// TODO: nsIArray dialogArguments { get; }
// TODO: PersonalBar Window.directories { get; }
        IHtmlDocument document { get;}
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
bool fullScreen { get; }
//Window.globalStorage
// TODO: History Window.history { get; }
int innerHeight { get; }
int innerWidth { get; }
bool isSecureContext { get; }
int length { get; }
string location { get; }
// TODO: LocationBar locationbar { get; }
// TODO: LocalStorage localStorage { get; }
// TODO: MenuBar menubar { get;  }
// TODO: MessageManager messageManager { get; set; } // May be deprecated
int mozAnimationStartTime { get; }
int mozInnerScreenX { get; }
int mozInnerScreenY { get; }
int mozPaintCount { get; }
string name { get; set; }
INavigator navigator { get; }
// TODO: Opener opener { get; set; } 
int orientation { get; }
int outerHeight { get; }
int outerWidth { get; }
int pageXOffset { get; }
int pageYOffset { get; }
// TODO: Parent parent { get; }
// TODO: Performance performance { get; }
// TODO: PersonalBar personalbar { get; }
//Window.pkcs11
//Window.returnValue
// TODO: Screen screen { get; }
int screenX { get; }
int screenLeft { get; }
int screenY { get; }
int screenTop { get; }
// TODO: ScrollBars scrollbars { get; }
int scrollMaxX { get; }
int scrollMaxY { get; }
int scrollX { get; }
int scrollY { get; }
Window self { get; }
// TODO: SessionStorage sessionStorage { get; set; }
Window sidebar { get; }
// TODO: SpeechSynthesis speechSynthesis { get; }
string status { get; set; }
// TODO: StatusBar statusbar { get; }
// TODO: ToolBar toolbar { get; }
Window top { get; }
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