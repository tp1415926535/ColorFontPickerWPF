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
    /// FontDialog.xaml 的交互逻辑
    /// </summary>
    public partial class FontDialog : Window
    {
        public Font SelectedFont;
        public FontDialog()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fontPickerControl.SelectedFont = SelectedFont;
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedFont = fontPickerControl.SelectedFont;
            this.DialogResult = true;
            this.Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        public void GetFont(Control control)
        {
            SelectedFont = FontHelper.GetFont(control);
        }
        public void GetFont(TextBlock textBlock)
        {
            SelectedFont = FontHelper.GetFont(textBlock);
        }
        public void SetFont(Control control)
        {
            FontHelper.SetFont(control, SelectedFont);
        }
        public void SetFont(TextBlock textblock)
        {
            FontHelper.SetFont(textblock, SelectedFont);
        }
    }
}
