using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace AppMsg.Touch
{
    internal class MsgManager:NSObject
    {
        private static MsgManager mInstance;
        private readonly ManualResetEvent allDone = new ManualResetEvent(false);


        private readonly Queue<AppMsg> msgQueue;

        private bool IsRunning;
        private bool needGoon;

        private MsgManager()
        {
            msgQueue = new Queue<AppMsg>();
        }

        public static MsgManager Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new MsgManager();
                }
                return mInstance;
            }
        }

        private void Start()
        {
			IsRunning = true;
            Task.Run(() =>
            {
                while (needGoon)
                {
					allDone.WaitOne();
                    DisplayMsg();
					allDone.Reset();
                }
                IsRunning = false;
            });
        }

        public void Add(AppMsg appMsg)
        {
            msgQueue.Enqueue(appMsg);
            needGoon = true;
			if (!IsRunning) {
				Start ();
				allDone.Set ();
			}
        }

        public void ClearMsg(AppMsg appMsg)
        {
            if (msgQueue.Contains(appMsg))
            {
                appMsg.State = MsgState.Removed;
            }
        }


        public void ClearAllMsg()
        {
            if (msgQueue != null)
            {
                msgQueue.Clear();
            }
        }

        private void DisplayMsg()
        {
            if (msgQueue.Count == 0)
            {
                needGoon = false;
                allDone.Set();
                return;
            }

            AppMsg appMsg = msgQueue.Dequeue();

            AddMsgToView(appMsg);
        }

        private void AddMsgToView(AppMsg appMsg)
        {
			UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                var layout = new UIView
                {
                    BackgroundColor = appMsg.Style.Background
                };
                if (appMsg.Position == DisplayPosition.Top)
                {
                    layout.Frame = new RectangleF(0, UIApplication.SharedApplication.StatusBarFrame.Height, UIScreen.MainScreen.Bounds.Width, AppMsg.MsgViewHeight);
                }
                else if (appMsg.Position == DisplayPosition.Center)
                {
                    layout.Frame = new RectangleF(0, (UIScreen.MainScreen.Bounds.Bottom - AppMsg.MsgViewHeight) / 2,
                        UIScreen.MainScreen.Bounds.Width, AppMsg.MsgViewHeight);
                }
                else
                {
                    layout.Frame = new RectangleF(0, UIScreen.MainScreen.Bounds.Bottom - AppMsg.MsgViewHeight,
                            UIScreen.MainScreen.Bounds.Width, AppMsg.MsgViewHeight);
                    
                }

                var inner = new UILabel
                {
                    Frame = new RectangleF(10, 5, UIScreen.MainScreen.Bounds.Width - 20, AppMsg.MsgViewHeight - 10),
                    Text = appMsg.Msg,
                    Font = UIFont.BoldSystemFontOfSize(14f),
                    TextColor = UIColor.White,
                    //BackgroundColor = appMsg.Style.Background,
                    TextAlignment = UITextAlignment.Center
                };
                layout.AddSubview(inner);
                appMsg.View = layout;
                
                UIApplication.SharedApplication.KeyWindow.AddSubview(layout);

                appMsg.State = MsgState.IsShowing;
					Task.Delay(appMsg.Duration-500).ContinueWith(r => UIApplication.SharedApplication.InvokeOnMainThread(() =>
                {
							UIView.AnimateAsync(.5,()=>{
								inner.Hidden=true;
								layout.BackgroundColor=layout.BackgroundColor.ColorWithAlpha(0);
							}).ContinueWith(t=>{
								
								InvokeOnMainThread (()=>{layout.Hidden=true;layout.RemoveFromSuperview();});
							});
					
                    //layout.Hidden = true;
                    
                    appMsg.State = MsgState.Display;
                    if (msgQueue.Count == 0)
                    {
                        needGoon = false;
                    }
                    allDone.Set();
                }));
            });
        }
    }
}