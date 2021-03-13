using System.Windows;
using System.Windows.Controls;

namespace OfficeEquipmentManager
{
    /// <summary>
    /// Логика взаимодействия для GlowFrame.xaml
    /// </summary>
    public partial class GlowFrame : UserControl
    {
        public GlowFrame()
        {
            InitializeComponent();
        }
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(GlowFrame));
    }
}
