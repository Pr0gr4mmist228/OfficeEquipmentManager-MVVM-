using OfficeEquipmentManager.LocalDB;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace OfficeEquipmentManager.AdministratorResourses
{
    /// <summary>
    /// Interaction logic for AddCategoryWindow.xaml
    /// </summary>
    public partial class AddCategoryWindow : Window
    {
        public AddCategoryWindow()
        {
            InitializeComponent();

            DataContext = new ViewModel.ApplicationViewModel();
        }
    }
}