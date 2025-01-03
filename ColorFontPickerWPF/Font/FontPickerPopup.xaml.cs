using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// FontPickerPopup.xaml 的交互逻辑
    /// </summary>
    public partial class FontPickerPopup : UserControl
    {
        public FontPickerPopup()
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
            fontPicker.ScrollToSelection();
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
