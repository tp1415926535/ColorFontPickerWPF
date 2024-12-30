using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorFontPickerWPF
{
    class FontHelper
    {
        public static Font GetFont(Control control)
        {
            var font = new Font
            {
                FontFamily = control.FontFamily,
                FontSize = control.FontSize,
                FamilyTypeface = new FamilyTypeface { Style = control.FontStyle, Stretch = control.FontStretch, Weight = control.FontWeight }
            };
            if (control is TextBox)
            {
                TextBox textBox = new TextBox();
                if (textBox.TextDecorations == TextDecorations.OverLine)
                    font.TextDecorationType = TextDecorationType.OverLine;
                else if (textBox.TextDecorations == TextDecorations.Strikethrough)
                    font.TextDecorationType = TextDecorationType.Strikethrough;
                else if (textBox.TextDecorations == TextDecorations.Baseline)
                    font.TextDecorationType = TextDecorationType.Baseline;
                else if (textBox.TextDecorations == TextDecorations.Underline)
                    font.TextDecorationType = TextDecorationType.Underline;
            }
            return font;
        }
        public static Font GetFont(TextBlock textBlock)
        {
            var font = new Font
            {
                FontFamily = textBlock.FontFamily,
                FontSize = textBlock.FontSize,
                FamilyTypeface = new FamilyTypeface { Style = textBlock.FontStyle, Stretch = textBlock.FontStretch, Weight = textBlock.FontWeight }
            };
            if (textBlock.TextDecorations == TextDecorations.OverLine)
                font.TextDecorationType = TextDecorationType.OverLine;
            else if (textBlock.TextDecorations == TextDecorations.Strikethrough)
                font.TextDecorationType = TextDecorationType.Strikethrough;
            else if (textBlock.TextDecorations == TextDecorations.Baseline)
                font.TextDecorationType = TextDecorationType.Baseline;
            else if (textBlock.TextDecorations == TextDecorations.Underline)
                font.TextDecorationType = TextDecorationType.Underline;
            return font;
        }
        public static void SetFont(Control control,Font font)
        {
            control.FontFamily = font.FontFamily;
            control.FontSize = font.FontSize;
            if (font.FamilyTypeface != null)
            {
                control.FontWeight = font.FamilyTypeface.Weight;
                control.FontStyle = font.FamilyTypeface.Style;
                control.FontStretch = font.FamilyTypeface.Stretch;
            }
            if (control is TextBox)
            {
                TextBox textBox = new TextBox();
                switch (font.TextDecorationType)
                {
                    case TextDecorationType.None:
                        textBox.TextDecorations = null;
                        break;
                    case TextDecorationType.OverLine:
                        textBox.TextDecorations = TextDecorations.OverLine;
                        break;
                    case TextDecorationType.Strikethrough:
                        textBox.TextDecorations = TextDecorations.Strikethrough;
                        break;
                    case TextDecorationType.Baseline:
                        textBox.TextDecorations = TextDecorations.Baseline;
                        break;
                    case TextDecorationType.Underline:
                        textBox.TextDecorations = TextDecorations.Underline;
                        break;
                }
            }
        }
        public static void SetFont(TextBlock textblock, Font font)
        {
            textblock.FontFamily = font.FontFamily;
            textblock.FontSize = font.FontSize;
            if (font.FamilyTypeface != null)
            {
                textblock.FontWeight = font.FamilyTypeface.Weight;
                textblock.FontStyle = font.FamilyTypeface.Style;
                textblock.FontStretch = font.FamilyTypeface.Stretch;
            }
            switch (font.TextDecorationType)
            {
                case TextDecorationType.None:
                    textblock.TextDecorations = null;
                    break;
                case TextDecorationType.OverLine:
                    textblock.TextDecorations = TextDecorations.OverLine;
                    break;
                case TextDecorationType.Strikethrough:
                    textblock.TextDecorations = TextDecorations.Strikethrough;
                    break;
                case TextDecorationType.Baseline:
                    textblock.TextDecorations = TextDecorations.Baseline;
                    break;
                case TextDecorationType.Underline:
                    textblock.TextDecorations = TextDecorations.Underline;
                    break;
            }
        }
    }
}
