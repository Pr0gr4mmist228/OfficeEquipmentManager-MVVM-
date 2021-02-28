using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OfficeEquipmentManager.BarcodeActions
{
    class BarcodeGenerator
    {
        public static void Generate(int[] serialNumbers, StackPanel barCodePanel, StackPanel stackNumbers)
        {
			Random rand = new Random();
			if (serialNumbers == null)
			{
				serialNumbers = new int[13];
			}

			for (int i = 0; i < serialNumbers.Length; i++)
			{
				if(serialNumbers[i] == 0)
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

				barCodePanel.Children.Add(barCodeLine);
				stackNumbers.Children.Add(number);
			}
		}
    }
}
