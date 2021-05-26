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
            InitializeComponent();

            Frames.MainFrame = MainFrame;
            MainFrame.Navigate(new OfficeEquipmentManager.View.MainResourses.WelcomePage());
        }
    }
}