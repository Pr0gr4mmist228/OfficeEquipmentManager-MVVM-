using OfficeEquipmentManager.LocalDB;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
            if (categories.All(x => x.Name != textBoxCatrogyName.Text))
            {
                EquipmentCategory category = new EquipmentCategory
                {
                    Name = textBoxCatrogyName.Text
                };
                ContextConnector.db.EquipmentCategory.Add(category);
                ContextConnector.db.SaveChanges();
                MessageBox.Show("Категория успешно добавлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            else
                MessageBox.Show("Категория уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}