namespace OfficeEquipmentManager.LocalDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shapes;

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
        public string ImagePathString { get { if (ImagePath != null)return Encoding.ASCII.GetString(ImagePath); return null; } set { ImagePathString = value; } }

        public long SerialNumber { get; set; }

        List<Line> linez = new List<Line>();
        [NotMapped()]
        public List<Line> lines
        {
            get
            {
                long assad = Barcode.BarcodeValue;

                int[] serialNumbers = assad.ToString().Select(a => int.Parse(a.ToString())).ToArray();

                for (int i = 0; i < serialNumbers.Length; i++)
                {
                    Line barCodeLine = new Line
                    {
                        X2 = 0,
                        Y2 = 100,
                        Stroke = Brushes.Black,
                        StrokeThickness = serialNumbers[i] / 2,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        Margin = new Thickness(0, 0, 5, 0)
                    };
                    linez.Add(barCodeLine);

                }
                return linez;
            }
            set { linez = value; }
        }

        public int StatusId { get; set; }

        [StringLength(100)]
        public string Сharacteristic { get; set; }

        public int CategoryId { get; set; }

        public int BarcodeId { get; set; }

        public virtual Barcode Barcode { get; set; }

        public virtual EquipmentCategory EquipmentCategory { get; set; }

        public virtual EquipmentStatus EquipmentStatus { get; set; }
    }
}
