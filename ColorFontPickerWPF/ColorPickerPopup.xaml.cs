using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
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
    /// ColorPickerPopup.xaml 的交互逻辑
    /// </summary>
    public partial class ColorPickerPopup : UserControl
    {
        public ColorTextFormat ColorText
        {
            get { return (ColorTextFormat)GetValue(ColorTextProperty); }
            set { SetValue(ColorTextProperty, value); }
        }
        public static readonly DependencyProperty ColorTextProperty = DependencyProperty.Register("ColorText", typeof(ColorTextFormat), typeof(ColorPickerPopup), new PropertyMetadata(null));

        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }
        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ColorPickerPopup), new PropertyMetadata(SelectionChangedEvent));

        private static void SelectionChangedEvent(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dep = d as ColorPickerPopup;
            if (dep == null) return;
            dep.SelectedColor = (Color)e.NewValue;
            dep.colorPicker.SelectedColor = dep.SelectedColor;
        }

        public ColorPickerPopup()
        {
            InitializeComponent();
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ControlPopup.IsOpen = !ControlPopup.IsOpen;
        }
    }

    public enum ColorTextFormat
    {
        None,
        RGB,
        HEX,
        HSL,
    }
    public class InvertLightColorConverter : IValueConverter
    {
        static SolidColorBrush whitebrush = new SolidColorBrush(Colors.White);
        static SolidColorBrush blackbrush = new SolidColorBrush(Colors.Black);
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return new SolidColorBrush(Colors.Black);
            else
            {
                SolidColorBrush brush = (SolidColorBrush)value;
                double grayScale = 0.30 * brush.Color.R + 0.59 * brush.Color.G + 0.11 * brush.Color.B;
                if (grayScale > 127)
                    return blackbrush;
                else
                    return whitebrush;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush(Colors.Transparent);
        }
    }
    public class ColorToTextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Length != 2) return string.Empty;
            Color color = (Color)values[0];
            ColorTextFormat colorTextFormat = (ColorTextFormat)values[1];
            switch (colorTextFormat)
            {
                case ColorTextFormat.None:
                    return string.Empty;
                case ColorTextFormat.RGB:
                    return string.Format("RGB({0},{1},{2})", color.R, color.G, color.B);
                case ColorTextFormat.HEX:
                    return new RGB(color).ToHEX().Code;
                case ColorTextFormat.HSL:
                    var hsl = new RGB(color).ToHSL();
                    return string.Format("HSL({0},{1},{2})", hsl.H, hsl.S, hsl.L);
                default:
                    return string.Empty;
            }

        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
