namespace OfficeEquipmentManager.LocalDB
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shapes;

    [Table("Equipment")]
    public partial class Equipment : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string name;
        [Required]
        [StringLength(50)]
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }

        public int Quantity { get; set; }

        public byte[] ImagePath { get; set; }

        [NotMapped()]
        public string ImagePathString { get { if (ImagePath != null) return Encoding.ASCII.GetString(ImagePath); return null; } set { ImagePathString = value; } }

        public long SerialNumber { get; set; }

        public int StatusId { get; set; }

        private List<Line> linez = new List<Line>();

        [NotMapped()]
        public List<Line> Lines
        {
            get
            {
                long assad = Barcode.BarcodeValue;

                int[] serialNumbers = assad.ToString().Select(a => int.Parse(a.ToString())).ToArray();
                linez.Clear();

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
        }


        [StringLength(100)]
        public string Сharacteristic { get; set; }

        public int? CategoryId { get; set; }

        public int BarcodeId { get; set; }

        public virtual Barcode Barcode { get; set; }

        public virtual EquipmentCategory EquipmentCategory { get; set; }

        public virtual EquipmentStatus EquipmentStatus { get; set; }

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
