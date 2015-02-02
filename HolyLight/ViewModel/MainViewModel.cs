namespace HolyLight.ViewModel
{
    using System.Windows.Media.Animation;

    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;

    using HolyLight.DataModel;

    public class MainViewModel : ViewModelBase
    {
        private Lyric lyric = new Lyric();

        public Lyric Lyric
        {
            get
            {
                return lyric;
            }
            set
            {
                lyric = value;
            }
        }
        public MainViewModel()
        {
        }
        public MainViewModel(Lyric lyric)
        {
            Lyric = lyric;
            Messenger.Default.Register<Lyric>(this, l =>
            {
                Lyric.Title = l.Title;
                Lyric.Content = l.Content;
            });
        }
    }
}
