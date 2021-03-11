using OfficeEquipmentManager.LocalDB;
using OfficeEquipmentManager.MainResourses;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms.DataVisualization.Charting;

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
            LocalDB.ContextConnector.db = new LocalDB.ModelContext();
            DataContext = new ViewModel.ApplicationViewModel();
            InitializeComponent();

            Frames.MainFrame = MainFrame;
            MainFrame.Navigate(new AuthorizationPage());
        }
    }
}