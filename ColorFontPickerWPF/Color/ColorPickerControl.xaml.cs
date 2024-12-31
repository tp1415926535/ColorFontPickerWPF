//using ColorMine.ColorSpaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
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
    /// ColorPickerControl.xaml 的交互逻辑
    /// </summary>
    public partial class ColorPickerControl : UserControl
    {
        /// <summary>
        /// Prevents properties from being re-altered when changing from internal code
        /// 从内部方法更改时防止被属性被重改
        /// </summary>
        bool changing { get; set; } = false;

        /// <summary>
        /// Initialisation check
        /// 初始化校验
        /// </summary>
        bool loaded { get; set; } = false;

        /// <summary>
        /// Saved colors list
        /// 保存的颜色列表
        /// </summary>
        ObservableCollection<Color> saveColors { get; set; } = new ObservableCollection<Color>();

        /// <summary>
        /// Determine how many colours have been added
        /// 确定有多少种颜色被添加
        /// </summary>
        int savedIndex { get; set; } = 0;

        public ColorPickerControl()
        {
            InitializeComponent();
            SaveItems.DataContext = saveColors;
            for (int i = 0; i < 8; i++)
                saveColors.Add(Colors.White);
            //var currentCulture = PickerLanguageManager.Settings.UIculture;
        }

        /// <summary>
        /// Control Load Completion Event
        /// 控件加载完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loaded = true;
            ApplyChangeToText(ColorFormat.None);
        }

        /// <summary>
        /// Update slider value when mouse is held down for movement
        /// 按住鼠标移动时更新滑动条的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slider_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var slider = (Slider)sender;
                Point position = e.GetPosition(slider);
                double d = 1 - (position.Y - 5) / (slider.ActualHeight - 10);
                d = d < 0 ? 0 : d;
                d = d > 1 ? 1 : d;
                double value = slider.Minimum + (slider.Maximum - slider.Minimum) * d;
                slider.Value = slider.TickFrequency == 1 ? Math.Round(value) : Math.Round(value, 2);
            }
        }
        /// <summary>
        /// Drag and drop field pickup points to change colour
        /// 拖拽域拾取点更改颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PickerGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (changing) return;
                changing = true;
                try
                {
                    var grid = (Grid)sender;
                    Point position = e.GetPosition(grid);

                    HSL hsl = new RGB(SelectedColor.R, SelectedColor.G, SelectedColor.B).ToHSL();
                    hsl.H = (int)Math.Round(position.X / grid.ActualWidth * 360);
                    hsl.S = (int)Math.Round((1 - position.Y / grid.ActualHeight) * 100);
                    HSLH.Text = hsl.H.ToString();
                    HSLS.Text = hsl.S.ToString();
                    RGB rgb = hsl.ToRGB();
                    SelectedColor = rgb.ToColor();

                    ApplyChangeToText(ColorFormat.HSL);
                }
                catch { }
                changing = false;
            }
        }
        /// <summary>
        /// Screen Global Pickup Colour
        /// 屏幕全局拾取颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PickScreenButton_Click(object sender, RoutedEventArgs e)
        {
            ScreenPickerWindow screenPickerWindow = new ScreenPickerWindow();
            screenPickerWindow.ShowDialog();
            if (screenPickerWindow.pickColor != Colors.Transparent)
            {
                if (changing) return;
                changing = true;
                try
                {
                    SelectedColor = screenPickerWindow.pickColor;
                    ApplyChangeToText(ColorFormat.None);
                }
                catch { }
                changing = false;
            }
        }
        /// <summary>
        /// Apply a colour from a preset or saved colour to the current
        /// 从预设颜色或保存的颜色应用到当前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (changing) return;
            changing = true;
            try
            {
                Button button = sender as Button;
                SelectedColor = (button.Background as SolidColorBrush).Color;
                ApplyChangeToText(ColorFormat.None);
            }
            catch { }
            changing = false;
        }
        /// <summary>
        /// Restrict text to numbers only
        /// 限制文本只能输入数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        /// <summary>
        /// Restrict text to alphanumeric and #
        /// 限制文本只能输入字母数字和#
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HEXTextbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9\#]+$");
            e.Handled = !regex.IsMatch(e.Text);
        }
        /// <summary>
        /// Save the current colour to the list
        /// 保存当前颜色到列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (savedIndex >= 0 && savedIndex < 8)
            {
                if (savedIndex > 0 && saveColors[savedIndex - 1].Equals(SelectedColor)) return;
                saveColors[savedIndex] = SelectedColor;
            }
            else
            {
                if (saveColors[saveColors.Count - 1].Equals(SelectedColor)) return;
                saveColors.Add(SelectedColor);
                saveColors.RemoveAt(0);
            }
            savedIndex++;
        }
        /// <summary>
        /// RGB text change
        /// RGB文本变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RGB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (changing) return;
            changing = true;
            try
            {
                TextBox textbox = sender as TextBox;
                switch (textbox.Name)
                {
                    case "RGBR":
                        int R = int.Parse(textbox.Text);
                        SelectedColor = Color.FromRgb((byte)R, SelectedColor.G, SelectedColor.B);
                        break;
                    case "RGBG":
                        int G = int.Parse(textbox.Text);
                        SelectedColor = Color.FromRgb(SelectedColor.R, (byte)G, SelectedColor.B);
                        break;
                    case "RGBB":
                        int B = int.Parse(textbox.Text);
                        SelectedColor = Color.FromRgb(SelectedColor.R, SelectedColor.G, (byte)B);
                        break;
                }
                ApplyChangeToText(ColorFormat.RGB);
            }
            catch { }
            changing = false;
        }
        /// <summary>
        /// HSL text change
        /// HSL文本变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HSL_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (changing) return;
            changing = true;
            try
            {
                TextBox textbox = sender as TextBox;
                HSL hsl = new RGB(SelectedColor.R, SelectedColor.G, SelectedColor.B).ToHSL();
                switch (textbox.Name)
                {
                    case "HSLH":
                        hsl.H = (byte)int.Parse(textbox.Text);
                        break;
                    case "HSLS":
                        hsl.S = (byte)int.Parse(textbox.Text);
                        break;
                    case "HSLL":
                        hsl.L = (byte)int.Parse(textbox.Text);
                        break;
                }
                RGB rgb = hsl.ToRGB();
                SelectedColor = rgb.ToColor();

                ApplyChangeToText(ColorFormat.HSL);
            }
            catch { }
            changing = false;
        }
        /// <summary>
        /// HEX text change
        /// HEX文本变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HEX_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (changing) return;
            changing = true;
            try
            {
                TextBox textbox = sender as TextBox;
                RGB rgb = new HEX(textbox.Text).ToRGB();
                SelectedColor = rgb.ToColor();

                ApplyChangeToText(ColorFormat.HEX);
            }
            catch { }
            changing = false;
        }

        /// <summary>
        /// Update other values according to the source of the colour change
        /// 根据颜色变更来源更新其他值
        /// </summary>
        /// <param name="fromColorFormat"></param>
        private void ApplyChangeToText(ColorFormat fromColorFormat)
        {
            if (!loaded)
            {
                changing = false;
                return;
            }
            RGB rgb = new RGB(SelectedColor.R, SelectedColor.G, SelectedColor.B);
            try
            {
                switch (fromColorFormat)
                {
                    case ColorFormat.HSL:
                        rgb = new HSL(int.Parse(HSLH.Text), byte.Parse(HSLS.Text), byte.Parse(HSLL.Text)).ToRGB();
                        break;
                }
                SelectedColor = rgb.ToColor();
            }
            catch { }

            if (fromColorFormat != ColorFormat.RGB)
            {
                RGBR.Text = rgb.R.ToString();
                RGBG.Text = rgb.G.ToString();
                RGBB.Text = rgb.B.ToString();
            }
            if (fromColorFormat != ColorFormat.HSL)
            {
                HSL hsl = rgb.ToHSL();
                HSLH.Text = hsl.H.ToString();
                HSLS.Text = hsl.S.ToString();
                HSLL.Text = hsl.L.ToString();
            }
            if (fromColorFormat != ColorFormat.HEX)
            {
                HEX hex = rgb.ToHEX();
                HEXTextbox.Text = hex.Code;
            }
            changing = false;
        }
    }

}
