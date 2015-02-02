using DevExpress.Mvvm;
using DevExpress.Xpf.WindowsUI.Navigation;
using HolyLight.DataModel;

namespace HolyLight.ViewModel
{
    public class ItemDetailViewModel : ViewModelBase, INavigationAware
    {
        private SampleDataItem selectedItem;
        public ItemDetailViewModel()
        {
        }
        public SampleDataItem SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                SetProperty<SampleDataItem>(ref selectedItem, value, "SelectedItem");
            }
        }
        private void LoadState(object navigationParameter)
        {
            var item = SampleDataSource.GetItem((string)navigationParameter);
            SelectedItem = item;
        }

        public void NavigatedFrom(NavigationEventArgs e)
        {
        }
        public void NavigatedTo(NavigationEventArgs e)
        {
            LoadState(e.Parameter);
        }
        public void NavigatingFrom(NavigatingEventArgs e)
        {
        }
    }
}
