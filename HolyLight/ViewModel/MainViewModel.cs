namespace HolyLight.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;

    using HolyLight.DataModel;

    public class MainViewModel : ViewModelBase
    {
        public const string LyricPropertyName = "Lyric";

        private Lyric lyric = new Lyric();

        public Lyric Lyric
        {
            get
            {
                return lyric;
            }

            set
            {
                if (lyric == value)
                {
                    return;
                }

                lyric = value;
                RaisePropertyChanged(LyricPropertyName);
            }
        }
        public MainViewModel()
        {
            Messenger.Default.Register<Lyric>(this,Notification.Lyric, l =>
            {
                Lyric.Title = l.Title;
                Lyric.Content = l.Content;
            });
            CloseCommand = new RelayCommand(Close);
        }

        public RelayCommand CloseCommand { get; private set; }

        public void Close()
        {
            Messenger.Default.Send<string>(null,Notification.Close);
        }
    }
}
