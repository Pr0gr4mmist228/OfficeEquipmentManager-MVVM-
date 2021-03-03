namespace OfficeEquipmentManager.LocalDB
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Booker")]
    public partial class Booker
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(12)]
        public string Phone { get; set; }

        public virtual User User { get; set; }
    }
}
