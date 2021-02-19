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

            GetStatusVisuals();

            if (currentEquipment.ImagePathString != null) {
                try
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.UriSource = new Uri(currentEquipment.ImagePathString);
                    bitmapImage.EndInit();

                    image.Source = bitmapImage;
                }
                catch (System.IO.FileNotFoundException)
                {
                    MessageBox.Show("Файл не найден","Ошибка",MessageBoxButton.OK,MessageBoxImage.Warning);
                }
            }
        }

        private void GetStatusVisuals()
        {
            for (int i = 0; i < ContextConnector.db.EquipmentStatus.Count(); i++)
            {
                Ellipse ellipse = new Ellipse
                {
                    Fill = Brushes.Blue,
                    Width = 80,
                    Height = 80,
                    Margin = new Thickness(0,0,30,0)
                };

                statusPanel.Children.Add(ellipse);
            }
        }
    }
}
