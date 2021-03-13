
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