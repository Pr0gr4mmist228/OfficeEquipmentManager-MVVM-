using Microsoft.Win32;
using OfficeEquipmentManager.LocalDB;
using System;
using System.Windows;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;

namespace OfficeEquipmentManager.MainResourses
{
    /// <summary>
    /// Interaction logic for AddEquipmentFromTxtPage.xaml
    /// </summary>
    public partial class AddEquipmentFromTxtPage : Page
    {
        public AddEquipmentFromTxtPage()
        {
            InitializeComponent();

            DataContext = new ViewModel.ApplicationViewModel();
        }
    }
}