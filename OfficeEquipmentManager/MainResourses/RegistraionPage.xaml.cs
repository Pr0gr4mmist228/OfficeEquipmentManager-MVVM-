
using Microsoft.Win32;
using OfficeEquipmentManager.LocalDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace OfficeEquipmentManager
{
    /// <summary>
    /// Interaction logic for RegistraionPage.xaml
    /// </summary>
    public partial class RegistraionPage : Page
    {
        public RegistraionPage()
        {
            InitializeComponent();
        }
        void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            List<User> loginsList = ContextConnector.db.User.ToList();
            if (loginsList.All(x => x.Login != loginBox.Text))
            {
                byte[] imagePath = Encoding.ASCII.GetBytes(imageLinkBox.Text);
                User newUser = new User
                {
                    FullName = fullnameBox.Text,
                    Login = loginBox.Text,
                    Password = passwordBox.Password,
                    RoleId = 2,
                    ImagePath = imagePath
                };

                ContextConnector.db.User.Add(newUser);
                ContextConnector.db.SaveChanges();

                Booker newBooker = new Booker
                {
                    Id = newUser.Id,
                    Email = emailBox.Text,
                    Phone = phoneBox.Text
                };

                ContextConnector.db.Booker.Add(newBooker);
                ContextConnector.db.SaveChanges();
                Frames.MainFrame.GoBack();
            }
            else
                MessageBox.Show("Такой логин уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        void ButtonSetImageFromIO_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog imageDialog = new OpenFileDialog();
            imageDialog.Filter = "Изображения | *.png;*.jpeg;";
            imageDialog.Multiselect = false;
            imageDialog.ShowDialog();

            if (!String.IsNullOrEmpty(imageDialog.FileName))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(imageDialog.FileName);
                image.EndInit();
                userImage.Source = image;
                imageLinkBox.Text = imageDialog.FileName;
            }
        }
        void ImageLinkBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(imageLinkBox.Text);
            image.EndInit();

            userImage.Source = image;
        }
        void ButtonAuthorization_Click(object sender, RoutedEventArgs e)
        {
            Frames.MainFrame.GoBack();
        }
    }
}