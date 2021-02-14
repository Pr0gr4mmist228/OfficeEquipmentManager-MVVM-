
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using OfficeEquipmentManager.Properties;
using OfficeEquipmentManager.DatabaseData;
using System.Configuration;

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
			Frames.mainFrame.Navigate(new RegistraionPage());
		}

		void ButtonAuth_Click(object sender, RoutedEventArgs e)
		{
			if(!String.IsNullOrEmpty(textBoxLogin.Text) && !String.IsNullOrEmpty(passwordBox.Password)){
			User user = ContextConnector.db.User.FirstOrDefault(x => x.Login == textBoxLogin.Text && x.Password == passwordBox.Password);
				if(user != null){
					switch(user.RoleId){
						case 1:
							Frames.mainFrame.Navigate(new AdministratorResourses.AdminMenuPage(user));
							break;
							
						case 2:
							Frames.mainFrame.Navigate(new BookerResourses.BookerMenuPage(user));
							break;
					}
				} else
					MessageBox.Show("Неверно введен пароль или логин","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
			}
			else MessageBox.Show("Введите логин и пароль","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
		}
	}
}