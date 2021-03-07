
using OfficeEquipmentManager.LocalDB;
using System;
using System.Linq;
using System.Windows;
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

           DataContext = new ViewModel.ApplicationViewModel();
        }
        void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Frames.MainFrame.Navigate(new RegistraionPage());
        }

        private void textBoxLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
                passwordBox.Focus();
        }

        private void passwordBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                textBoxLogin.Focus();
        }
    }
}