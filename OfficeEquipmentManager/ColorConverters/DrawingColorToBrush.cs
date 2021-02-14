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
using OfficeEquipmentManager.Properties;
using OfficeEquipmentManager.DatabaseData;
using System.Configuration;
using System.Globalization;

namespace OfficeEquipmentManager.ColorConverters
{
	/// <summary>
	/// Description of DrawingColorToBrush.
	/// </summary>
	public class DrawingColorToBrush : IValueConverter
	{

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
			System.Drawing.Color drawingColor = (System.Drawing.Color)value;
			int r = drawingColor.R;
            int a = drawingColor.A;
            int g = drawingColor.G;
            int b = drawingColor.B;

            Brush brush = new SolidColorBrush(Color.FromArgb((byte)a, (byte)r, (byte)g, (byte)b));
            
			return brush;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
			return DependencyProperty.UnsetValue;
	}
	}
}
