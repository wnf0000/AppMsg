# AppMsg
采用Xamarin开发的提示消息组件，支持Android/iOS两个平台<br/>
Android用法
<pre>
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
</pre>
iOS用法
<pre>
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
</pre>
效果图 Android<br/>
<img src="https://github.com/wnf0000/AppMsg/blob/master/%E6%88%AA%E5%9B%BE/QQ%E6%88%AA%E5%9B%BE20140527205823.png"/>
<img src="https://github.com/wnf0000/AppMsg/blob/master/%E6%88%AA%E5%9B%BE/QQ%E6%88%AA%E5%9B%BE20140527205831.png"/>
<img src="https://github.com/wnf0000/AppMsg/blob/master/%E6%88%AA%E5%9B%BE/QQ%E6%88%AA%E5%9B%BE20140527205952.png"/>
效果图 iOS<br/>
<img src="https://github.com/wnf0000/AppMsg/blob/master/%E6%88%AA%E5%9B%BE/iOS%20Simulator%20Screen%20Shot%202015%E5%B9%B48%E6%9C%889%E6%97%A5%20%E4%B8%8B%E5%8D%882.40.38.png"/>
<img src="https://github.com/wnf0000/AppMsg/blob/master/%E6%88%AA%E5%9B%BE/iOS%20Simulator%20Screen%20Shot%202015%E5%B9%B48%E6%9C%889%E6%97%A5%20%E4%B8%8B%E5%8D%882.40.58.png"/>
<img src="https://github.com/wnf0000/AppMsg/blob/master/%E6%88%AA%E5%9B%BE/iOS%20Simulator%20Screen%20Shot%202015%E5%B9%B48%E6%9C%889%E6%97%A5%20%E4%B8%8B%E5%8D%882.41.05.png"/>
<img src="https://github.com/wnf0000/AppMsg/blob/master/%E6%88%AA%E5%9B%BE/iOS%20Simulator%20Screen%20Shot%202015%E5%B9%B48%E6%9C%889%E6%97%A5%20%E4%B8%8B%E5%8D%882.41.11.png"/>
