using OfficeEquipmentManager.LocalDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

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

            listEquipment.ItemsSource = ContextConnector.db.Equipment.ToList();
        }

        private void Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new MainResourses.EquipmentEditWindow(listEquipment.SelectedItem as Equipment).ShowDialog();
        }

        void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Frames.MainFrame.GoBack();
        }
    }
}