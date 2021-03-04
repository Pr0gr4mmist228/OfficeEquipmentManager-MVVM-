using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using OfficeEquipmentManager.LocalDB;

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
