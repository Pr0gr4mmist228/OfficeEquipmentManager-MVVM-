
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
using OfficeEquipmentManager.DatabaseData;
using Microsoft.Win32;
using System.IO;

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
				Filter = "Текстовый файл | *csv"
			};
			txtDialog.ShowDialog();

			if(!String.IsNullOrEmpty(txtDialog.FileName)){
			using (StreamReader txtReader = new StreamReader(txtDialog.FileName)) {
					string[] allText = txtReader.ReadToEnd().Split(';','\\');
					
					string[,] allTextArray = new string[allText.Count() /7 ,7];
					
					for (int i = 0; i < allTextArray.GetLength(0); i++) {
						
						for (int j = 0; j < allTextArray.GetLength(1); j++) {
							allTextArray[i,j] = allText[j];
						}
					}
				
					for (int i = 0; i < allText.Length; i++) {
						string name = allText[i];
						int quantity = int.Parse(allText[++i]);
						//byte[] imagePath = allText.Substring(2,allText.IndexOf('|'));
						int serialNumber = int.Parse(allText[++i]);
						int statusId = int.Parse(allText[++i]);
						string characteristic = allText[++i];
						int categoryId = int.Parse(allText[++i]);
						int barcodeId = int.Parse(allText[++i]);
						
						Equipment equipment = new Equipment{
							Name = name,
							Сharacteristic = characteristic,
							Quantity = quantity,
							SerialNumber = serialNumber,
							StatusId = statusId,
							CategoryId = categoryId,
							BarcodeId = barcodeId
						};
						ContextConnector.db.Equipment.Add(equipment);
						ContextConnector.db.SaveChanges();
					}
				}
			}
		}
	}
}