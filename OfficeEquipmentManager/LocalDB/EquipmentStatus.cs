namespace OfficeEquipmentManager.LocalDB
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class EquipmentStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EquipmentStatus()
        {
            Equipment = new HashSet<Equipment>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Equipment> Equipment { get; set; }
    }
}
