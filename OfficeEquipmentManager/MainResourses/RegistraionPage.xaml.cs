
using Microsoft.Win32;
using OfficeEquipmentManager.LocalDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace OfficeEquipmentManager
{
    /// <summary>
    /// Interaction logic for RegistraionPage.xaml
    /// </summary>
    public partial class RegistraionPage : Page
    {
        public RegistraionPage()
        {
            InitializeComponent();

            DataContext = new ViewModel.ApplicationViewModel();
        }

        void ImageLinkBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(imageLinkBox.Text);
            image.EndInit();

            userImage.Source = image;
        }
        void ButtonAuthorization_Click(object sender, RoutedEventArgs e)
        {
            Frames.MainFrame.GoBack();
        }

    }
}