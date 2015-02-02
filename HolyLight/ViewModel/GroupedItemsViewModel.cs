using System.Collections.Generic;
using DevExpress.Mvvm;
using DevExpress.Xpf.WindowsUI.Navigation;
using HolyLight.DataModel;

namespace HolyLight.ViewModel
{
    public class GroupedItemsViewModel : ViewModelBase, INavigationAware
    {
        private IEnumerable<SampleDataItem> items;
        public GroupedItemsViewModel()
        {
        }
        public IEnumerable<SampleDataItem> Items
        {
            get
            {
                return items;
            }
            private set
            {
                SetProperty<IEnumerable<SampleDataItem>>(ref items, value, "Items");
            }
        }
        public void LoadState(object navigationParameter)
        {
            Items = SampleDataSource.Instance.Items;
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
