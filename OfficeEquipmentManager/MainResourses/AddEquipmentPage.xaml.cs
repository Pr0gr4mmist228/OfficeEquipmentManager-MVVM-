using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace OfficeEquipmentManager
{
    /// <summary>
    /// Interaction logic for AddEquipmentPage.xaml
    /// </summary>
    public partial class AddEquipmentPage : Page
    {
        public AddEquipmentPage()
        {
            InitializeComponent();
        }

        void EquipmentQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) < 0;
        }
        void ImageLinkBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            // image.UriSource = new Uri(imageLinkBox.Text);
            image.EndInit();

            // equipmentImage.Source = image;
        }
    }
}