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

            DataContext = new ViewModel.ApplicationViewModel();
        }

        private void Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new MainResourses.EquipmentEditWindow(listEquipment.SelectedItem as Equipment).ShowDialog();
        }

        void ButtonEquipmentPlus_Click(object sender, RoutedEventArgs e)
        {
            Equipment clickedEquipment = listEquipment.SelectedItem as Equipment;
            clickedEquipment.Quantity += 1;
            ContextConnector.db.SaveChanges();
            listEquipment.ItemsSource = ContextConnector.db.Equipment.ToList();
        }
        void ButtonEquipmentMinus_Click(object sender, RoutedEventArgs e)
        {
            Equipment clickedEquipment = listEquipment.SelectedItem as Equipment;
            clickedEquipment.Quantity -= 1;
            ContextConnector.db.SaveChanges();
            listEquipment.ItemsSource = ContextConnector.db.Equipment.ToList();
        }
        void TextBoxQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) < 0;
        }
        void TextBoxQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox clickedTextBox = sender as TextBox;
            Equipment clickedEquipment = listEquipment.SelectedItem as Equipment;
            clickedEquipment.Quantity = int.Parse(clickedTextBox.Text);
            ContextConnector.db.SaveChanges();
            listEquipment.ItemsSource = ContextConnector.db.Equipment.ToList();
        }
        void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Frames.MainFrame.GoBack();
        }
    }
}