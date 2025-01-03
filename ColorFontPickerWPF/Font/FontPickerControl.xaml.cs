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
        /// defualt font
        /// 默认字体
        /// </summary>
        Font defaultFont { get; set; } = new Font() { FontSize = 12 };

        /// <summary>
        /// Font Size Display and Corresponding Size Dictionary
        /// 字号显示和对应大小字典
        /// </summary>
        static Dictionary<string, double> fontSizeDic { get; set; } = new Dictionary<string, double>
        {
            {"8",8 },{"9",9},{"10",10},{"11",11 },{"12",12},{"14",14},{"16",16 },{"18",18},{"20",20},{"22",22},{"24",24},{"26",26},{"28",28 },{"36",36},{"48",48},{"72",72},
            {"初号",42 },{"小初",36 },{"一号",26 },{"小一",24 },{"二号",22 },{"小二",18 },{"三号",16 },{"小三",15 },{"四号",14 },{"小四",12 },{"五号",10.5 },{"小五",9 },
            {"六号",7.5 },{"小六",6.5 },{"七号",5.5 },{"八号",5 },
        };

        /// <summary>
        /// all font names
        /// 所有字体名称
        /// </summary>
        static List<string> fontNames { get; set; } = Fonts.SystemFontFamilies.SelectMany(f => f.FamilyNames.Values).Select(f => f.ToLowerInvariant()).ToList();

        /// <summary>
        /// font family typeface ViewModel
        /// 文本样式对应的ViewModel
        /// </summary>
        FontPickerViewModel vm { get; set; } = new FontPickerViewModel();

        public FontPickerControl()
        {
            InitializeComponent();
            //Setting the default language resource 设置默认的语言资源
            var currentCulture = PickerLanguageManager.Settings.UIculture;

            //Convert font name to current language text. 将字体名称转为当前语言文本
            var language = XmlLanguage.GetLanguage(currentCulture.Name);//CultureInfo.CurrentUICulture
            var list = Fonts.SystemFontFamilies.Select(font => font.FamilyNames.ContainsKey(language) && !string.IsNullOrEmpty(font.FamilyNames[language]) ?
                                                                new FontFamily(font.FamilyNames[language]) : font);

            if (fontNames.Contains("Microsoft YaHei UI".ToLowerInvariant()))
                defaultFont.FontFamily = new FontFamily("Microsoft YaHei UI");
            else
                defaultFont.FontFamily = list.First();

            FontSelector.ItemsSource = list;
            SizeSelector.ItemsSource = fontSizeDic;
            TypefaceSelector.DataContext = FontTextbox.DataContext = vm;

            try
            {
                //Settings Example of the current language. 设置当前语言示例
                string lan = currentCulture.Name.ToLower().Substring(0, 2);//System.Threading.Thread.CurrentThread.CurrentUICulture
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

        /// <summary>
        /// UserControl loaded event
        /// 控件加载完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (SelectedFont == null)
                SelectedFont = defaultFont;
            loaded = true;
        }

        /// <summary>
        /// Selected font Changed
        /// 选中字体变更
        /// </summary>
        private void UpdateFontChanged()
        {
            if (changing) return;
            try
            {
                changing = true;
                if (SelectedFont == null)
                    SelectedFont = defaultFont;
                vm.FontFamilyText = SelectedFont.FontFamily.ToString();
                if (!(SizeSelector.SelectedItem != null && fontSizeDic[SizeSelector.SelectedValue.ToString()] == SelectedFont.FontSize))
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
                vm.FamilyTypeFace = new FamilyTypeface()
                {
                    Stretch = SelectedFont.FontStretch,
                    Style = SelectedFont.FontStyle,
                    Weight = SelectedFont.FontWeight,
                };
                ScrollToSelection();
            }
            catch { }
            changing = false;
        }

        /// <summary>
        /// The list scrolls to the position of the selected item
        /// 列表滚动到选中项对应的位置
        /// </summary>
        public void ScrollToSelection()
        {
            FontSelector.ScrollIntoView(FontSelector.SelectedItem);
            TypefaceSelector.ScrollIntoView(TypefaceSelector.SelectedItem);
            SizeSelector.ScrollIntoView(SizeSelector.SelectedItem);
        }

        #region Font Family
        /// <summary>
        /// When the font family list selection changes
        /// 当字体列表选中项变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypefaceSelector.SelectedIndex == -1)
                TypefaceSelector.SelectedIndex = 0;
            vm.FontFamilyText = FontSelector.SelectedItem?.ToString();
        }
        /// <summary>
        /// When the font family textbox changes
        /// 当字体文本框变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(vm.FontFamilyText)) return;
            if (FontSelector.SelectedItem != null && vm.FontFamilyText == FontSelector.SelectedItem.ToString())
            {
                FontSelector.ScrollIntoView(FontSelector.SelectedItem);
                return;
            }
            bool matched = false;
            foreach (var item in FontSelector.Items)
            {
                if (item.ToString().ToLower() == vm.FontFamilyText)
                {
                    FontSelector.SelectedItem = item;
                    FontSelector.ScrollIntoView(FontSelector.SelectedItem);
                    break;
                }
                if (matched == true) continue;
                if (item.ToString().ToLower().Contains(vm.FontFamilyText.ToLower()))
                {
                    FontSelector.ScrollIntoView(item);
                    matched = true;
                }
            }
        }
        #endregion

        #region Family Typeface
        /// <summary>
        /// When the font family typeface list selection changes
        /// 当字体样式列表选中项变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TypefaceSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!loaded) return;
            if (vm.FamilyTypeFace == null) return;
            changing = true;
            try
            {
                SelectedFont.FontStretch = vm.FamilyTypeFace.Stretch;
                SelectedFont.FontStyle = vm.FamilyTypeFace.Style;
                SelectedFont.FontWeight = vm.FamilyTypeFace.Weight;
            }
            catch { }
            changing = false;
        }
        /// <summary>
        /// When the font family typeface textbox changes
        /// 当字体样式文本框变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TypefaceTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(TypefaceTextbox.Text)) return;
            if (TypefaceSelector.SelectedItem != null && TypefaceTextbox.Text == TypefaceSelector.SelectedItem.ToString())
            {
                TypefaceSelector.ScrollIntoView(TypefaceSelector.SelectedItem);
                return;
            }
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
        #endregion

        #region Font Size
        /// <summary>
        /// When the font size list selection changes
        /// 字号列表选中项变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SizeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SizeSelector.SelectedItem == null) return;
            SelectedFont.FontSize = fontSizeDic[SizeSelector.SelectedValue.ToString()];
            SizeTextbox.Text = SizeSelector.SelectedValue.ToString();
        }
        /// <summary>
        /// When the font size textbox changes
        /// 当字号文本框变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SizeTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SizeTextbox.Text)) return;
            if (SizeSelector.SelectedItem != null && SizeTextbox.Text == SizeSelector.SelectedValue.ToString())
            {
                SizeSelector.ScrollIntoView(SizeSelector.SelectedItem);
                return;
            }
            double value = 0;
            if (double.TryParse(SizeTextbox.Text, out value) && value > 0)
            {
                SelectedFont.FontSize = value;
                SizeSelector.SelectedValue = fontSizeDic.FirstOrDefault(x => x.Value.Equals(SelectedFont.FontSize)).Key;
            }
            else if (fontSizeDic.ContainsKey(SizeTextbox.Text))
            {
                if (fontSizeDic[SizeTextbox.Text] != SelectedFont.FontSize)
                    SizeSelector.SelectedValue = SizeTextbox.Text;
            }
            SizeSelector.ScrollIntoView(SizeSelector.SelectedItem);
        }
        #endregion

        /// <summary>
        /// Text Decorator Selection Changes
        /// 文本装饰器选中项变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        #region External methods 对外提供方法
        /// <summary>
        /// Get the font from the specified control as the selected font
        /// 从指定控件获取字体为当前选中字体
        /// </summary>
        /// <param name="control"></param>
        public void GetFont<T>(T control) where T : DependencyObject
        {
            SelectedFont = FontHelper.GetFont(control);
        }
        /// <summary>
        /// Sets the selected font to the specified control
        /// 将当前选中字体设置到指定控件的字体
        /// </summary>
        /// <param name="control"></param>
        public void SetFont<T>(T control) where T : DependencyObject
        {
            FontHelper.SetFont(control, SelectedFont);
        }
        #endregion
    }
}
