using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OfficeEquipmentManager.LocalDB;

namespace OfficeEquipmentManager.AdministratorResourses
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        TextBlock te = new TextBlock { Text = "asdsda" };
      

        public Page1()
        {
            InitializeComponent();

            listbox.ItemsSource = ContextConnector.db.Equipment.ToList();

           // DataContext = new aSddd();
        }
    }

    public  class aSddd
    {
        int asss;
        public int GetAds { get { return  asss; } }

        public List<Line> Count
        {
            get
            {
                List<Barcode> barcodes = ContextConnector.db.Barcode.ToList();
                List<Line> lines = new List<Line>();

                string barcode = barcodes[1].BarcodeValue.ToString();
                int[] serialNumbers = barcode.Select(a => int.Parse(a.ToString())).ToArray();

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
                    lines.Add(barCodeLine);
                }
                return lines;
            }
        }
    }
}
