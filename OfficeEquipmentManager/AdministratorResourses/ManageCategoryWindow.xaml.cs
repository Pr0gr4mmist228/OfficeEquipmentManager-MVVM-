
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

            listBoxCategories.ItemsSource = ContextConnector.db.EquipmentCategory.ToList();
        }

        void ButtonDeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            var messageBox = MessageBox.Show("Вы действительно хотите удалить эту категорию?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBox == MessageBoxResult.Yes)
            {
                int id = Convert.ToInt32((sender as Button).Tag);
                EquipmentCategory editingEquipment = ContextConnector.db.EquipmentCategory.First(x => x.Id == id);
                ContextConnector.db.EquipmentCategory.Remove(editingEquipment);
                ContextConnector.db.SaveChanges();
                MessageBox.Show("Категория успешно удалена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                listBoxCategories.ItemsSource = ContextConnector.db.EquipmentCategory.ToList();
            }
        }
        void ButtonSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            ContextConnector.db.SaveChanges();
            MessageBox.Show("Изменения успешно сохранены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void buttonAddCategory_Click(object sender, RoutedEventArgs e)
        {
            new AddCategoryWindow().ShowDialog();
            listBoxCategories.ItemsSource = ContextConnector.db.EquipmentCategory.ToList();
        }
    }
}