using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
        /// <summary>
        /// Get selected color text
        /// 获取选中颜色文本
        /// </summary>
        public string ColorTextValue
        {
            get
            {
                switch (ColorText)
                {
                    case ColorTextFormat.RGB:
                        return $"RGB({SelectedColor.R},{SelectedColor.G},{SelectedColor.B})";
                    case ColorTextFormat.HEX:
                        return new RGB(SelectedColor).ToHEX().Code;
                    case ColorTextFormat.HSL:
                        var hsl = new RGB(SelectedColor).ToHSL();
                        return $"HSL({hsl.H},{hsl.S},{hsl.L})";
                    default:
                        return string.Empty;
                }
            }
        }

        public ColorPickerPopup()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Show or close popup
        /// 打开或关闭popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ControlPopup.IsOpen = !ControlPopup.IsOpen;
        }
    }
}
