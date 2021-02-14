
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
	/// Interaction logic for EqupmentListSplitterPage.xaml
	/// </summary>
	public partial class EqupmentListSplitterPage : Page
	{
		public EqupmentListSplitterPage()
		{
			InitializeComponent();
			
			listBoxEquipment.ItemsSource = ContextConnector.db.Equipment.ToList();
		}
		void ButtonEquipmentPlus_Click(object sender, RoutedEventArgs e)
		{
			int id = Convert.ToInt32((sender as Button).Tag);
			Equipment clickedEquipment = ContextConnector.db.Equipment.First(x => x.Id == id);
			clickedEquipment.Quantity += 1;
			ContextConnector.db.SaveChanges();
			listBoxEquipment.ItemsSource = ContextConnector.db.Equipment.ToList();
		}
		void ButtonEquipmentMinus_Click(object sender, RoutedEventArgs e)
		{
			int id = Convert.ToInt32((sender as Button).Tag);
			Equipment clickedEquipment = ContextConnector.db.Equipment.First(x => x.Id == id);
			clickedEquipment.Quantity -= 1;
			ContextConnector.db.SaveChanges();
			listBoxEquipment.ItemsSource = ContextConnector.db.Equipment.ToList();
		}
		void TextBoxQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = "1234567890".IndexOf(e.Text) < 0;
		}
		void TextBoxQuantity_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox clickedTextBox = sender as TextBox;
			int id = Convert.ToInt32(clickedTextBox.Tag);
			Equipment clickedEquipment = ContextConnector.db.Equipment.First(x => x.Id == id);
			clickedEquipment.Quantity = int.Parse(clickedTextBox.Text);
			ContextConnector.db.SaveChanges();
			listBoxEquipment.ItemsSource = ContextConnector.db.Equipment.ToList();
		}
		void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			Frames.mainFrame.GoBack();
		}
		void ListBoxEquipment_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			stackEquipInfo.DataContext = listBoxEquipment.SelectedItem;
		}
	}
}