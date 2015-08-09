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
                    new StringElement("������ʾ", () =>
                    {
                        AppMsg.MakeText(this, "������ʾ", AppMsg.StyleInfo).SetDisplayPosition(DisplayPosition.Top).Show();
                    }),
                     new StringElement("�м���ʾ", () =>
                    {
                        AppMsg.MakeText(this, "�м���ʾ", AppMsg.StyleInfo).SetDisplayPosition(DisplayPosition.Center).Show();
                    }),
                    new StringElement("�ײ���ʾ", () =>
                    {
                        AppMsg.MakeText(this, "�ײ���ʾ", AppMsg.StyleInfo).SetDisplayPosition(DisplayPosition.Bottom).Show();
                    }),
                    new StringElement("��ʾ�ɹ�", () =>
                    {
                        AppMsg.MakeText(this, "��ʾ�ɹ�", AppMsg.StyleSuccess).Show();
                    }),
                    new StringElement("��ʾʧ��", () =>
                    {
                        AppMsg.MakeText(this, "��ʾʧ��", AppMsg.StyleError).Show();
                    })
				},
			};
            
        }
    }
}