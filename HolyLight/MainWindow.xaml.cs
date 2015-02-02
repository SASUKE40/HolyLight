using DevExpress.Mvvm;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace HolyLight
{
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

