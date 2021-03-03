using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using OfficeEquipmentManager.LocalDB;
using Microsoft.Win32;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace OfficeEquipmentManager.MainResourses
{
	/// <summary>
	/// Interaction logic for AddEquipmentFromTxtPage.xaml
	/// </summary>
	public partial class AddEquipmentFromTxtPage : Page
	{
		public AddEquipmentFromTxtPage()
		{
			InitializeComponent();
		}
		void ButtonSetFilePath_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog txtDialog = new OpenFileDialog{
				Multiselect = false,
			//	Filter = "Текстовый файл | *csv"
			};
			txtDialog.ShowDialog();

			var excelBook = new Excel.Application();
			excelBook.Workbooks.Open(txtDialog.FileName);

			Excel.Worksheet worksheet = excelBook.Worksheets.Item[1];
			int columns = worksheet.UsedRange.Columns.Count;
			int rows = worksheet.UsedRange.Rows.Count;

			for (int i = 1; i <= rows; i++)
            {
				object[] equipmentValues = new object[7];

				for (int j = 1; j <= columns; j++)
                 {
					Excel.Range range = worksheet.Cells[i, j] as Excel.Range;
					string rad = Convert.ToString(range.Value2);
					equipmentValues[j - 1] = range.Value2;
				}

				Barcode barcode = new Barcode
				{
					BarcodeValue = Convert.ToInt32(equipmentValues[6])
				};

				ContextConnector.db.Barcode.Add(barcode);
				ContextConnector.db.SaveChanges();

				Equipment newEquipment = new Equipment
				{
					Name = equipmentValues[0].ToString(),
					Quantity = Convert.ToInt32(equipmentValues[1]),
					SerialNumber = Convert.ToInt32(equipmentValues[2]),
					StatusId = Convert.ToInt32(equipmentValues[3]),
					Сharacteristic = equipmentValues[4].ToString(),
					CategoryId = Convert.ToInt32(equipmentValues[5]),
					BarcodeId = barcode.Id
				};
				ContextConnector.db.Equipment.Add(newEquipment);
				ContextConnector.db.SaveChanges();

			}

			//if (!String.IsNullOrEmpty(txtDialog.FileName)){
			//using (StreamReader txtReader = new StreamReader(txtDialog.FileName)) {
			//		string[] allText = txtReader.ReadToEnd().Split(';','\\');
					
			//		string[,] allTextArray = new string[allText.Count() /7 ,7];
					
			//		for (int i = 0; i < allTextArray.GetLength(0); i++) {
						
			//			for (int j = 0; j < allTextArray.GetLength(1); j++) {
			//				allTextArray[i,j] = allText[j];
			//			}
			//		}
				
			//		for (int i = 0; i < allText.Length; i++) {
			//			string name = allText[i];
			//			int quantity = int.Parse(allText[++i]);
			//			//byte[] imagePath = allText.Substring(2,allText.IndexOf('|'));
			//			int serialNumber = int.Parse(allText[++i]);
			//			int statusId = int.Parse(allText[++i]);
			//			string characteristic = allText[++i];
			//			int categoryId = int.Parse(allText[++i]);
			//			int barcodeId = int.Parse(allText[++i]);
						
			//			Equipment equipment = new Equipment{
			//				Name = name,
			//				Сharacteristic = characteristic,
			//				Quantity = quantity,
			//				SerialNumber = serialNumber,
			//				StatusId = statusId,
			//				CategoryId = categoryId,
			//				BarcodeId = barcodeId
			//			};
			//			ContextConnector.db.Equipment.Add(equipment);
			//			ContextConnector.db.SaveChanges();
					}
				}
			}