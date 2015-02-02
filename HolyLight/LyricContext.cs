namespace HolyLight
{
    using System.Data.Entity;

    using HolyLight.DataModel;

    public class LyricContext:DbContext
    {
        public DbSet<Lyric> Lyrics { get; set; } 
    }
}