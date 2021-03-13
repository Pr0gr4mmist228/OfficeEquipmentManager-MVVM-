using OfficeEquipmentManager.LocalDB;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OfficeEquipmentManager
{
    /// <summary>
    /// Interaction logic for EquipmentListManagmentPage.xaml
    /// </summary>
    public partial class EquipmentListManagmentPage : Page
    {
        public EquipmentListManagmentPage()
        {
            InitializeComponent();
        }

        private void Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new MainResourses.EquipmentEditWindow(listEquipment.SelectedItem as Equipment).ShowDialog();
        }
    }
}