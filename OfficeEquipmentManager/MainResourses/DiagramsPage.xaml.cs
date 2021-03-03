
using OfficeEquipmentManager.LocalDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting;

namespace OfficeEquipmentManager.MainResourses
{
    /// <summary>
    /// Interaction logic for DiagramsPage.xaml
    /// </summary>
    public partial class DiagramsPage : Page
    {
        Series currentSeries;
        public DiagramsPage()
        {
            InitializeComponent();

            chartEquipment.ChartAreas.Add(new ChartArea("Main"));

            currentSeries = new Series("Тип оргтехники")
            {
                IsValueShownAsLabel = true
            };
            chartEquipment.Series.Add(currentSeries);

            comboDiagramTypes.ItemsSource = Enum.GetValues(typeof(SeriesChartType));

            currentSeries.ChartType = (SeriesChartType)comboDiagramTypes.SelectedItem;

            List<EquipmentCategory> categories = ContextConnector.db.EquipmentCategory.ToList();
            List<Equipment> equipment = ContextConnector.db.Equipment.ToList();
            int count = 0;
            for (int i = 0; i < categories.Count(); i++)
            {
                count = equipment.Count(x => x.EquipmentCategory.Name == categories[i].Name);

                currentSeries.Points.AddXY(categories[i].Name, count);
            }
        }
        void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Frames.mainFrame.GoBack();
        }
        void ComboDiagramTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentSeries.ChartType = (SeriesChartType)comboDiagramTypes.SelectedItem;
        }
    }
}