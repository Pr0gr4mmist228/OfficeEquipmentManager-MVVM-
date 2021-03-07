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
        public ObservableCollection<EquipmentStatus> EquipmentStatuses { get; set; }
        public ObservableCollection<Role> Roles { get; set; }
        public ObservableCollection<Booker> Bookers { get; set; }

        public ApplicationViewModel()
        {
            Users = new ObservableCollection<User>(ContextConnector.db.User);
            Administrators = new ObservableCollection<Administrator>(ContextConnector.db.Administrator);
            Barcodes = new ObservableCollection<Barcode>(ContextConnector.db.Barcode);
            Equipment = new ObservableCollection<Equipment>(ContextConnector.db.Equipment);
            EquipmentCategories = new ObservableCollection<EquipmentCategory>(ContextConnector.db.EquipmentCategory);
            EquipmentStatuses = new ObservableCollection<EquipmentStatus>(ContextConnector.db.EquipmentStatus);
            Roles = new ObservableCollection<Role>(ContextConnector.db.Role);
            Bookers = new ObservableCollection<Booker>(ContextConnector.db.Booker);
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

        public User CurrentUser { get { return user; } set { user = value; OnPropertyChanged("CurrentUser"); } }

        User user;
        private void AuthorizationCheck()
        {
            user = Users.FirstOrDefault(x => x.Login == Login && x.Password == Password);
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

        public RelayCommand OpenWindowCommand { get { return new RelayCommand(obj => OpenWindow(obj as String)); } }

        private void OpenWindow(string windowSource)
        {
            Type userType = Type.GetType("OfficeEquipmentManager." + windowSource);
            object navigationSource = Activator.CreateInstance(userType);
            (navigationSource as System.Windows.Window).ShowDialog();
        }

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
