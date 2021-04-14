
using OfficeEquipmentManager.LocalDB;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        }
        void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Frames.MainFrame.GoBack();
        }
    }
}