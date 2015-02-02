using DevExpress.Mvvm;
using HolyLight.DataModel;

namespace HolyLight.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.IO;

    using System.Windows.Media;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    using Newtonsoft.Json;
    using GalaSoft.MvvmLight.Messaging;

    public class OptionViewModel : ViewModelBase
    {
        private MainWindow mainWindow;

        private readonly string OPTION_CONFIG_FILE = "Option.json";
        protected MainViewModel MainViewModel { get; private set; }


        public const string OptionPropertyName = "Option";

        private Option option = new Option();

        public Option Option
        {
            get
            {
                return option;
            }

            set
            {
                if (option == value)
                {
                    return;
                }

                option = value;
                RaisePropertyChanged(OptionPropertyName);
            }
        }
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
        public OptionViewModel()
        {
            if (File.Exists(OPTION_CONFIG_FILE))
            {
                Option = JsonConvert.DeserializeObject<Option>(File.ReadAllText(OPTION_CONFIG_FILE));
            }
            else
            {
                File.WriteAllText(OPTION_CONFIG_FILE, JsonConvert.SerializeObject(Option, Formatting.Indented));
            }
            using (var db = new LyricContext())
            {
                LyricCollection = db.Lyrics.Local;
                db.Lyrics.Load();
            }
            SubmitCommand = new RelayCommand(this.Submit);
            StartCommand = new RelayCommand(this.Start);
            SaveCommand = new RelayCommand(this.Save);
        }

        public RelayCommand SubmitCommand { get; private set; }
        public RelayCommand StartCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public ObservableCollection<Lyric> LyricCollection { get; set; }

        public void Submit(){
            using (var db = new LyricContext())
            {
                db.Lyrics.Add(Lyric);
                db.SaveChanges();
                LyricCollection = db.Lyrics.Local;
                db.Lyrics.Load();
            }
        }


        public void Start()
        {
            mainWindow = new MainWindow();
            mainWindow.Show();
            Messenger.Default.Send<Option>(Option, Notification.Option);
            Messenger.Default.Send<Lyric>(Lyric, Notification.Lyric);
        }

        public void Save()
        {
            File.WriteAllText(OPTION_CONFIG_FILE, JsonConvert.SerializeObject(Option, Formatting.Indented));
        }
        
    }
    
}
