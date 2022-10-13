using ColorFontPickerWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoNETFramework
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            InitializeComponent();
        }

        private void ColorDialogButton_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.SelectedColor = ((SolidColorBrush)PreviewLabel.Background).Color;
            if (colorDialog.ShowDialog() == true)
                PreviewLabel.Background = new SolidColorBrush(colorDialog.SelectedColor);
        }

        private void FontDialogButton_Click(object sender, RoutedEventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.GetFont(PreviewTextBlock);
            if (fontDialog.ShowDialog() == true)
                fontDialog.SetFont(PreviewTextBlock);
        }
    }
}
