using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace ColorFontPickerWPF
{
    class FontHelper
    {
        /// <summary>
        /// Get the font infomation from the specified control
        /// 从指定控件获取字体信息
        /// </summary>
        /// <param name="control"></param>
        public static Font GetFont<T>(T control) where T : DependencyObject
        {
            var font = new Font();

            //control.GetType().GetProperty(nameof(Control.FontFamily)).GetValue(control)
            font.FontFamily = (FontFamily)control.GetValue(Control.FontFamilyProperty) ?? Fonts.SystemFontFamilies.First();
            font.FontSize = (double)(control.GetValue(Control.FontSizeProperty) ?? 12.0);
            font.FontStyle = (FontStyle)(control.GetValue(Control.FontStyleProperty) ?? FontStyles.Normal);
            font.FontStretch = (FontStretch)(control.GetValue(Control.FontStretchProperty) ?? FontStretches.Normal);
            font.FontWeight = (FontWeight)(control.GetValue(Control.FontWeightProperty) ?? FontWeights.Normal);

            var decorations = control.GetValue(TextBlock.TextDecorationsProperty) as TextDecorationCollection;
            if (decorations != null && decorations.Count > 0)
            {
                foreach (var decoration in decorations)
                {
                    switch (decoration.Location)
                    {
                        case TextDecorationLocation.OverLine:
                            font.TextDecorationType = TextDecorationType.OverLine;
                            break;
                        case TextDecorationLocation.Strikethrough:
                            font.TextDecorationType = TextDecorationType.Strikethrough;
                            break;
                        case TextDecorationLocation.Baseline:
                            font.TextDecorationType = TextDecorationType.Baseline;
                            break;
                        case TextDecorationLocation.Underline:
                            font.TextDecorationType = TextDecorationType.Underline;
                            break;
                    }
                    if (font.TextDecorationType != TextDecorationType.None) break;
                }
            }

            return font;
        }


        /// <summary>
        /// Sets the font to the specified control
        /// 将字体设置到指定控件的字体
        /// </summary>
        /// <param name="control"></param>        
        /// <param name="font"></param>
        public static void SetFont<T>(T control, Font font) where T : DependencyObject
        {
            var type = control.GetType();
            var fontFamilyProperty = type.GetProperty(nameof(Control.FontFamily));
            var fontSizeProperty = type.GetProperty(nameof(Control.FontSize));
            var fontStyleProperty = type.GetProperty(nameof(Control.FontStyle));
            var fontStretchProperty = type.GetProperty(nameof(Control.FontStretch));
            var fontWeightProperty = type.GetProperty(nameof(Control.FontWeight));
            var textDecorationsProperty = type.GetProperty(nameof(TextBlock.TextDecorations));

            if (fontFamilyProperty != null)
                fontFamilyProperty.SetValue(control, font.FontFamily);
            if (fontSizeProperty != null && font.FontSize > 0)
                fontSizeProperty.SetValue(control, font.FontSize);
            if (fontStyleProperty != null)
                fontStyleProperty.SetValue(control, font.FontStyle);
            if (fontStretchProperty != null)
                fontStretchProperty.SetValue(control, font.FontStretch);
            if (fontWeightProperty != null)
                fontWeightProperty.SetValue(control, font.FontWeight);
            if (textDecorationsProperty != null && font.TextDecorationType != TextDecorationType.None)
            {
                TextDecorationCollection collect = null;
                switch (font.TextDecorationType)
                {
                    case TextDecorationType.OverLine:
                        collect = TextDecorations.OverLine;
                        break;
                    case TextDecorationType.Strikethrough:
                        collect = TextDecorations.Strikethrough;
                        break;
                    case TextDecorationType.Baseline:
                        collect = TextDecorations.Baseline;
                        break;
                    case TextDecorationType.Underline:
                        collect = TextDecorations.Underline;
                        break;
                }
                textDecorationsProperty.SetValue(control, collect);
            }
        }
    }
}
