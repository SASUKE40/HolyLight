
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace HolyLight
{
    using System.Windows;
    using System.Windows.Media;

    using GalaSoft.MvvmLight.Messaging;


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<Storyboard>(this, s =>
            {
                s.Begin();
            });
            Messenger.Default.Register<string>(this, Notification.Close, s => this.Close());
            Messenger.Default.Register<HolyLight.DataModel.Option>(this, Notification.Option, o =>
                {
                        this.TitleLabel.Foreground = new SolidColorBrush(o.TitleColor); 
                        this.ContentTextBlock.Foreground = new SolidColorBrush(o.ContentColor);
                        this.BackgroundPanel.Background = new SolidColorBrush(o.BackgroundColor);
                        this.DateTimeLabel.Content = o.DisplayDateTime.ToLongDateString();
                        if (o.ShowDateTime)
                        {
                            this.DateTimeLabel.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            this.DateTimeLabel.Visibility = Visibility.Hidden;
                        }
                    });
        }

        private void layoutGroup_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                var fadeOutStoryboard = FindResource("FadeOutStoryboard") as Storyboard;
                fadeOutStoryboard.Begin();
                fadeOutStoryboard.Completed += FadeOutStoryboard_Completed;
            }
        }

        private void FadeOutStoryboard_Completed(object sender, System.EventArgs e)
        {
            var fadeInStoryboard = FindResource("FadeInStoryboard") as Storyboard;
            fadeInStoryboard.Begin();
        }
    }
}

