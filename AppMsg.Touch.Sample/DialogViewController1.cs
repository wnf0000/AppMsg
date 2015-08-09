using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace AppMsg.Touch.Sample
{
    public partial class DialogViewController1 : DialogViewController
    {
        public DialogViewController1()
            : base(UITableViewStyle.Grouped, null)
        {
            Root = new RootElement("DialogViewController1") {
				new Section (""){
                    //new StringElement ("Hello", () => {
                    //    new UIAlertView ("Hola", "Thanks for tapping!", null, "Continue").Show (); 
                    //}),
					//new EntryElement ("Name", "Enter your name", String.Empty),
                    new StringElement("顶部显示", () =>
                    AppMsg.MakeText (this, "顶部显示", AppMsg.StyleInfo).SetDisplayPosition (DisplayPosition.Top).Show ()),
                     new StringElement("中间显示", () =>
                    AppMsg.MakeText (this, "中间显示", AppMsg.StyleInfo).SetDisplayPosition (DisplayPosition.Center).Show ()),
                    new StringElement("底部显示", () =>
                    AppMsg.MakeText (this, "底部显示", AppMsg.StyleInfo).SetDisplayPosition (DisplayPosition.Bottom).Show ()),
                    new StringElement("显示成功", () =>
                    AppMsg.MakeText (this, "显示成功", AppMsg.StyleSuccess).Show ()),
                    new StringElement("显示失败", () =>
                    AppMsg.MakeText (this, "显示失败", AppMsg.StyleError).Show ())
				},
			};
            
        }
    }
}