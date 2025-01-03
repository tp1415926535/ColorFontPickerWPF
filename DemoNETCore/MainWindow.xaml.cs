using ColorFontPickerWPF;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Media;

namespace DemoNETCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //PickerLanguageManager.settings.UIculture = new CultureInfo("en-US");

            colorPickerPopup.DataContext = colorPicker.DataContext =
            fontPicker.DataContext = fontPickerPopup.DataContext =
            BindExtProText.DataContext = new MainViewModel();
        }

        private void ColorDialogButton_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.SelectedColor = ((SolidColorBrush)PreviewLabel.Background).Color;
            if (colorDialog.ShowDialog() == true)
                PreviewLabel.Background = new SolidColorBrush(colorDialog.SelectedColor);
        }

        private void FontDialogButton_Click(object sender, RoutedEventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.GetFont(PreviewTextBlock);
            if (fontDialog.ShowDialog() == true)
                fontDialog.SetFont(PreviewTextBlock);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            fontPicker.SelectedFont = new Font
            {
                FontFamily = new FontFamily("Microsoft YaHei UI"),
                FontSize = 12,
            };
            colorPicker.SelectedColor = Colors.Blue;
        }

        private void colorPicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            Debug.WriteLine(colorPicker.SelectedColor);
            //Debug.WriteLine(e.NewValue);
        }

        private void colorPickerPopup_ValueChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            Debug.WriteLine(colorPickerPopup.ColorTextValue);
        }

        private void fontPicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<Font> e)
        {
            Debug.WriteLine(JsonConvert.SerializeObject(fontPicker.SelectedFont));
        }

        private void fontPickerPopup_ValueChanged(object sender, RoutedPropertyChangedEventArgs<Font> e)
        {
            Debug.WriteLine(JsonConvert.SerializeObject(fontPickerPopup.SelectedFont));
        }
    }

}
