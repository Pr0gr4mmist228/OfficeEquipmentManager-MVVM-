using System.Windows;

namespace OfficeEquipmentManager
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            // DataContext = new ViewModel.ApplicationViewModel();
            InitializeComponent();

            Frames.MainFrame = MainFrame;
            MainFrame.Navigate(new AuthorizationPage());
        }
    }
}