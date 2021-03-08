using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using OfficeEquipmentManager.LocalDB;
using System.Windows.Navigation;
using System.Data.Entity;

namespace OfficeEquipmentManager.ViewModel
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Administrator> Administrators { get; set; }
        public ObservableCollection<Barcode> Barcodes { get; set; }
        public ObservableCollection<Equipment> Equipment { get; set; }

        private Equipment selectedEquipment;
        public Equipment SelectedEquipment { get { return selectedEquipment; } set { selectedEquipment = value; OnPropertyChanged("SelectedEquipment"); } }

        public ObservableCollection<EquipmentCategory> EquipmentCategories { get; set; }

        private EquipmentCategory selectedCategory;
        public EquipmentCategory SelectedCategory { get { return selectedCategory; } set { selectedCategory = value; OnPropertyChanged("SelectedCategory"); } }

        public ObservableCollection<EquipmentStatus> EquipmentStatuses { get; set; }
        public ObservableCollection<Role> Roles { get; set; }
        public ObservableCollection<Booker> Bookers { get; set; }

        public ApplicationViewModel()
        {
            ContextConnector.db.User.Load();
            Users = ContextConnector.db.User.Local;

            ContextConnector.db.Administrator.Load();
            Administrators = ContextConnector.db.Administrator.Local;

            ContextConnector.db.Barcode.Load();
            Barcodes = ContextConnector.db.Barcode.Local;

            ContextConnector.db.EquipmentCategory.Load();
            EquipmentCategories = ContextConnector.db.EquipmentCategory.Local;

            ContextConnector.db.Administrator.Load();
            Administrators = ContextConnector.db.Administrator.Local;

            ContextConnector.db.Barcode.Load();
            Barcodes = ContextConnector.db.Barcode.Local;

            ContextConnector.db.Equipment.Load();
            Equipment = ContextConnector.db.Equipment.Local;

            ContextConnector.db.EquipmentStatus.Load();
            EquipmentStatuses = ContextConnector.db.EquipmentStatus.Local;

            ContextConnector.db.Role.Load();
            Roles = ContextConnector.db.Role.Local;

            ContextConnector.db.Booker.Load();
            Bookers = ContextConnector.db.Booker.Local;
        }

        public string Password { get { return password; } set { password = value; OnPropertyChanged("Password"); } }
        private string password;
        private string login;
        public string Login { get { return login; } set { login = value; OnPropertyChanged("Login"); } }

        public RelayCommand AuthCommand
        {
            get
            {
                return new RelayCommand(obj => AuthorizationCheck());
            }
        }

        public static User CurrentUser { get; set; }

        private void AuthorizationCheck()
        {
            User user = Users.FirstOrDefault(x => x.Login == Login && x.Password == Password);
            if (!String.IsNullOrEmpty(Password) && !String.IsNullOrEmpty(Login))
            {
                if (user != null)
                {
                    if (user.RoleId == 1) { Frames.MainFrame.Navigate(new AdministratorResourses.AdminMenuPage(user)); CurrentUser = user; };
                    if (user.RoleId == 2) { Frames.MainFrame.Navigate(new BookerResourses.BookerMenuPage(user)); CurrentUser = user; };
                }
                else MessageBox.Show("Неверно введен пароль или логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Введите логин и пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

       public RelayCommand NavigateCommand { get { return new RelayCommand(obj => Navigate(obj as String)); } }

        private void Navigate(string navigationSourceString)
        {
            Type userType = Type.GetType("OfficeEquipmentManager." + navigationSourceString);
            object navigationSource = Activator.CreateInstance(userType);
            Frames.MainFrame.Navigate(navigationSource);
        }

        public static Window CurrentWindow { get; set; }

        public RelayCommand OpenWindowCommand { get { return new RelayCommand(obj => OpenWindow(obj as String)); } }

        private void OpenWindow(string windowSource)
        {
            Type userType = Type.GetType("OfficeEquipmentManager." + windowSource);
            object navigationSource = Activator.CreateInstance(userType);
            System.Windows.Window window = (navigationSource as System.Windows.Window);
            CurrentWindow = window;
            window.ShowDialog();
        }

        public RelayCommand AddCategoryCommmand { get {
                EquipmentCategory category = null;
                return new RelayCommand(obj =>
                {
                    category = new EquipmentCategory
                    {
                        Name = obj as String
                    };
                    EquipmentCategories.Add(category);
                    MessageBox.Show("Категория успешно добавлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    CurrentWindow.Close();
                });
                }
            }

        public RelayCommand DeleteCategoryCommand { get { return new RelayCommand(obj => ContextConnector.db.EquipmentCategory.Remove(SelectedCategory)); } }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
                ContextConnector.db.SaveChanges();
            }
        }
    }
}
