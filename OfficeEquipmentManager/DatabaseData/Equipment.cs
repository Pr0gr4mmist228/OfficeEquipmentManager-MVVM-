namespace OfficeEquipmentManager.DatabaseData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Text;

    [Table("Equipment")]
    public partial class Equipment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Quantity { get; set; }

        public byte[] ImagePath { get; set; }
        
		[NotMapped()]
		public string ImagePathString { get { if (ImagePath != null) { return Encoding.ASCII.GetString(ImagePath); } return null; } set { ImagePathString = value; } }

        public long SerialNumber { get; set; }

        public int StatusId { get; set; }

        [Required]
        [StringLength(100)]
        public string Сharacteristic { get; set; }

        public int CategoryId { get; set; }

        public int? BarcodeId { get; set; }

        public virtual Barcode Barcode { get; set; }

        public virtual EquipmentCategory EquipmentCategory { get; set; }

        public virtual EquipmentStatus EquipmentStatus { get; set; }
    }
}
