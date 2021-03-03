using OfficeEquipmentManager.Properties;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace OfficeEquipmentManager.MainResourses
{
    /// <summary>
    /// Interaction logic for ColorManagmentPage.xaml
    /// </summary>
    public partial class ColorManagmentPage : Page
    {
        public ColorManagmentPage()
        {
            InitializeComponent();

            ColorDialog dialog = new ColorDialog()
            {
                AnyColor = true,
                AllowFullOpen = true
            };
            dialog.ShowDialog();

            System.Drawing.Color color = dialog.Color;

            Settings.Default.BackgroundColor = color;
            Settings.Default.Save();
            Settings.Default.Reload();
            Settings.Default.Upgrade();
        }
        void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Frames.mainFrame.GoBack();
        }
    }
}