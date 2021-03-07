using OfficeEquipmentManager.LocalDB;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace OfficeEquipmentManager.AdministratorResourses
{
    /// <summary>
    /// Interaction logic for AdminMenuPage.xaml
    /// </summary>
    public partial class AdminMenuPage : Page
    {
        List<Button> buttonsList;

        private int cho;

        private int WrapPanelItemsCount { get { return wrapPanelButtons.Children.Count; } set { WrapPanelItemsCount = value; } }
        private int SelectedButtonIndex { get { return cho; } set { if (value < wrapPanelButtons.Children.Count && value >= 0) cho = value; else cho = WrapPanelItemsCount - 1; } }

        public AdminMenuPage(User thisAdmin)
        {
            InitializeComponent();

            DataContext = new ViewModel.ApplicationViewModel();

            SelectedButtonIndex = 0;
            cho = 0;

            buttonsList = new List<Button>();
            for (int i = 0; i < WrapPanelItemsCount; i++)
            {
                buttonsList.Add((Button)wrapPanelButtons.Children[i]);
            }
            buttonsList[1].Focus();
        }

        private void AdminMenuPageButtonGotFocus(Button sender)
        {
            for (int i = 0; i < buttonsList.Count; i++)
            {
                buttonsList[i].SetAllShitIsStandartColor<Button>();
            }
            Button button = sender;
            button.Focus();

            button.GetChildOfType<Button>();
        }

        private void wrapPanelButtons_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                SelectedButtonIndex++;
                AdminMenuPageButtonGotFocus(buttonsList[SelectedButtonIndex]);
            }
            else if (e.Key == Key.Left)
            {
                SelectedButtonIndex--;
                AdminMenuPageButtonGotFocus(buttonsList[SelectedButtonIndex]);
            }
        }

        void ButtonWatchEquipmentList_Click(object sender, RoutedEventArgs e)
        {
            Frames.MainFrame.Navigate(new EquipmentListManagmentPage());
        }
        void ButtonAddEquipment_Click(object sender, RoutedEventArgs e)
        {
            Frames.MainFrame.Navigate(new AddEquipmentPage());
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
            Frames.MainFrame.Navigate(new MainResourses.DiagramsPage());
        }
        void ButtonAddFromTxt_Click(object sender, RoutedEventArgs e)
        {
            Frames.MainFrame.Navigate(new MainResourses.AddEquipmentFromTxtPage());
        }
        void ButtonManageColors_Click(object sender, RoutedEventArgs e)
        {
            Frames.MainFrame.Navigate(new MainResourses.ColorManagmentPage());
        }
    }
    static class govno
    {
        public static T GetChildOfType<T>(this DependencyObject depObj, bool SetAllShit = true)
        where T : DependencyObject
        {
            if (depObj == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                if (child is ContentControl)
                {
                    var asds = (ContentControl)child;
                    if (SetAllShit)
                        asds.Foreground = Brushes.Aqua;
                    else asds.Foreground = Brushes.Red;
                }
                var result = (child as T) ?? GetChildOfType<T>(child);
                if (result != null) return result;
            }
            return null;
        }

        public static T SetAllShitIsStandartColor<T>(this DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                if (child is ContentControl)
                {
                    var asds = (ContentControl)child;
                    asds.Foreground = Brushes.Red;

                }
                var result = (child as T) ?? GetChildOfType<T>(child, false);
                if (result != null) return result;
            }
            return null;
        }
    }
}