using OfficeEquipmentManager.Properties;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace OfficeEquipmentManager.MainResourses
{
    /// <summary>
    /// Interaction logic for ColorManagmentPage.xaml
    /// </summary>
    public partial class ColorManagmentPage : Page
    {
        public ColorManagmentPage()
        {
            InitializeComponent();

            DataContext = new ViewModel.ApplicationViewModel();
        }
        void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Frames.MainFrame.GoBack();
        }
    }
}