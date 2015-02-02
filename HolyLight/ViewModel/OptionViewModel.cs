using DevExpress.Mvvm;
using HolyLight.DataModel;

namespace HolyLight.ViewModel
{
    using System;
    using System.IO;

    using DevExpress.Mvvm.DataAnnotations;
    using System.Windows.Media;

    using Newtonsoft.Json;

    public class OptionViewModel : ViewModelBase
    {
        private MainWindow mainWindow;
        private Lyric lyric = new Lyric();

        private readonly string OPTION_CONFIG_FILE = "Option.json";
        protected MainViewModel MainViewModel { get; private set; }

        private Option option;
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
            if (System.IO.File.Exists(OPTION_CONFIG_FILE))
            {
                Option = JsonConvert.DeserializeObject<Option>(System.IO.File.ReadAllText(OPTION_CONFIG_FILE));
            }
            else
            {
                Option = new Option();
                System.IO.File.WriteAllText(OPTION_CONFIG_FILE, JsonConvert.SerializeObject(Option, Formatting.Indented));
            }
        }
        [Command]
        public void Submit(){
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
            System.IO.File.WriteAllText(OPTION_CONFIG_FILE, JsonConvert.SerializeObject(Option, Formatting.Indented));
        }
        
    }
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

        public override string ToString()
        {
            return string.Format("First: {0}, Second: {1}, Third: {2}, DisplayDateTime: {3}, ShowTitle: {4}, ShowDateTime: {5}, BackgroundColor: {6}, TitleColor: {7}, ContentColor: {8}", this.First, this.Second, this.Third, this.DisplayDateTime, this.ShowTitle, this.ShowDateTime, this.BackgroundColor, this.TitleColor, this.ContentColor);
        }
    }
}
