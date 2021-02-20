
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
		List<Button> buttonsList;

		private int cho;

		private int wrapPanelItemsCount { get { return wrapPanelButtons.Children.Count; } set { wrapPanelItemsCount = value; } }
		private int selectedButtonIndex { get { return cho; } set { if (value < wrapPanelButtons.Children.Count && value >= 0) cho = value; else cho = wrapPanelItemsCount-1; } }

		public AdminMenuPage(User thisAdmin)
		{
			InitializeComponent();

			selectedButtonIndex = 0;
			cho = 0;

			textBlockName.Text = "Добро пожаловать, " + thisAdmin.FullName + "!";

			buttonsList = new List<Button>();
			for (int i = 0; i < wrapPanelItemsCount; i++)
			{
				buttonsList.Add((Button)wrapPanelButtons.Children[i]);
			}

            foreach (var button in buttonsList)
            {
				button.IsDefault = false;
            }

			buttonsList[1].Focus();
		}

        private void AdminMenuPageButtonGotFocus(Button sender)
        {
			for (int i = 0; i < buttonsList.Count; i++)
			{
				if (i != selectedButtonIndex)
					buttonsList[i].Foreground = Brushes.White;
			}
			Button button = sender;
			button.Foreground = Brushes.Aqua;
			button.Focus();
		}

		private void wrapPanelButtons_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Right)
			{
				selectedButtonIndex++;
				AdminMenuPageButtonGotFocus(buttonsList[selectedButtonIndex]);
			}
			else if (e.Key == Key.Left)
			{
				selectedButtonIndex--;
				AdminMenuPageButtonGotFocus(buttonsList[selectedButtonIndex]);
			}
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
			Frames.mainFrame.Navigate(new MainResourses.DiagramsPage());
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