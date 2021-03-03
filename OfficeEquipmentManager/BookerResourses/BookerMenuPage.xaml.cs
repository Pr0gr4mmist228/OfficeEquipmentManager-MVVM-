using OfficeEquipmentManager.LocalDB;
using System.Windows;
using System.Windows.Controls;

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