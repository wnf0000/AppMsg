using System.Collections.Generic;
using System.Linq;
using Android.OS;
using Android.Views;
using Android.Views.Animations;
using Java.Lang;

namespace AppMsg.Droid
{
    internal class MsgManager : Handler
    {
        private const int MessageDisplay = 0xc2007;
        private const int MessageAddView = -1040157475;
        private const int MessageRemove = -1040155167;
        private static MsgManager mInstance;

        private readonly Queue<AppMsg> msgQueue;

        private Animation inAnimation, outAnimation;

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

        public void Add(AppMsg appMsg)
        {
            msgQueue.Enqueue(appMsg);
            if (inAnimation == null)
            {
                inAnimation = AnimationUtils.LoadAnimation(appMsg.Activity,
                    Android.Resource.Animation.FadeIn);
            }
            if (outAnimation == null)
            {
                outAnimation = AnimationUtils.LoadAnimation(appMsg.Activity,
                    Android.Resource.Animation.FadeOut);
            }
            DisplayMsg();
        }

        public void ClearMsg(AppMsg appMsg)
        {
            if (msgQueue.Contains(appMsg))
            {
                RemoveMessages(MessageRemove);
                msgQueue.ToList().Remove(appMsg);
                RemoveMsg(appMsg);
            }
        }

        public void ClearAllMsg()
        {
            if (msgQueue != null)
            {
                msgQueue.Clear();
            }
            RemoveMessages(MessageDisplay);
            RemoveMessages(MessageAddView);
            RemoveMessages(MessageRemove);
        }

        private void DisplayMsg()
        {
            if (msgQueue.Count == 0)
            {
                return;
            }

            AppMsg appMsg = msgQueue.Peek();
            if (appMsg.Activity == null)
            {
                msgQueue.Dequeue();
            }

            Message msg;
            if (!appMsg.IsShowing)
            {
                msg = ObtainMessage(MessageAddView);
                msg.Obj = appMsg;
                SendMessage(msg);
            }
            else
            {
                msg = ObtainMessage(MessageDisplay);
                SendMessageDelayed(msg, appMsg.Duration
                                        + inAnimation.Duration + outAnimation.Duration);
            }
        }

        private void RemoveMsg(AppMsg appMsg)
        {
            var parent = ((ViewGroup) appMsg.View.Parent);
            if (parent != null)
            {
                outAnimation.SetAnimationListener(new OutAnimationListener(appMsg));
                appMsg.View.StartAnimation(outAnimation);

                msgQueue.Dequeue();
                if (appMsg.IsFloating)
                {
                    parent.RemoveView(appMsg.View);
                }
                else
                {
                    appMsg.View.Visibility = ViewStates.Invisible;
                }

                Message msg = ObtainMessage(MessageDisplay);
                SendMessage(msg);
            }
        }

        private void AddMsgToView(AppMsg appMsg)
        {
            View view = appMsg.View;

            if (view.Parent == null)
            {
                appMsg.Activity.AddContentView(
                    view,
                    appMsg.LayoutParams);
            }
            view.StartAnimation(inAnimation);

            if (view.Visibility != ViewStates.Visible)
            {
                view.Visibility = ViewStates.Visible;
            }
            var msg = ObtainMessage(MessageRemove);
            msg.Obj = appMsg;
            SendMessageDelayed(msg, appMsg.Duration);
        }


        public override void HandleMessage(Message msg)
        {
            AppMsg appMsg;
            switch (msg.What)
            {
                case MessageDisplay:
                    DisplayMsg();
                    break;
                case MessageAddView:
                    appMsg = (AppMsg) msg.Obj;
                    AddMsgToView(appMsg);
                    break;
                case MessageRemove:
                    appMsg = (AppMsg) msg.Obj;
                    RemoveMsg(appMsg);
                    break;
                default:
                    base.HandleMessage(msg);
                    break;
            }
        }

        private class OutAnimationListener : Object, Animation.IAnimationListener
        {
            private readonly AppMsg appMsg;

            public OutAnimationListener(AppMsg appMsg)
            {
                this.appMsg = appMsg;
            }


            public void OnAnimationStart(Animation animation)
            {
            }


            public void OnAnimationEnd(Animation animation)
            {
                if (!appMsg.IsFloating)
                {
                    appMsg.View.Visibility = ViewStates.Gone;
                }
            }


            public void OnAnimationRepeat(Animation animation)
            {
            }

            //public IntPtr Handle { private set; get; }

            //public void Dispose()
            //{
            //}
        }
    }
}