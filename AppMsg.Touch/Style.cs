using System;
using MonoTouch.UIKit;

namespace AppMsg.Touch
{
    public class Style:IEquatable<Style>
    {
        public Style(int duration, UIColor color)
        {
            Duration = duration;
            Background = color;
        }

        public int Duration { private set; get; }

        public UIColor Background { private set; get; }

        public bool Equals(Style other)
        {
            if (other==null) return false;
            return other.Duration == Duration && other.Background == Background;
        }

        //public override bool Equals(object obj)
        //{
        //    if (!(obj is Style)) return false;
        //    var style = obj as Style;
        //    return style.Duration == Duration && style.Background == Background;
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
        //}
    }
}