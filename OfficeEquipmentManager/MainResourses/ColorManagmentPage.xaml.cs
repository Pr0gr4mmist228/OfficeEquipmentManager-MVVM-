
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
using System.Windows.Forms;
using System.Configuration;
using OfficeEquipmentManager.Properties;

namespace OfficeEquipmentManager.MainResourses
{
	/// <summary>
	/// Interaction logic for ColorManagmentPage.xaml
	/// </summary>
	public partial class ColorManagmentPage : Page
	{
		public ColorManagmentPage()
		{
			InitializeComponent();
			
			ColorDialog dialog = new ColorDialog() {
				AnyColor = true,
				AllowFullOpen = true
			};
			dialog.ShowDialog();
			
			System.Drawing.Color color = dialog.Color;
			
			Settings.Default.BackgroundColor = color;
			Settings.Default.Save();
			Settings.Default.Reload();
			Settings.Default.Upgrade();
		}
		void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			Frames.mainFrame.GoBack();
		}
	}
}