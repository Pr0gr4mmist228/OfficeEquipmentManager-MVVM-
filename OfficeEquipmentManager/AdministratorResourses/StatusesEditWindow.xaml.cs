
using Microsoft.VisualBasic;
using OfficeEquipmentManager.LocalDB;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace OfficeEquipmentManager.AdministratorResourses
{
    /// <summary>
    /// Interaction logic for StatusesEditWindow.xaml
    /// </summary>
    public partial class StatusesEditWindow : Window
    {
        public StatusesEditWindow()
        {
            InitializeComponent();

            listBoxStatuses.ItemsSource = ContextConnector.db.EquipmentStatus.ToList();
        }
        void ButtonDeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            var messageBox = MessageBox.Show("Вы действительно хотите удалить этот статус?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBox == MessageBoxResult.Yes)
            {
                int id = Convert.ToInt32((sender as Button).Tag);
                EquipmentStatus editingStatus = ContextConnector.db.EquipmentStatus.First(x => x.Id == id);
                ContextConnector.db.EquipmentStatus.Remove(editingStatus);
                ContextConnector.db.SaveChanges();
                MessageBox.Show("Категория успешно удалена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                listBoxStatuses.ItemsSource = ContextConnector.db.EquipmentStatus.ToList();
            }
        }
        void ButtonSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            ContextConnector.db.SaveChanges();
            MessageBox.Show("Изменения успешно сохранены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        void ButtonAddStatus_Click(object sender, RoutedEventArgs e)
        {
            string newStatusName = Interaction.InputBox("Введите статус");

            if (!String.IsNullOrEmpty(newStatusName))
            {
                EquipmentStatus newStatus = new EquipmentStatus
                {
                    Status = newStatusName
                };
                ContextConnector.db.EquipmentStatus.Add(newStatus);
                ContextConnector.db.SaveChanges();
                MessageBox.Show("Статус успешно добавлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                listBoxStatuses.ItemsSource = ContextConnector.db.EquipmentStatus.ToList();
            }
        }
    }
}