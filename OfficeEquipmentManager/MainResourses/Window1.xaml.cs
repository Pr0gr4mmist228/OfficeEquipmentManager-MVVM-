using OfficeEquipmentManager.MainResourses;
using System.IO;
using System.Linq;
using System.Windows;

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
            mainFrame.Navigate(new Test());
        }
    }
}