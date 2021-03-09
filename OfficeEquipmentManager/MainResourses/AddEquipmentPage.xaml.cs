﻿
using Microsoft.Win32;
using OfficeEquipmentManager.LocalDB;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OfficeEquipmentManager
{
    /// <summary>
    /// Interaction logic for AddEquipmentPage.xaml
    /// </summary>
    public partial class AddEquipmentPage : Page
    {
        int[] serialNumbers;

        public AddEquipmentPage()
        {
            InitializeComponent();

            BarCodeGenerate();
            //TODO : Add random barcode

            DataContext = new ViewModel.ApplicationViewModel();
        }

        void BarCodeGenerate()
        {
            serialNumbers = new int[13];
            Random rand = new Random();
            for (int i = 0; i < serialNumbers.Length; i++)
            {
                serialNumbers[i] = rand.Next(2, 10);
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
                TextBlock number = new TextBlock
                {
                    Text = serialNumbers[i].ToString(),
                    VerticalAlignment = VerticalAlignment.Bottom
                };
                barcodePanel.Children.Add(barCodeLine);
                stackNumbers.Children.Add(number);
            }
        }

        void EquipmentQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) < 0;
        }
        void ButtonSetImageFromIO_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog imageDialog = new OpenFileDialog();
            imageDialog.Filter = "Изображения | *.png;*.jpeg;";
            imageDialog.Multiselect = false;
            imageDialog.ShowDialog();

            if (!String.IsNullOrEmpty(imageDialog.FileName))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(imageDialog.FileName);
                image.EndInit();
                //equipmentImage.Source = image;
            //    imageLinkBox.Text = imageDialog.FileName;
            }
        }
        void ImageLinkBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
           // image.UriSource = new Uri(imageLinkBox.Text);
            image.EndInit();

           // equipmentImage.Source = image;
        }
        void ButtonAddEquipment_Click(object sender, RoutedEventArgs e)
        {
            long barcode;
            string barcodeString = "";
            for (int i = 0; i < serialNumbers.Length; i++)
            {
                barcodeString += serialNumbers[i];
            }
            barcode = long.Parse(barcodeString);

            Barcode newBarcode = new Barcode
            {
                BarcodeValue = barcode
            };
            ContextConnector.db.Barcode.Add(newBarcode);
            ContextConnector.db.SaveChanges();

            //byte[] imageBytes = Encoding.GetEncoding(1251).GetBytes(imageLinkBox.Text);
            Equipment newEquipment = new Equipment
            {
                Name = equimpentName.Text,
               // Quantity = int.Parse(equipmentQuantity.Text),
                //ImagePath = imageBytes,
            //    SerialNumber = int.Parse(equipmentSerialNumber.Text),
                StatusId = 1,
             //   Сharacteristic = new TextRange(equipmentCharacteristic.Document.ContentStart, equipmentCharacteristic.Document.ContentEnd).Text,
           //     CategoryId = (equipmentCategory.SelectedValue as EquipmentCategory).Id,
                BarcodeId = newBarcode.Id
            };
            ContextConnector.db.Equipment.Add(newEquipment);
            ContextConnector.db.SaveChanges();
            MessageBox.Show("Оргтехника успешно добавлена!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
            Frames.MainFrame.GoBack();
        }
        void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Frames.MainFrame.GoBack();
        }
        // dobavit remove
        void EquipmentSerialNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        //			int serialNumber = int.Parse(equipmentSerialNumber.Text.Last().ToString());
        //			
        //				Line BarcodeLine = new Line{
        //					X2 = 0,
        //					Y2 = 100,
        //					Stroke = Brushes.Black,
        //					StrokeThickness = serialNumber / 2,
        //					HorizontalAlignment= HorizontalAlignment.Stretch,
        //					VerticalAlignment = VerticalAlignment.Stretch,
        //					Margin= new Thickness(0,0,5,0)
        //						};
        //				BarcodePanel.Children.Add(BarcodeLine);
        //			}
    }
}