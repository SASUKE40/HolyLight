using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm;








namespace HolyLight.DataModel
{
    /// <summary>
    /// Base class for <see cref="SampleDataItem"/> and <see cref="SampleDataGroup"/> that
    /// defines properties common to both.
    /// </summary>
    public abstract class SampleDataCommon : ViewModelBase
    {
        private static Uri _baseUri = new Uri("pack://application:,,,");
        private static BitmapImage GetImage(string path)
        {
#if SILVERLIGHT
            return new BitmapImage(new Uri("../"  + path, UriKind.RelativeOrAbsolute));
#else
            return new BitmapImage(new Uri(_baseUri, path));
#endif
        }
        private static int count;
        private static string GetUniqueId()
        {
            return "Item" + count++;
        }
        public SampleDataCommon(String title, String subtitle, String imagePath, String description)
        {
            _uniqueId = GetUniqueId();
            _title = title;
            _subtitle = subtitle;
            _description = description;
            _imagePath = imagePath;
        }
        private string _uniqueId = string.Empty;
        public string UniqueId
        {
            get
            {
                return _uniqueId;
            }
        }
        private string _title = string.Empty;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                SetProperty(ref _title, value, "Title");
            }
        }
        private string _subtitle = string.Empty;
        public string Subtitle
        {
            get
            {
                return _subtitle;
            }
            set
            {
                SetProperty(ref _subtitle, value, "Subtitle");
            }
        }
        private string _description = string.Empty;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                SetProperty(ref _description, value, "Description");
            }
        }
        private ImageSource _image = null;
        private String _imagePath = null;
        public ImageSource Image
        {
            get
            {
                if (_image == null && _imagePath != null)
                {
                    _image = SampleDataCommon.GetImage(_imagePath);
                }
                return _image;
            }
            set
            {
                _imagePath = null;
                SetProperty(ref _image, value, "Image");
            }
        }
        public override string ToString()
        {
            return Title;
        }
    }

    /// <summary>
    /// Generic item data model.
    /// </summary>
    public class SampleDataItem : SampleDataCommon
    {
        public SampleDataItem(String title, String subtitle, String imagePath, String description, String content)
            : this(title, subtitle, imagePath, description, content, false, string.Empty)
        {
        }
        public SampleDataItem(String title, String subtitle, String imagePath, String description, String content, bool isFlowBreak, string groupHeader)
            : base(title, subtitle, imagePath, description)
        {
            _content = content;
            IsFlowBreak = isFlowBreak;
            GroupHeader = groupHeader;
        }
        private string _GroupHeader;
        private bool _IsFlowBreak;
        private string _content = string.Empty;
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                SetProperty(ref _content, value, "Content");
            }
        }

        public bool IsFlowBreak
        {
            get
            {
                return _IsFlowBreak;
            }
            set
            {
                SetProperty(ref _IsFlowBreak, value, "IsFlowBreak");
            }
        }

        public string GroupHeader
        {
            get
            {
                return _GroupHeader;
            }
            set
            {
                SetProperty(ref _GroupHeader, value, "GroupHeader");
            }
        }
    }

    /// <summary>
    /// Creates a collection of groups and items with hard-coded content.
    ///
    /// SampleDataSource initializes with placeholder data rather than live production
    /// data so that sample data is provided at both design-time and run-time.
    /// </summary>
    public sealed class SampleDataSource
    {
        public static SampleDataSource Instance
        {
            get
            {
                return _sampleDataSource;
            }
        }
        private static readonly SampleDataSource _sampleDataSource = new SampleDataSource();

        private ObservableCollection<SampleDataItem> _items;
        public ObservableCollection<SampleDataItem> Items
        {
            get
            {
                return Instance._items;
            }
        }

        public static SampleDataItem GetItem(string uniqueId)
        {
            var matches = SampleDataSource.Instance.Items.Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1)
            {
                return matches.First();
            }
            return null;
        }

        public SampleDataSource()
        {
            _items = new ObservableCollection<SampleDataItem>();
            var ITEM_CONTENT = String.Format("Item Content: {0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}",
                        "Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat");
            var ITEM_DESCRIPTION = "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.";

            _items.Add(new SampleDataItem(
                    "Item Title: 1",
                    "Item Subtitle: 1",
                    "Assets/LightGray.png",
                    ITEM_DESCRIPTION,
                    ITEM_CONTENT, true, "Group 1"));
            _items.Add(new SampleDataItem(
                    "Item Title: 2",
                    "Item Subtitle: 2",
                    "Assets/DarkGray.png",
                    ITEM_DESCRIPTION,
                    ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                    "Item Title: 3",
                    "Item Subtitle: 3",
                    "Assets/MediumGray.png",
                    ITEM_DESCRIPTION,
                    ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                    "Item Title: 4",
                    "Item Subtitle: 4",
                    "Assets/DarkGray.png",
                    ITEM_DESCRIPTION,
                    ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                    "Item Title: 5",
                    "Item Subtitle: 5",
                    "Assets/DarkGray.png",
                    ITEM_DESCRIPTION,
                    ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                    "Item Title: 6",
                    "Item Subtitle: 6",
                    "Assets/DarkGray.png",
                    ITEM_DESCRIPTION,
                    ITEM_CONTENT));


            _items.Add(new SampleDataItem(
                    "Item Title: 1",
                    "Item Subtitle: 1",
                    "Assets/MediumGray.png",
                    ITEM_DESCRIPTION,
                    ITEM_CONTENT, true, "Group 2"));
            _items.Add(new SampleDataItem(
                    "Item Title: 2",
                    "Item Subtitle: 2",
                    "Assets/DarkGray.png",
                    ITEM_DESCRIPTION,
                    ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                    "Item Title: 3",
                    "Item Subtitle: 3",
                    "Assets/DarkGray.png",
                    ITEM_DESCRIPTION,
                    ITEM_CONTENT));

            _items.Add(new SampleDataItem(
                    "Item Title: 1",
                    "Item Subtitle: 1",
                    "Assets/MediumGray.png",
                    ITEM_DESCRIPTION,
                    ITEM_CONTENT, true, "Group 3"));
            _items.Add(new SampleDataItem(
                    "Item Title: 2",
                    "Item Subtitle: 2",
                    "Assets/DarkGray.png",
                    ITEM_DESCRIPTION,
                    ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                    "Item Title: 3",
                    "Item Subtitle: 3",
                    "Assets/LightGray.png",
                    ITEM_DESCRIPTION,
                    ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                    "Item Title: 4",
                    "Item Subtitle: 4",
                    "Assets/DarkGray.png",
                    ITEM_DESCRIPTION,
                    ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                    "Item Title: 5",
                    "Item Subtitle: 5",
                    "Assets/DarkGray.png",
                    ITEM_DESCRIPTION,
                    ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                    "Item Title: 6",
                    "Item Subtitle: 6",
                    "Assets/DarkGray.png",
                    ITEM_DESCRIPTION,
                    ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                    "Item Title: 7",
                    "Item Subtitle: 7",
                    "Assets/DarkGray.png",
                    ITEM_DESCRIPTION,
                    ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                    "Item Title: 8",
                    "Item Subtitle: 8",
                    "Assets/DarkGray.png",
                    ITEM_DESCRIPTION,
                    ITEM_CONTENT));

            _items.Add(new SampleDataItem(
                   "Item Title: 1",
                   "Item Subtitle: 1",
                   "Assets/MediumGray.png",
                   ITEM_DESCRIPTION,
                   ITEM_CONTENT, true, "Group 4"));
            _items.Add(new SampleDataItem(
                    "Item Title: 2",
                    "Item Subtitle: 2",
                    "Assets/DarkGray.png",
                    ITEM_DESCRIPTION,
                    ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                    "Item Title: 3",
                    "Item Subtitle: 3",
                    "Assets/LightGray.png",
                    ITEM_DESCRIPTION,
                    ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                    "Item Title: 4",
                    "Item Subtitle: 4",
                    "Assets/DarkGray.png",
                    ITEM_DESCRIPTION,
                    ITEM_CONTENT));
        }
    }
}
