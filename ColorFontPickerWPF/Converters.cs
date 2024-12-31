using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ColorFontPickerWPF
{
    /// <summary>
    /// Color to brush
    /// 颜色转为brush
    /// </summary>
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return new SolidColorBrush(Colors.Transparent);
            else
            {
                Color color = (Color)value;
                return new SolidColorBrush(color);
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Colors.Transparent;
            else
            {
                SolidColorBrush solidColorBrush = (SolidColorBrush)value;
                return solidColorBrush.Color;
            }
        }
    }

    /// <summary>
    /// HSL area colour gradient
    /// HSL域颜色渐变
    /// </summary>
    public class HSLRangeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Colors.Transparent;
            else
            {
                Color color = (Color)value;
                HSL hsl = new RGB(color.R, color.G, color.B).ToHSL();
                string param = (string)parameter;
                RGB rgb;
                switch (param)
                {
                    case "Top":
                        rgb = new HSL(hsl.H, hsl.S, 100).ToRGB();
                        break;
                    case "Center":
                        rgb = new HSL(hsl.H, hsl.S, 50).ToRGB();
                        break;
                    case "Bottom":
                        rgb = new HSL(hsl.H, hsl.S, 0).ToRGB();
                        break;
                    default:
                        return Colors.Transparent;
                }
                return rgb.ToColor();
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    /// <summary>
    /// HSL pickup point location binding
    /// HSL拾取点位置绑定
    /// </summary>
    public class HslPickerPosConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var type = parameter as string;
            if (type == "left")
            {
                var width = (double)values[0];
                var hslAreaH = 0;
                int.TryParse(values[1].ToString(), out hslAreaH);
                return hslAreaH / 360.0 * width;
            }
            else if (type == "top")
            {
                var height = (double)values[0];
                var hslAreaS = 0;
                int.TryParse(values[1].ToString(), out hslAreaS);
                return (1 - hslAreaS / 100.0) * height;
            }
            return 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
