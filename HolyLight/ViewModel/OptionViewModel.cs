using DevExpress.Mvvm;
using HolyLight.DataModel;

namespace HolyLight.ViewModel
{
    using System;
    using DevExpress.Mvvm.DataAnnotations;
    using System.Windows.Media;

    using LiteDB;

    public class OptionViewModel : ViewModelBase
    {
        private MainWindow mainWindow;
        private Lyric lyric = new Lyric();
        protected MainViewModel MainViewModel { get; private set; }

        protected IDialogService DialogService
        {
            get
            {
                return GetService<IDialogService>();
            }
        }

        private Option option = new Option();
        public Option Option
        {
            get { return option; }
            set { option = value; }
        }
        

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
        public OptionViewModel()
        {
        }
        [Command]
        public void Submit()
        {
            MainViewModel = new MainViewModel(Lyric);
            mainWindow = new MainWindow();
            mainWindow.DataContext = MainViewModel;
            mainWindow.Show();
        }
        [Command]
        public void File()
        {
        }
        [Command]
        public void Start()
        {
        }

        [Command]
        public void Save()
        {
            using (var db = new LiteEngine(@"Option.db"))
            {
                Collection<Option> options = db.GetCollection<Option>("Options");
                Option.Id = 1;
                Option option = options.FindById(1);
                Console.WriteLine(option);
                options.Update(Option);
            }
//            Console.WriteLine(Option);
        }}
    public class Option
    {
        [BsonId]
        public int Id { get; set; }

        public int First { get; set; }

        public int Second { get; set; }

        public int Third { get; set; }

        public DateTime DisplayDateTime { get; set; }

        public bool ShowTitle { get; set; }

        public bool ShowDateTime { get; set; }

        public Color BackgroundColor { get; set; }

        public Color TitleColor { get; set; }

        public Color ContentColor { get; set; }

        public override string ToString()
        {
            return string.Format("First: {0}, Second: {1}, Third: {2}, DisplayDateTime: {3}, ShowTitle: {4}, ShowDateTime: {5}, BackgroundColor: {6}, TitleColor: {7}, ContentColor: {8}", this.First, this.Second, this.Third, this.DisplayDateTime, this.ShowTitle, this.ShowDateTime, this.BackgroundColor, this.TitleColor, this.ContentColor);
        }
    }
}
