using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HolyLight.DataModel
{
    using System.Windows.Media;

    public class Option
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Third { get; set; }

        public DateTime DisplayDateTime { get; set; }

        public bool ShowTitle { get; set; }

        public bool ShowDateTime { get; set; }

        public Color BackgroundColor { get; set; }

        public Color TitleColor { get; set; }

        public Color ContentColor { get; set; }

        public string TitleFontFamily { get; set; }

        public int TitleFontSize { get; set; }

        public string ContentFontFamily { get; set; }

        public int ContentFontSize { get; set; }

        public override string ToString()
        {
            return string.Format("First: {0}, Second: {1}, Third: {2}, DisplayDateTime: {3}, ShowTitle: {4}, ShowDateTime: {5}, BackgroundColor: {6}, TitleColor: {7}, ContentColor: {8}, TitleFontFamily: {9}, TitleFontSize: {10}, ContentFontFamily: {11}, ContentFontSize: {12}", this.First, this.Second, this.Third, this.DisplayDateTime, this.ShowTitle, this.ShowDateTime, this.BackgroundColor, this.TitleColor, this.ContentColor, this.TitleFontFamily, this.TitleFontSize, this.ContentFontFamily, this.ContentFontSize);
        }
    }
}
