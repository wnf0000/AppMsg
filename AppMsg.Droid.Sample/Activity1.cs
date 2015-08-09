using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace AppMsg.Droid.Sample
{
    [Activity(Label = "AppMsg.Droid.Sample", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button1 = FindViewById<Button>(Resource.Id.MyButton1);
            button1.Click += delegate { AppMsg.MakeText(this,"顶部显示",AppMsg.StyleInfo).SetDisplayPosition(DisplayPosition.Top).Show();};

            Button button2 = FindViewById<Button>(Resource.Id.MyButton2);
            button2.Click += delegate { AppMsg.MakeText(this, "中间显示", AppMsg.StyleInfo).SetDisplayPosition(DisplayPosition.Center).Show(); };
            Button button3 = FindViewById<Button>(Resource.Id.MyButton3);
            button3.Click += delegate { AppMsg.MakeText(this, "底部显示", AppMsg.StyleInfo).SetDisplayPosition(DisplayPosition.Bottom).Show(); };

            Button button4 = FindViewById<Button>(Resource.Id.MyButton4);
            button4.Click += delegate { AppMsg.MakeText(this, "显示成功", AppMsg.StyleSuccess).Show(); };

            Button button5 = FindViewById<Button>(Resource.Id.MyButton5);
            button5.Click += delegate { AppMsg.MakeText(this, "显示错误", AppMsg.StyleError).SetDisplayPosition(DisplayPosition.Bottom).Show(); };
        }
    }
}

