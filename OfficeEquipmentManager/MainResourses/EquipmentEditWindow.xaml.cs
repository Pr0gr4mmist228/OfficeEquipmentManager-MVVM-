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
        Equipment currentEquipment;
        public EquipmentEditWindow(Equipment currentEquipment)
        {
            InitializeComponent();

            this.currentEquipment = currentEquipment;

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

            string barcode = currentEquipment.Barcode.Barcode1.ToString();

            int[] serialNumbers = barcode.Select(a => int.Parse(a.ToString())).ToArray();

            BarcodeActions.BarcodeGenerator.Generate(serialNumbers, barCodePanel, stackNumbers);
        }

        List<Ellipse> ellipses = new List<Ellipse>();

        Grid grid;
        private void GetStatusVisuals()
        {
            statusPanel.Children.Clear();
            for (int i = 0; i < currentEquipment.EquipmentStatus.Id; i++)
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
                block.Margin = new Thickness(-30, 0, 0, 0);

                grid.Children.Add(ellipse);
                grid.Children.Add(block);
                statusPanel.Children.Add(grid);

                ellipses.Add(ellipse);
            }
            ellipses.Last().Fill = Brushes.Red;
        }

            void ConnectTheEllipses()
            {
                line.Visibility = Visibility.Visible;
            }

        private void changeEquipmentStatus_MouseEnter(object sender, MouseEventArgs e)
        {
            listBoxStatuses.ItemsSource = ContextConnector.db.EquipmentStatus.ToList();
            popUp.IsOpen = true;
        }

        private void listBoxStatuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentEquipment.EquipmentStatus = listBoxStatuses.SelectedItem as EquipmentStatus;
            ContextConnector.db.SaveChanges();
            MessageBox.Show("Статус успешно изменен","Успех",MessageBoxButton.OK,MessageBoxImage.Information);
            GetStatusVisuals();
            popUp.IsOpen = false;
        }

        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            popUp.IsOpen = false;
        }
    }
}
 
