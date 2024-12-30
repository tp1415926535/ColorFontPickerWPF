using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorFontPickerWPF
{
    /// <summary>
    /// FontPickerPopup.xaml 的交互逻辑
    /// </summary>
    public partial class FontPickerPopup : UserControl
    {
        public string FontText
        {
            get { return (string)GetValue(FontTextProperty); }
            set { SetValue(FontTextProperty, value); }
        }
        public static readonly DependencyProperty FontTextProperty = DependencyProperty.Register("FontText", typeof(string), typeof(FontPickerPopup), new PropertyMetadata(null));

        public Font SelectedFont
        {
            get { return (Font)GetValue(SelectedFontProperty); }
            set { SetValue(SelectedFontProperty, value); }
        }
        public static readonly DependencyProperty SelectedFontProperty = DependencyProperty.Register("SelectedFont", typeof(Font), typeof(FontPickerPopup), new PropertyMetadata(SelectionChangedEvent));

        private static void SelectionChangedEvent(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dep = d as FontPickerPopup;
            if (dep == null) return;
            dep.SelectedFont = (Font)e.NewValue;
            dep.fontPicker.SelectedFont = dep.SelectedFont;
        }

        public FontPickerPopup()
        {
            InitializeComponent();
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ControlPopup.IsOpen = !ControlPopup.IsOpen;
            fontPicker.ScrollToSelection();
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
    public class SizeFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return value;
            else
            {
                string str = (string)value;

                return str;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush(Colors.Transparent);
        }
    }
    public class FontTextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Length != 2|| !(values[1] is double)) return string.Empty;
            string fontText = (string)values[0];
            double fontsize = (double)values[1];
            if (string.IsNullOrEmpty(fontText))
                return "FontSize: " + fontsize + "px";
            else
                return fontText.Replace("{fontsize}", fontsize.ToString());
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
