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
        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }
        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ColorPickerControl), new PropertyMetadata(null));

        public bool WithoutColorCells
        {
            get { return (bool)GetValue(WithoutColorCellsProperty); }
            set { SetValue(WithoutColorCellsProperty, value); }
        }
        public static readonly DependencyProperty WithoutColorCellsProperty = DependencyProperty.Register("WithoutColorCells", typeof(bool), typeof(ColorPickerControl), new PropertyMetadata(null));


       /* public SupportedLanguage UILanguage
        {
            get { return (SupportedLanguage)GetValue(UILanguageProperty); }
            set{SetValue(UILanguageProperty, value);}
        }
        public static readonly DependencyProperty UILanguageProperty = DependencyProperty.Register("UILanguage", typeof(SupportedLanguage), typeof(ColorPickerControl), new FrameworkPropertyMetadata(0,new PropertyChangedCallback(UILanguagePropertyChanged)));

        private static void UILanguagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            switch (e.NewValue)
            {
                case SupportedLanguage.Auto:
                    LanguageManager.SwitchLanguage(System.Threading.Thread.CurrentThread.CurrentUICulture.Name);
                    break;
                case SupportedLanguage.Chinese:
                    LanguageManager.SwitchLanguage("zh-CN");
                    break;
                case SupportedLanguage.English:
                    LanguageManager.SwitchLanguage("en-US");
                    break;
            }
        }*/

        bool changing = false;
        bool loaded = false;
        ObservableCollection<Color> saveColors = new ObservableCollection<Color>();
        int savedIndex = 0;

        public ColorPickerControl()
        {
            InitializeComponent();
            SaveItems.DataContext = saveColors;

            for (int i = 0; i < 8; i++)
                saveColors.Add(Colors.White);

            PickerLanguageManager.SwitchLanguage(System.Threading.Thread.CurrentThread.CurrentUICulture);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loaded = true;
            ApplyChangeToText(ColorFormat.None);
        }
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
        private void PickerGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
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

                PickerPath.Margin = new Thickness(position.X - 5, position.Y - 5, 0, 0);
                changing = true;
                ApplyChangeToText(ColorFormat.Picker);
                changing = false;
            }
        }
        private void ColorGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double left = double.Parse(HSLH.Text) / 360.0 * PickerGrid.ActualWidth;
            double top = (1 - double.Parse(HSLS.Text) / 100.0) * PickerGrid.ActualHeight;
            PickerPath.Margin = new Thickness(left - 5, top - 5, 0, 0);
        }
        private void PickScreenButton_Click(object sender, RoutedEventArgs e)
        {
            ScreenPickerWindow screenPickerWindow = new ScreenPickerWindow();
            screenPickerWindow.ShowDialog();
            if (screenPickerWindow.pickColor != Colors.Transparent)
            {
                SelectedColor = screenPickerWindow.pickColor;
                ApplyChangeToText(ColorFormat.None);
            }
        }
        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            SelectedColor = (button.Background as SolidColorBrush).Color;
            ApplyChangeToText(ColorFormat.None);
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void HEXTextbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9\#]+$");
            e.Handled = !regex.IsMatch(e.Text);
        }
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
            if (fromColorFormat != ColorFormat.HSL && fromColorFormat != ColorFormat.Picker)
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
            if (fromColorFormat != ColorFormat.Picker)
            {
                double left = double.Parse(HSLH.Text) / 360.0 * PickerGrid.ActualWidth;
                double top = (1 - double.Parse(HSLS.Text) / 100.0) * PickerGrid.ActualHeight;
                PickerPath.Margin = new Thickness(left - 5, top - 5, 0, 0);
                LightSlider.Value = int.Parse(HSLL.Text);
            }
            changing = false;
        }

        enum ColorFormat
        {
            None,
            RGB,
            HSL,
            HEX,
            Picker,
        }

    }
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

}
