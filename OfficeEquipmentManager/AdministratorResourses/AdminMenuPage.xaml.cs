
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
using OfficeEquipmentManager.LocalDB;

namespace OfficeEquipmentManager.AdministratorResourses
{
	/// <summary>
	/// Interaction logic for AdminMenuPage.xaml
	/// </summary>
	public partial class AdminMenuPage : Page
	{
		public AdminMenuPage(User thisAdmin)
		{
			InitializeComponent();
			
			textBlockName.Text = "Добро пожаловать, " + thisAdmin.FullName + "!";
		}
		
		void ButtonWatchEquipmentList_Click(object sender, RoutedEventArgs e)
		{
			Frames.mainFrame.Navigate(new EquipmentListManagmentPage());
		}
		void ButtonAddEquipment_Click(object sender, RoutedEventArgs e)
		{
			Frames.mainFrame.Navigate(new AddEquipmentPage());
		}
		void ButtonEditEquipment_Click(object sender, RoutedEventArgs e)
		{
			new StatusesEditWindow().ShowDialog();
		}
		void ButtonAddCategory_Click(object sender, RoutedEventArgs e)
		{
			new ManageCategoryWindow().ShowDialog();
		}
		void ButtonWatchDiagrams_Click(object sender, RoutedEventArgs e)
		{
			Frames.mainFrame.Navigate(new OfficeEquipmentManager.MainResourses.DiagramsPage());
		}
		void ButtonAddFromTxt_Click(object sender, RoutedEventArgs e)
		{
			Frames.mainFrame.Navigate(new MainResourses.AddEquipmentFromTxtPage());
		}
		void ButtonManageColors_Click(object sender, RoutedEventArgs e)
		{
			Frames.mainFrame.Navigate(new MainResourses.ColorManagmentPage());
		}
	}
}