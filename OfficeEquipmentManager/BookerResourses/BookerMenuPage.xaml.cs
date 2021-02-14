
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

namespace OfficeEquipmentManager.BookerResourses
{
	/// <summary>
	/// Interaction logic for BookerMenuPage.xaml
	/// </summary>
	public partial class BookerMenuPage : Page
	{
		public BookerMenuPage(User thisBooker)
		{
			InitializeComponent();
			
			textBlockName.Text = "Добро пожаловать, " + thisBooker.FullName + "!";
		}
		void ButtonWatchEquipmentList_Click(object sender, RoutedEventArgs e)
		{
			Frames.mainFrame.Navigate(new EquipmentListManagmentPage());
		}
		void ButtonAddEquipment_Click(object sender, RoutedEventArgs e)
		{
			Frames.mainFrame.Navigate(new AddEquipmentPage());
		}
	}
}