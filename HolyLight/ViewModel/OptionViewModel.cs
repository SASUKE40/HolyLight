using HolyLight.DataModel;

namespace HolyLight.ViewModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    using Newtonsoft.Json;
    using GalaSoft.MvvmLight.Messaging;

    public class OptionViewModel : ViewModelBase
    {
        /// <summary>
        /// 幻灯片窗口
        /// </summary>
        private MainWindow mainWindow;

        /// <summary>
        /// 设置配置文件
        /// </summary>
        private readonly string OPTION_CONFIG_FILE = "Option.json";

        /// <summary>
        /// 数据库上下文对象
        /// </summary>
        private LyricContext db = new LyricContext();

        /// <summary>
        /// 友好提示信息
        /// </summary>
        private static readonly string TIPS = "目前版本号1.0.0，本功能暂未开发！";

        public const string OptionPropertyName = "Option";

        /// <summary>
        /// 设置参数对象
        /// </summary>
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

        private ICollectionView lyricView;
        public ICollectionView LyricView
        {
            get
            {
                if (lyricView == null)
                {
                    lyricView = CreateView(LyricCollection);
                }
                return lyricView;
            }
        }
        /// <summary>
        /// 创建集合视图
        /// </summary>
        /// <param name="source">集合</param>
        /// <returns>视图</returns>
        private ICollectionView CreateView(IEnumerable source)
        {
            var cvs = new CollectionViewSource();
            cvs.Source = source;
            return cvs.View;
        }

        public OptionViewModel()
        {
            if (File.Exists(OPTION_CONFIG_FILE))
            {
                //配置文件存在，则读取配置文件
                Option = JsonConvert.DeserializeObject<Option>(File.ReadAllText(OPTION_CONFIG_FILE));
            }
            else
            {
                //如果配置文件不存在，则创建一个默认配置文件
                File.WriteAllText(OPTION_CONFIG_FILE, JsonConvert.SerializeObject(Option, Formatting.Indented));
            }
            //加载数据库
            LyricCollection = this.db.Lyrics.Local;
            db.Lyrics.Load();
            //创建命令
            SubmitCommand = new RelayCommand(this.Submit);
            StartCommand = new RelayCommand(this.Start);
            SaveCommand = new RelayCommand(this.Save);
            EditCommand = new RelayCommand(this.Edit);
            DeleteCommand = new RelayCommand(this.Delete);
            HelpCommand = new RelayCommand(this.Help);
            KeyDownCommand = new RelayCommand<KeyEventArgs>(KeyDown);
        }
        /// <summary>
        /// 提交新的歌词命令
        /// </summary>
        public RelayCommand SubmitCommand { get; private set; }

        /// <summary>
        /// 开始播放命令
        /// </summary>
        public RelayCommand StartCommand { get; private set; }

        /// <summary>
        /// 保存配置参数命令
        /// </summary>
        public RelayCommand SaveCommand { get; private set; }

        /// <summary>
        /// 编辑歌词命令
        /// </summary>
        public RelayCommand EditCommand { get; private set; }

        /// <summary>
        /// 删除歌词命令
        /// </summary>
        public RelayCommand DeleteCommand { get; private set; }

        /// <summary>
        /// 按键命令
        /// </summary>
        public RelayCommand<KeyEventArgs> KeyDownCommand { get; private set; }

        /// <summary>
        /// 帮助命令
        /// </summary>
        public RelayCommand HelpCommand { get; private set; }
        public const string LyricCollectionPropertyName = "LyricCollection";

        private ObservableCollection<Lyric> _lyricCollection;
        public ObservableCollection<Lyric> LyricCollection
        {
            get
            {
                return _lyricCollection;
            }

            set
            {
                if (_lyricCollection == value)
                {
                    return;
                }

                _lyricCollection = value;
                RaisePropertyChanged(LyricCollectionPropertyName);
            }
        }

        /// <summary>
        /// 提交歌词
        /// </summary>
        public void Submit()
        {
            db.Lyrics.Add(Lyric);
            db.SaveChanges();
        }
        
        /// <summary>
        /// 开始播放幻灯片
        /// </summary>
        public void Start()
        {
            List = new List<Lyric>();
            try
            {
                //根据Lid查找对应歌词信息
                Lyric l1= LyricCollection.Where(l => l.Lid == Option.First).FirstOrDefault();
                if(l1!= null)List.Add(l1);
                Lyric l2= LyricCollection.Where(l => l.Lid == Option.Second).FirstOrDefault();
                if(l2!= null)List.Add(l2);
                Lyric l3= LyricCollection.Where(l => l.Lid == Option.Third).FirstOrDefault();
                if(l3!= null)List.Add(l3);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            //显示幻灯片窗口
            mainWindow = new MainWindow();
            mainWindow.Show();
            //发送配置信息
            Messenger.Default.Send<Option>(Option, Notification.Option);
            //发送歌词列表
            Messenger.Default.Send<List<Lyric>>(List, Notification.Lyric);
        }

        /// <summary>
        /// 按键事件
        /// </summary>
        /// <param name="e"></param>
        public void KeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F5:this.Start();
                    break;
                case Key.F1: this.Help();
                    break;
                default: break;
            }
        }

        /// <summary>
        /// 歌词列表
        /// </summary>
        public List<Lyric> List { get; set; }

        /// <summary>
        /// 保存配置信息
        /// </summary>
        public void Save()
        {
            File.WriteAllText(OPTION_CONFIG_FILE, JsonConvert.SerializeObject(Option, Formatting.Indented));
        }

        /// <summary>
        /// 编辑歌词
        /// </summary>
        public void Edit()
        {
            MessageBox.Show(TIPS, "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        /// <summary>
        /// 删除歌词
        /// </summary>
        public void Delete()
        {
            MessageBox.Show(TIPS, "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        /// <summary>
        /// 帮助
        /// </summary>
        public void Help()
        {
            MessageBox.Show("操作帮助：\r\nF1：帮助\r\nF5：播放\r\n→/↓：下一张\r\n←/↑：上一张\r\nEsc：退出\r\n\r\n关于：\r\n作者：SASUKE\r\nCopyright©2015 乐恩教会", "帮助");
        }

    }
    
}
