using System.Windows.Controls;
using System.Windows.Input;

namespace OfficeEquipmentManager
{
    /// <summary>
    /// Interaction logic for AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }
        void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Frames.MainFrame.Navigate(new RegistraionPage());
        }
    }
}