
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using OfficeEquipmentManager.DatabaseData;
using System.Linq;

namespace OfficeEquipmentManager.AdministratorResourses
{
	/// <summary>
	/// Interaction logic for AddCategoryWindow.xaml
	/// </summary>
	public partial class AddCategoryWindow : Window
	{
		public AddCategoryWindow()
		{
			InitializeComponent();
		}
		void ButtonAddCategory_Click(object sender, RoutedEventArgs e)
		{
			List<EquipmentCategory> categories = ContextConnector.db.EquipmentCategory.ToList();
			if(categories.All(x => x.Name != textBoxCatrogyName.Text)){
				EquipmentCategory category = new EquipmentCategory{
					Name = textBoxCatrogyName.Text
				};
				ContextConnector.db.EquipmentCategory.Add(category);
				ContextConnector.db.SaveChanges();
				MessageBox.Show("Категория успешно добавлена","Успех",MessageBoxButton.OK,MessageBoxImage.Information);
			} else
				MessageBox.Show("Категория уже существует","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
		}
	}
}