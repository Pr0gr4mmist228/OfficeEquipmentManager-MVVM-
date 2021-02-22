
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using OfficeEquipmentManager.MainResourses;

namespace OfficeEquipmentManager
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public string iconPath { get { return Directory.GetCurrentDirectory() + @"/icon.ico"; } }

		public Window1()
		{
			DataContext = this;
			InitializeComponent();

			LocalDB.ContextConnector.db = new LocalDB.ModelContext();
			Frames.mainFrame = mainFrame;
			mainFrame.Navigate(new AuthorizationPage());
		}
	}
}