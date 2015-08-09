using System;

namespace AppMsg.Droid
{
    public class Style:IEquatable<Style>
    {
        public Style(int duration, int resId)
        {
            Duration = duration;
            Background = resId;
        }

        public int Duration { private set; get; }


        public int Background { private set; get; }

        public bool Equals(Style other)
        {
            if (other == null) return false;
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