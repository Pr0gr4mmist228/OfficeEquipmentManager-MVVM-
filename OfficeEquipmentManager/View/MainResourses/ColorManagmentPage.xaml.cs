using System.Windows;
using System.Windows.Controls;

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
        }
        void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Frames.MainFrame.GoBack();
        }
    }
}