namespace OfficeEquipmentManager.LocalDB
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public partial class EquipmentStatus : INotifyPropertyChanged
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EquipmentStatus()
        {
            Equipment = new HashSet<Equipment>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get { return status; } set { status = value;OnPropertyChanged("Status"); } }
        private string status;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Equipment> Equipment { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public async void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
                await ContextConnector.db.SaveChangesAsync();
            }
        }
    }
}
