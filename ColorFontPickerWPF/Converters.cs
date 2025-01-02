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

    /// <summary>
    /// Reverse to black or white based on grayscale
    /// 根据灰度计算反向黑色或白色
    /// </summary>
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


    /// <summary>
    /// Color to corresponding formatted text
    /// 颜色转为对应格式文本
    /// </summary>
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
                    return $"RGB({color.R},{color.G},{color.B})";
                case ColorTextFormat.HEX:
                    return new RGB(color).ToHEX().Code;
                case ColorTextFormat.HSL:
                    var hsl = new RGB(color).ToHSL();
                    return $"HSL({hsl.H},{hsl.S},{hsl.L})";
                default:
                    return string.Empty;
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }



    /// <summary>
    /// typeFace转字体
    /// </summary>
    public class FontTypeFaceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FontTypeFace t = (FontTypeFace)value;
            var typeFace = new FontTypeFace
            {
                Stretch = t.Stretch,
                Style = t.Style,
                Weight = t.Weight,
            };
            return typeFace;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FamilyTypeface t = (FamilyTypeface)value;
            var typeFace = new FontTypeFace
            {
                Stretch = t.Stretch,
                Style = t.Style,
                Weight = t.Weight,
            };
            return typeFace;
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
            return null;
        }
    }


    public class FontTextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Length != 2 || !(values[1] is double)) return string.Empty;
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
