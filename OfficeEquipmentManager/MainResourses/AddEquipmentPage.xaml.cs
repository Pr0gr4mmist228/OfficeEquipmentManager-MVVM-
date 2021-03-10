
using Microsoft.Win32;
using OfficeEquipmentManager.LocalDB;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

            DataContext = new ViewModel.ApplicationViewModel();
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