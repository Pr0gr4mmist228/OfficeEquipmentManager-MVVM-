
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using OfficeEquipmentManager.DatabaseData;

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