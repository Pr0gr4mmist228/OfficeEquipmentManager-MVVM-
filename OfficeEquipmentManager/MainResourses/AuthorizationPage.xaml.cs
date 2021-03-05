
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

        void ButtonAuth_Click(object sender, RoutedEventArgs e)
        {
            //if (!String.IsNullOrEmpty(textBoxLogin.Text) && !String.IsNullOrEmpty(passwordBox.Password))
            //{
            //    User user = ContextConnector.db.User.FirstOrDefault(x => x.Login == textBoxLogin.Text && x.Password == passwordBox.Password);
            //    if (user != null)
            //    {
            //        switch (user.RoleId)
            //        {
            //            case 1:
            //                Frames.MainFrame.Navigate(new AdministratorResourses.AdminMenuPage(user));
            //                break;

            //            case 2:
            //                Frames.MainFrame.Navigate(new BookerResourses.BookerMenuPage(user));
            //                break;
            //        }
            //    }
            //    else
            //        MessageBox.Show("Неверно введен пароль или логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            //else MessageBox.Show("Введите логин и пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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