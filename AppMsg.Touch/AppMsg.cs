using System;
using MonoTouch.UIKit;

namespace AppMsg.Touch
{
    public class AppMsg
    {
        public const int DefaultDuration = 3000;
        public const float MsgViewHeight = 48f;
        public static DisplayPosition DefaultPosition = DisplayPosition.Bottom;

        public UIViewController Controller { private set; get; }
        public String Msg { private set; get; }
        public Style Style { private set; get; }
        public int Duration { private set; get; }
        public DisplayPosition Position { set; get; }
        public UIView View { set; get; }
        public static readonly Style StleAlert = new Style(DefaultDuration, UIColor.FromRGB(204, 0, 0));
        public static readonly Style StyleConfirm = new Style(DefaultDuration, UIColor.FromRGB(255, 136, 0));
        public static readonly Style StyleInfo = new Style(DefaultDuration, UIColor.FromRGB(102, 153, 0));
        public static readonly Style StyleSuccess = new Style(DefaultDuration, UIColor.FromRGB(107, 153, 0));
        public static readonly Style StyleError = new Style(DefaultDuration, UIColor.FromRGB(210, 10, 1));
        public static readonly Style StyleFail = new Style(DefaultDuration, UIColor.FromRGB(245, 93, 92));
        public MsgState State = MsgState.Added;
        public static void SetDefaultPosition(DisplayPosition defaultPosition)
        {
            DefaultPosition = defaultPosition;
        }
        private AppMsg(UIViewController controller, string msg, Style style, int duration = DefaultDuration,
            DisplayPosition position = DisplayPosition.Bottom)
        {
            Controller = controller;
            Msg = msg;
            Style = style;
            Duration = duration;
            Position = position;
        }


        public static AppMsg MakeText(UIViewController controller, string msg, Style style)
        {
            var appMsg = new AppMsg(controller, msg, style);
            return appMsg;
        }

        public AppMsg SetDisplayPosition(DisplayPosition position)
        {
            Position = position;
            return this;
        }

        public AppMsg SetStyle(Style style)
        {
            Style = style;
            return this;
        }

        public AppMsg Show()
        {
            MsgManager manager = MsgManager.Instance;
            manager.Add(this);
            return this;
        }
    }
}