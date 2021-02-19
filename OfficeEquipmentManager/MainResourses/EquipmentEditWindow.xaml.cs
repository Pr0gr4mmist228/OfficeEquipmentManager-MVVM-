using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using OfficeEquipmentManager.LocalDB;
using OfficeEquipmentManager.MainResourses;

namespace OfficeEquipmentManager.MainResourses
{
    /// <summary>
    /// Логика взаимодействия для EquipmentEditWindow.xaml
    /// </summary>
    public partial class EquipmentEditWindow : Window
    {
        public EquipmentEditWindow(Equipment currentEquipment)
        {
            InitializeComponent();

            DataContext = currentEquipment;

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(currentEquipment.ImagePathString);
            bitmapImage.EndInit();

            image.Source = bitmapImage;
        }
    }
}
