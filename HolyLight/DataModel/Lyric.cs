using System;
using System.Collections.Generic;
using System.Linq;

namespace HolyLight.DataModel
{
    public class Lyric
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public override string ToString()
        {
            return string.Format("Title: {0}, Content: {1}", Title, Content);
        }
    }
}
