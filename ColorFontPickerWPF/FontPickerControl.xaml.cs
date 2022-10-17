using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorFontPickerWPF
{
    /// <summary>
    /// FontPickerControl.xaml 的交互逻辑
    /// </summary>
    public partial class FontPickerControl : UserControl
    {
        public Font SelectedFont
        {
            get { return (Font)GetValue(SelectedFontProperty); }
            set { SetValue(SelectedFontProperty, value);
                UpdateFontChanged();
            }
        }
        public static readonly DependencyProperty SelectedFontProperty = DependencyProperty.Register("SelectedFont", typeof(Font), typeof(FontPickerControl), new PropertyMetadata(null));
        public bool WithoutDecorations
        {
            get { return (bool)GetValue(WithoutDecorationsProperty); }
            set { SetValue(WithoutDecorationsProperty, value); }
        }
        public static readonly DependencyProperty WithoutDecorationsProperty = DependencyProperty.Register("WithoutDecorations", typeof(bool), typeof(FontPickerControl), new PropertyMetadata(null));
        public bool WithoutPreviewRow
        {
            get { return (bool)GetValue(WithoutPreviewProperty); }
            set { SetValue(WithoutPreviewProperty, value); }
        }
        public static readonly DependencyProperty WithoutPreviewProperty = DependencyProperty.Register("WithoutPreviewRow", typeof(bool), typeof(FontPickerControl), new PropertyMetadata(null));


        Dictionary<string, double> fontSizeDic = new Dictionary<string, double>
        {
            {"8",8 },{"9",9},{"10",10},{"11",11 },{"12",12},{"14",14},{"16",16 },{"18",18},{"20",20},{"22",22},{"24",24},{"26",26},{"28",28 },{"36",36},{"48",48},{"72",72},
            {"初号",42 },{"小初",36 },{"一号",26 },{"小一",24 },{"二号",22 },{"小二",18 },{"三号",16 },{"小三",15 },{"四号",14 },{"小四",12 },{"五号",10.5 },{"小五",9 },
            {"六号",7.5 },{"小六",6.5 },{"七号",5.5 },{"八号",5 },
        };
        bool loaded = false;
        Font defaultFont = new Font() { FontFamily = new FontFamily("Microsoft YaHei UI"), FamilyTypeface = new FamilyTypeface(), FontSize = 12 };

        public FontPickerControl()
        {
            InitializeComponent();

            var language = XmlLanguage.GetLanguage(CultureInfo.CurrentUICulture.Name);
            var list = Fonts.SystemFontFamilies.Select(font => font.FamilyNames.ContainsKey(language) && !string.IsNullOrEmpty(font.FamilyNames[language]) ? new FontFamily(font.FamilyNames[language]) : font);
            FontSelector.ItemsSource = list;
            SizeSelector.ItemsSource = fontSizeDic;

            PickerLanguageManager.SwitchLanguage(System.Threading.Thread.CurrentThread.CurrentUICulture);
            try
            {
                string lan = System.Threading.Thread.CurrentThread.CurrentUICulture.Name.ToLower().Substring(0, 2);
                for (int i = 0; i < LanguageCombo.Items.Count; i++)
                {
                    ComboBoxItem comboBoxItem = LanguageCombo.Items[i] as ComboBoxItem;
                    if (comboBoxItem.Tag.ToString().Equals(lan))
                    {
                        LanguageCombo.SelectedIndex = i;
                        break;
                    }
                }
            }
            catch { }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loaded = true;
            if (SelectedFont == null)
                SelectedFont = defaultFont;

        }
        private void UpdateFontChanged()
        {
            SizeSelector.SelectedItem = fontSizeDic.FirstOrDefault(x => x.Value.Equals(SelectedFont.FontSize));
            if (SizeSelector.SelectedItem == null)
                SizeTextbox.Text = SelectedFont.FontSize.ToString();
            switch (SelectedFont.TextDecorationType)
            {
                case TextDecorationType.None:
                    NoLineRadio.IsChecked = true;
                    break;
                case TextDecorationType.OverLine:
                    OverLineRadio.IsChecked = true;
                    break;
                case TextDecorationType.Strikethrough:
                    StrikethroughRadio.IsChecked = true;
                    break;
                case TextDecorationType.Baseline:
                    BaselineRadio.IsChecked = true;
                    break;
                case TextDecorationType.Underline:
                    UnderlineRadio.IsChecked = true;
                    break;
            }
            ScrollToSelection();
        }
        public void ScrollToSelection()
        {
            FontSelector.ScrollIntoView(FontSelector.SelectedItem);
            TypefaceSelector.ScrollIntoView(TypefaceSelector.SelectedItem);
            SizeSelector.ScrollIntoView(SizeSelector.SelectedItem);
        }
        private void SizeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pair = (KeyValuePair<string, double>)SizeSelector.SelectedItem;
            SelectedFont.FontSize = pair.Value;
            SizeTextbox.Text = pair.Key.ToString();
        }
        private void FontSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypefaceSelector.SelectedIndex == -1)
                TypefaceSelector.SelectedIndex = 0;
        }
        private void FontTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(FontTextbox.Text)) return;
            if (FontSelector.SelectedItem == null) return;
            if (FontTextbox.Text.Equals(FontSelector.SelectedItem.ToString())) return;
            bool matchedStart = false;
            foreach (var item in FontSelector.Items)
            {
                if (item.ToString().ToLower().Equals(FontTextbox.Text.ToLower()))
                {
                    FontSelector.SelectedItem = item;
                    FontSelector.ScrollIntoView(FontSelector.SelectedItem);
                    break;
                }
                if (!matchedStart && item.ToString().ToLower().StartsWith(FontTextbox.Text.ToLower()))
                {
                    FontSelector.ScrollIntoView(item);
                    matchedStart = true;
                }
            }
        }

        private void TypefaceTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(TypefaceTextbox.Text)) return;
            if (TypefaceSelector.SelectedItem == null) return;
            if (TypefaceTextbox.Text.Equals(TypefaceSelector.SelectedItem.ToString())) return;
            var language = XmlLanguage.GetLanguage("en-US");
            bool matchedStart = false;
            foreach (FamilyTypeface item in TypefaceSelector.Items)
            {
                if (item.AdjustedFaceNames[language].ToLower().Equals(TypefaceTextbox.Text.ToLower()))
                {
                    TypefaceSelector.SelectedItem = item;
                    TypefaceSelector.ScrollIntoView(TypefaceSelector.SelectedItem);
                    break;
                }
                if (!matchedStart && item.ToString().ToLower().StartsWith(TypefaceTextbox.Text.ToLower()))
                {
                    TypefaceSelector.ScrollIntoView(item);
                    matchedStart = true;
                }
            }
        }
        private void SizeTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SizeTextbox.Text)) return;
            if (SizeSelector.SelectedItem == null) return;
            if (SizeTextbox.Text.Equals(((KeyValuePair<string, double>)SizeSelector.SelectedItem).Key)) return;

            double value = 0;
            if (double.TryParse(SizeTextbox.Text, out value))
            {
                SelectedFont.FontSize = value;
                SizeSelector.SelectedItem = fontSizeDic.FirstOrDefault(x => x.Value.Equals(SelectedFont.FontSize));
            }
            else if (fontSizeDic.ContainsKey(SizeTextbox.Text))
                SizeSelector.SelectedItem = fontSizeDic.FirstOrDefault(x => x.Key.Equals(SizeTextbox.Text));
            SizeSelector.ScrollIntoView(SizeSelector.SelectedItem);
        }

        private void DecorationRadio_Checked(object sender, RoutedEventArgs e)
        {
            if (!loaded) return;
            string name = (sender as RadioButton).Name;
            switch (name)
            {
                case "NoLineRadio":
                    SelectedFont.TextDecorationType = TextDecorationType.None;
                    break;
                case "OverLineRadio":
                    SelectedFont.TextDecorationType = TextDecorationType.OverLine;
                    break;
                case "StrikethroughRadio":
                    SelectedFont.TextDecorationType = TextDecorationType.Strikethrough;
                    break;
                case "BaselineRadio":
                    SelectedFont.TextDecorationType = TextDecorationType.Baseline;
                    break;
                case "UnderlineRadio":
                    SelectedFont.TextDecorationType = TextDecorationType.Underline;
                    break;
            }
        }
        public void GetFont(Control control)
        {
            SelectedFont = FontHelper.GetFont(control);
        }
        public void GetFont(TextBlock textBlock)
        {
            SelectedFont = FontHelper.GetFont(textBlock);
        }
        public void SetFont(Control control)
        {
            FontHelper.SetFont(control, SelectedFont);
        }
        public void SetFont(TextBlock textblock)
        {
            FontHelper.SetFont(textblock, SelectedFont);
        }

    }
    public class Font : INotifyPropertyChanged
    {
        private FontFamily _FontFamily;
        public FontFamily FontFamily
        {
            get { return _FontFamily; }
            set
            {
                _FontFamily = value;
                this.NotifyPropertyChanged("FontFamily");
            }
        }

        private FamilyTypeface _FamilyTypeface = new FamilyTypeface();
        public FamilyTypeface FamilyTypeface
        {
            get { return _FamilyTypeface; }
            set
            {
                _FamilyTypeface = value;
                this.NotifyPropertyChanged("FamilyTypeface");
            }
        }

        private TextDecorationType _TextDecorationType;
        public TextDecorationType TextDecorationType
        {
            get { return _TextDecorationType; }
            set
            {
                _TextDecorationType = value;
                this.NotifyPropertyChanged("TextDecorationType");
            }
        }

        private double _FontSize;
        public double FontSize
        {
            get { return _FontSize; }
            set
            {
                _FontSize = value;
                this.NotifyPropertyChanged("FontSize");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
    public enum TextDecorationType
    {
        None,
        OverLine,
        Strikethrough,
        Baseline,
        Underline,
    }

}
