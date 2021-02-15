namespace OfficeEquipmentManager.LocalDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public int RoleId { get; set; }

        public byte[] ImagePath { get; set; }

        public virtual Administrator Administrator { get; set; }

        public virtual Booker Booker { get; set; }

        public virtual Role Role { get; set; }
    }
}
