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
            ConnectTheEllipses();

            if (!String.IsNullOrEmpty(currentEquipment.ImagePathString)) {
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

        List<Ellipse> ellipses = new List<Ellipse>();

        Grid grid;
        private void GetStatusVisuals()
        {
            for (int i = 0; i < ContextConnector.db.EquipmentStatus.Count(); i++)
            {
                List<EquipmentStatus> equipmentStatuses = ContextConnector.db.EquipmentStatus.ToList();
                Ellipse ellipse = new Ellipse
                {
                    Fill = Brushes.Blue,
                    Width = 80,
                    Height = 80,
                    Margin = new Thickness(0, 0, 30, 0)
                };

                TextBlock block = new TextBlock
                {
                    Text = equipmentStatuses[i].Status
                };

                grid = new Grid();

                block.VerticalAlignment = VerticalAlignment.Center;
                block.HorizontalAlignment = HorizontalAlignment.Center;
                block.Margin = new Thickness(-25, 0, 0, 0);

                grid.Children.Add(ellipse);
                grid.Children.Add(block);
                statusPanel.Children.Add(grid);

                ellipses.Add(ellipse);
            }
        }

            void ConnectTheEllipses()
            {   
                for (int i = 0; i < ellipses.Count; i+=2)
                {
                    double left = ellipses[i].RenderedGeometry.Bounds.Left;
                    double right = ellipses[i++].RenderedGeometry.Bounds.Right;
                    Line line = new Line
                    {
                        X1 = left,
                        Y1 = right,
                        StrokeThickness = 2,
                        Stroke = Brushes.Black
                    };
                    grid.Children.Add(line);
                }
            }
        }
    }
