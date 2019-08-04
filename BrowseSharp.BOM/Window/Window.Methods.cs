using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BrowseSharp.BOM.Window
{
    public partial class Window
    {

        #region Methods
        public dynamic Alert { get; set; }

        public dynamic Blur { get; set; }
        
        public dynamic CancelAnimationFrame { get; set; }
        
        public dynamic CancelIdleCallback { get; set; }
        
        public dynamic CaptureEvents { get; set; }
        
        public dynamic ClearImmediate { get; set; }

        public dynamic Close { get; set; }
        
        public dynamic Confirm { get; set; }
        
        public dynamic DispatchEvent { get; set; }
    
        public dynamic Dump { get; set; }
        

        public dynamic Find { get; set; }

        public dynamic Focus { get; set; }
    
        public dynamic Forward { get; set; }
        
        public dynamic GetAttention { get; set; }
        
        public dynamic Home { get; set; }
       
        public dynamic Maximize { get; set; }
       
        public dynamic Minimize { get; set; }
        
        public dynamic MoveBy { get; set; }
        
        public dynamic MoveTo { get; set; }
        
        public dynamic Open { get; set; }
        
        public dynamic OpenDialog { get; set; }
        
        public dynamic PostMessage { get; set; }
        
        public dynamic Print { get; set; }
        
        public dynamic Prompt { get; set; }
        
        public dynamic ReleaseEvents { get; set; }
        
        public dynamic RequestAnimationFrame { get; set; }
        
        public dynamic RequestIdleCallback { get; set; }
        
        public dynamic ResizeBy { get; set; }
        
        public dynamic ResizeTo { get; set; }
        
        public dynamic Restore { get; set; }
        
        public dynamic RouteEvent { get; set; }
        
        public dynamic Scroll { get; set; }
        
        public dynamic ScrollBy { get; set; }
        
        public dynamic ScrollByLines { get; set; }
        
        public dynamic ScrollByPages { get; set; }
        
        public dynamic ScrollTo { get; set; }
        
        public dynamic SetCursor { get; set; }
        
        public dynamic SetImmediate { get; set; }
        
        public dynamic SetResizable { get; set; }
        
        public dynamic SizeToContent { get; set; }
        
        public dynamic Stop { get; set; }
        
        public dynamic UpdateCommands { get; set; }
        
        public dynamic AddEventListener { get; set; }
        public async Task<int> setTimeout(dynamic func, int timeout = 500, dynamic arguments = null)
        {
            
            await Task.Delay(timeout);
            if(arguments != null)
                func(arguments);
            return 0;
        }

        #endregion
    }
}
