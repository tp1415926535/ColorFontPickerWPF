using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ColorFontPickerWPF
{
    /// <summary>
    /// Font Basic Information
    /// 字体基本信息
    /// </summary>
    public class Font : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private FontFamily _FontFamily;
        /// <summary>
        /// font family
        /// 字体
        /// </summary>
        public FontFamily FontFamily
        {
            get { return _FontFamily; }
            set
            {
                _FontFamily = value;
                NotifyPropertyChanged();
            }
        }

        private TextDecorationType _TextDecorationType;
        /// <summary>
        /// text decoration type: Underline, strikethrough, etc.
        /// 装饰类型：下划线，删除线等
        /// </summary>
        public TextDecorationType TextDecorationType
        {
            get { return _TextDecorationType; }
            set
            {
                _TextDecorationType = value;
                NotifyPropertyChanged();
            }
        }

        private FontStyle _FontStyle;
        /// <summary>
        /// font style: Normal, italicized, etc.
        /// 文本样式：正常，斜体等
        /// </summary>
        public FontStyle FontStyle
        {
            get { return _FontStyle; }
            set
            {
                _FontStyle = value;
                NotifyPropertyChanged();
            }
        }

        private FontWeight _FontWeight;
        /// <summary>
        /// font weight: regular, bold, etc.
        /// 字重：常规，加粗等
        /// </summary>
        public FontWeight FontWeight
        {
            get { return _FontWeight; }
            set
            {
                _FontWeight = value;
                NotifyPropertyChanged();
            }
        }

        private FontStretch _FontStretch;
        /// <summary>
        /// font stretch: Normal, Expanded, Condensed, etc.
        /// 文本拉伸方式：正常、拉伸、压缩等
        /// </summary>
        public FontStretch FontStretch
        {
            get { return _FontStretch; }
            set
            {
                _FontStretch = value;
                NotifyPropertyChanged();
            }
        }


        private double _FontSize;
        /// <summary>
        /// font size
        /// 字号
        /// </summary>
        public double FontSize
        {
            get { return _FontSize; }
            set
            {
                _FontSize = value;
                NotifyPropertyChanged();
            }
        }
    }
}
