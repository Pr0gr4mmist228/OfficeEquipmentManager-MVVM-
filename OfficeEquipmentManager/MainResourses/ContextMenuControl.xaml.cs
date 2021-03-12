
using OfficeEquipmentManager.LocalDB;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace OfficeEquipmentManager.MainResourses
{
    /// <summary>
    /// Interaction logic for ContextMenuControl.xaml
    /// </summary>
    public partial class ContextMenuControl : ContextMenu
    {
        public ContextMenuControl()
        {
            InitializeComponent();
        }

        void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            ListBox listBox = (ListBox)contextMenu.PlacementTarget;
            ListBoxItem item = (ListBoxItem)listBox.SelectedItem;
            int id = Convert.ToInt32(item.Tag);
            Equipment equip = ContextConnector.db.Equipment.First(x => x.Id == id);
            ContextConnector.db.Equipment.Remove(equip);
            ContextConnector.db.SaveChanges();
        }
    }
}