using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace AppMsg.Droid
{
    public class AppMsg : Object
    {
        //private static GravityFlags _defaultGravity = GravityFlags.Bottom;
        public static DisplayPosition DefaultPosition = DisplayPosition.Bottom;
        //public DisplayPosition Position { set; get; }
        public static void SetDefaultPosition(DisplayPosition defaultPosition)
        {
            DefaultPosition = defaultPosition;
        }

        public const int LengthShort = 3000;

        public const int LengthLong = 5000;
        private readonly Activity mContext;


        public readonly static Style StleAlert = new Style(LengthLong, Resource.Color.monodroid_appmsg_alert);

        public readonly static Style StyleConfirm = new Style(LengthShort, Resource.Color.monodroid_appmsg_confirm);

        public readonly static Style StyleInfo = new Style(LengthShort, Resource.Color.monodroid_appmsg_info);

        public readonly static Style StyleSuccess = new Style(LengthShort, Resource.Color.monodroid_appmsg_success);

        public readonly static Style StyleError = new Style(LengthShort, Resource.Color.monodroid_appmsg_error);

        public readonly static Style StyleFail = new Style(LengthShort, Resource.Color.monodroid_appmsg_fail);

        private bool Floating;
        private ViewGroup.LayoutParams mLayoutParams;
        private View mView;

        public AppMsg(Activity context)
        {
            mContext = context;
            Duration = LengthShort;
        }

        public bool IsShowing
        {
            get
            {
                if (Floating)
                {
                    return mView != null && mView.Parent != null;
                }
                else
                {
                    return mView.Visibility == ViewStates.Visible;
                }
            }
        }

        public Activity Activity
        {
            get { return mContext; }
        }

        public View View { get; set; }

        public int Duration { set; get; }

        public ViewGroup.LayoutParams LayoutParams
        {
            get
            {
                if (mLayoutParams == null)
                {
                    mLayoutParams = new FrameLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                                                         ViewGroup.LayoutParams.WrapContent, ConvertPosition(DefaultPosition));
                }
                return mLayoutParams;
            }
            set { mLayoutParams = value; }
        }

        public bool IsFloating
        {
            get { return Floating; }
        }

        public static AppMsg MakeText(Activity context, string text, Style style)
        {
            return MakeText(context, text, style, Resource.Layout.monodroid_app_msg);
        }

        public static AppMsg MakeText(Activity context, string text, Style style, int layoutId)
        {
            var inflate = (LayoutInflater)
                          context.GetSystemService(Context.LayoutInflaterService);
            View v = inflate.Inflate(layoutId, null);

            return MakeText(context, text, style, v, true);
        }

        public static AppMsg MakeText(Activity context, string text, Style style, View customView)
        {
            return MakeText(context, text, style, customView, false);
        }

        private static AppMsg MakeText(Activity context, string text, Style style, View view, bool floating)
        {
            var result = new AppMsg(context);

            view.SetBackgroundResource(style.Background);

            var tv = (TextView)view.FindViewById(Android.Resource.Id.Message);
            tv.Text = text;

            result.View = view;
            result.Duration = style.Duration;
            result.SetFloating(floating);

            return result;
        }


        public static AppMsg MakeText(Activity context, int resId, Style style, View customView, bool floating)
        {
            return MakeText(context, context.Resources.GetText(resId), style, customView, floating);
        }



        public static AppMsg MakeText(Activity context, int resId, Style style)
        {
            return MakeText(context, context.Resources.GetText(resId), style);
        }

        public static AppMsg MakeText(Activity context, int resId, Style style, int layoutId)
        {
            return MakeText(context, context.Resources.GetText(resId), style, layoutId);
        }



        public void Show()
        {
            MsgManager manager = MsgManager.Instance;
            manager.Add(this);
        }

        public void Cancel()
        {
            MsgManager.Instance.ClearMsg(this);
        }


        public static void CancelAll()
        {
            MsgManager.Instance.ClearAllMsg();
        }

        public void SetText(int resId)
        {
            SetText(mContext.GetText(resId));
        }


        public void SetText(string s)
        {
            if (mView == null)
            {
                throw new RuntimeException("This AppMsg was not created with AppMsg.makeText()");
            }
            var tv = (TextView)mView.FindViewById(Android.Resource.Id.Message);
            if (tv == null)
            {
                throw new RuntimeException("This AppMsg was not created with AppMsg.makeText()");
            }
            tv.Text = s;
        }

        public AppMsg SetDisplayPosition(DisplayPosition position)
        {
            var gravity = ConvertPosition(position);
            mLayoutParams = new FrameLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                                                         ViewGroup.LayoutParams.WrapContent, gravity);
            return this;
        }

        GravityFlags ConvertPosition(DisplayPosition position)
        {
            switch (position)
            {
                case DisplayPosition.Top:
                    return GravityFlags.Top;
                case DisplayPosition.Center:
                    return GravityFlags.Center;
                case DisplayPosition.Bottom:
                default:
                    return GravityFlags.Bottom;

            }
        }
        public void SetFloating(bool floating)
        {
            this.Floating = floating;
        }

    }
}