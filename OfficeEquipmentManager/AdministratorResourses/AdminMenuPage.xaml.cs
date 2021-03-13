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
                SetFocusedColor(buttonsList[i], false);
            }
            Button button = sender;
            button.Focus();

            SetFocusedColor(button, true);
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

        public void SetFocusedColor(DependencyObject depObj, bool setAquaColor)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                if (child is ContentControl)
                {
                    var asds = (ContentControl)child;
                    if (setAquaColor)
                        asds.Foreground = Brushes.Aqua;
                    else asds.Foreground = Brushes.Red;
                }
                if (child as Button == null) SetFocusedColor(child, setAquaColor);
            }
        }
    }
}