
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace HolyLight
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Media;

    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Threading;

    using HolyLight.DataModel;
    using System.Windows.Threading;
    using System;





    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public Lyric Lyric { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            //注册窗口关闭
            Messenger.Default.Register<string>(this, Notification.Close, s => this.Close());

            //注册应用配置
            Messenger.Default.Register<Option>(this, Notification.Option, o =>
                {
                    this.TitleLabel.Foreground = new SolidColorBrush(o.TitleColor);
                    this.ContentTextBlock.Foreground = new SolidColorBrush(o.ContentColor);
                    this.BackgroundPanel.Background = new SolidColorBrush(o.BackgroundColor);
                    this.DateTimeLabel.Content = o.DisplayDateTime.ToLongDateString();
                    this.TitleLabel.FontSize = o.TitleFontSize;
                    this.ContentTextBlock.FontSize = o.ContentFontSize;
                    this.TitleLabel.FontFamily = new FontFamily(o.TitleFontFamily);
                    this.ContentTextBlock.FontFamily = new FontFamily(o.ContentFontFamily);
                    if (o.ShowDateTime)
                    {
                        this.DateTimeLabel.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        this.DateTimeLabel.Visibility = Visibility.Hidden;
                    }
                    if (o.ShowTitle){
                        this.TitleLabel.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        this.TitleLabel.Visibility = Visibility.Hidden;
                    }
                        
                });

            //注册播放下一张
            Messenger.Default.Register<Lyric>(this, Notification.Next, l =>
                {
                    Lyric = l;
                    var fadeOutStoryboard = FindResource("FadeOutStoryboard") as Storyboard;
                    fadeOutStoryboard.Completed += FadeOutStoryboard_Completed;
                    fadeOutStoryboard.Begin();
                });

            //注册播放上一张
            Messenger.Default.Register<Lyric>(this, Notification.Prev, l =>
            {
                Lyric = l;
                var fadeOutStoryboard = FindResource("FadeOutStoryboard") as Storyboard;
                fadeOutStoryboard.Completed += FadeOutStoryboard_Completed;
                fadeOutStoryboard.Begin();
            });
        }

        /// <summary>
        /// 动画播放完毕事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FadeOutStoryboard_Completed(object sender, EventArgs e)
        {
            //修改歌词
            this.TitleLabel.Content = Lyric.Title;
            this.ContentTextBlock.Text = Lyric.Content;
            var fadeInStoryboard = FindResource("FadeInStoryboard") as Storyboard;
            fadeInStoryboard.Begin();
        }
        private DispatcherTimer Timer_MouseMove;

        /// <summary>
        /// 监听鼠标，如果鼠标不动则隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Timer_MouseMove = new DispatcherTimer();
            this.Timer_MouseMove.Tick += this.Timer_MouseMove_Tick;
            this.Timer_MouseMove.Interval = new TimeSpan(0, 0, 1);
            this.Timer_MouseMove.Start();
        }

        /// <summary>
        /// 监听鼠标，如果鼠标不动则隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_MouseMove_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!MouseMonitorHelper.HaveUsedTo())
                {
                    MouseMonitorHelper.CheckCount++;
                    if (MouseMonitorHelper.CheckCount == 3)
                    {
                        MouseMonitorHelper.CheckCount = 0;
                        // 关闭按钮隐藏、鼠标隐藏
                        Mouse.OverrideCursor = Cursors.None;
                    }
                }
                else MouseMonitorHelper.CheckCount = 0;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 监听鼠标，如果鼠标移动则显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.Timer_MouseMove.Stop();
            Mouse.OverrideCursor = Cursors.Arrow;
        }
    }
}

