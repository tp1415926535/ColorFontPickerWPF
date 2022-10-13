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
            var beforeFont = new Font
            {
                FontFamily = control.FontFamily,
                FontSize = control.FontSize,
                FamilyTypeface = new FamilyTypeface { Style = control.FontStyle, Stretch = control.FontStretch, Weight = control.FontWeight }
            };
            if (control is TextBox)
            {
                TextBox textBox = new TextBox();
                if (textBox.TextDecorations == TextDecorations.OverLine)
                    beforeFont.TextDecorationType = TextDecorationType.OverLine;
                else if (textBox.TextDecorations == TextDecorations.Strikethrough)
                    beforeFont.TextDecorationType = TextDecorationType.Strikethrough;
                else if (textBox.TextDecorations == TextDecorations.Baseline)
                    beforeFont.TextDecorationType = TextDecorationType.Baseline;
                else if (textBox.TextDecorations == TextDecorations.Underline)
                    beforeFont.TextDecorationType = TextDecorationType.Underline;
            }
            SelectedFont = beforeFont;
        }
        public void GetFont(TextBlock textBlock)
        {
            var beforeFont = new Font
            {
                FontFamily = textBlock.FontFamily,
                FontSize = textBlock.FontSize,
                FamilyTypeface = new FamilyTypeface { Style = textBlock.FontStyle, Stretch = textBlock.FontStretch, Weight = textBlock.FontWeight }
            };
            if (textBlock.TextDecorations == TextDecorations.OverLine)
                beforeFont.TextDecorationType = TextDecorationType.OverLine;
            else if (textBlock.TextDecorations == TextDecorations.Strikethrough)
                beforeFont.TextDecorationType = TextDecorationType.Strikethrough;
            else if (textBlock.TextDecorations == TextDecorations.Baseline)
                beforeFont.TextDecorationType = TextDecorationType.Baseline;
            else if (textBlock.TextDecorations == TextDecorations.Underline)
                beforeFont.TextDecorationType = TextDecorationType.Underline;
            SelectedFont = beforeFont;
        }
        public void SetFont(Control control)
        {
            Font font = SelectedFont;
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
        public void SetFont(TextBlock textblock)
        {
            Font font = SelectedFont;
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
