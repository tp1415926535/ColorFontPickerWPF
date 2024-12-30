using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ColorFontPickerWPF
{
    /// <summary>
    /// ColorDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ColorDialog : Window
    {
        public Color SelectedColor;
        public ColorDialog()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            colorPickerControl.SelectedColor = SelectedColor;
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = colorPickerControl.SelectedColor;
            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }


    }
}
