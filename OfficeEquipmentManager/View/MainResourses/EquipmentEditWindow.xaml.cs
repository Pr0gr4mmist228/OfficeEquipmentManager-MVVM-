using OfficeEquipmentManager.LocalDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OfficeEquipmentManager.MainResourses
{
    /// <summary>
    /// Логика взаимодействия для EquipmentEditWindow.xaml
    /// </summary>
    public partial class EquipmentEditWindow : Window
    {
        public EquipmentEditWindow()
        {
            InitializeComponent();
        }

        private void changeEquipmentStatus_MouseEnter(object sender, MouseEventArgs e)
        {
            popUp.IsOpen = true;
        }

        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            popUp.IsOpen = false;
        }
    }
}

