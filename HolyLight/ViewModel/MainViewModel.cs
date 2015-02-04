namespace HolyLight.ViewModel
{
    using System.Text.RegularExpressions;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;

    using HolyLight.DataModel;
    using System.Collections.Generic;
    using System.Windows.Input;

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

        /// <summary>
        /// 歌词列表
        /// </summary>
        public List<Lyric> List { get; set; }

        /// <summary>
        /// 歌词页面数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 歌词页码
        /// </summary>
        public int Index { get; set; }

        public MainViewModel()
        {
            //注册接受歌词
            Messenger.Default.Register<List<Lyric>>(this,Notification.Lyric, l =>
            {
                SplistLyric(l);
                Count = List.Count;
                Lyric = List[Index];
            });

            //创建命令
            CloseCommand = new RelayCommand(Close);
            MouseDownCommand = new RelayCommand(MouseDown);
            NextCommand = new RelayCommand(Next);
            PrevCommand = new RelayCommand(Prev);
            KeyDownCommand = new RelayCommand<KeyEventArgs>(KeyDown);
        }

        /// <summary>
        /// 切分歌词
        /// </summary>
        /// <param name="lyrics">歌词对象</param>
        private void SplistLyric(List<Lyric> lyrics)
        {
            List = new List<Lyric>();
            foreach (var lyric in lyrics)
            {
                List<string> list = this.SplitBlock(lyric.Content);
                foreach (var str in list)
                {
                    Lyric l = new Lyric { Id = lyric.Id, Lid = lyric.Lid, Title = lyric.Title, Content = str };
                    List.Add(l);
                }
            }
        }

        /// <summary>
        /// 根据空白行切分文本
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns>字符数组</returns>
        private List<string> SplitBlock(string s)
        {
            List<string> list = new List<string>();
            foreach (var str in Regex.Split(s, @"(\r\n){2,}"))
            {
                if (!string.IsNullOrWhiteSpace(str))
                {
                    list.Add(str);
                }
            }
            return list;
        } 
        /// <summary>
        /// 关闭幻灯片命令
        /// </summary>
        public RelayCommand CloseCommand { get; private set; }

        /// <summary>
        /// 鼠标点击命令
        /// </summary>
        public RelayCommand MouseDownCommand { get; private set; }

        /// <summary>
        /// 下一张命令
        /// </summary>
        public RelayCommand NextCommand { get; private set; }

        /// <summary>
        /// 上一张命令
        /// </summary>
        public RelayCommand PrevCommand { get; private set; }

        /// <summary>
        /// 按键命令
        /// </summary>
        public RelayCommand<KeyEventArgs> KeyDownCommand { get; private set; }

        /// <summary>
        /// 关闭幻灯片
        /// </summary>
        public void Close()
        {
            Messenger.Default.Send<string>(null,Notification.Close);
        }

        /// <summary>
        /// 按键事件
        /// </summary>
        /// <param name="e"></param>
        public void KeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down: this.Next(); break;
                case Key.Right: this.Next(); break;
                case Key.Left: this.Prev(); break;
                case Key.Up: this.Prev(); break;
                case Key.Escape: Messenger.Default.Send<string>(null, Notification.Close); break;
                default:break;
            }
        }

        /// <summary>
        /// 鼠标点击事件
        /// </summary>
        public void MouseDown()
        {
            this.Next();
        }

        /// <summary>
        /// 下一张
        /// </summary>
        private void Next()
        {
            if (Index == Count - 1)
            {
                Messenger.Default.Send<string>(null, Notification.Close);
                Index = 0;
                return;
            }
            if (Index < Count - 1) Index++;
                Messenger.Default.Send<Lyric>(List[Index], Notification.Next);
        }

        /// <summary>
        /// 上一张
        /// </summary>
        private void Prev()
        {
            if (Index > 0) Index--; 
                Messenger.Default.Send<Lyric>(List[Index], Notification.Prev);
        }
    }
}
