using System;
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
        }

        void ButtonAuthorization_Click(object sender, RoutedEventArgs e)
        {
            Frames.MainFrame.GoBack();
        }

    }
}