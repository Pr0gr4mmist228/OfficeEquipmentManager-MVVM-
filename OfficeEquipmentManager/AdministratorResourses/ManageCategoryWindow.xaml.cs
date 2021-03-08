
using OfficeEquipmentManager.LocalDB;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace OfficeEquipmentManager.AdministratorResourses
{
    /// <summary>
    /// Interaction logic for ManageCategoryWindow.xaml
    /// </summary>
    public partial class ManageCategoryWindow : Window
    {
        public ManageCategoryWindow()
        {
            InitializeComponent();

            DataContext = new ViewModel.ApplicationViewModel();
        }
    }
}