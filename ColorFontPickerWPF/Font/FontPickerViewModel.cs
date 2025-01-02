using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media;

namespace ColorFontPickerWPF
{
    /// <summary>
    /// Used to bind the selected Family Typeface item.
    /// 用于绑定 选中的Family Typeface 项
    /// </summary>
    internal class FontPickerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        private string _FontFamilyText;
        /// <summary>
        /// font family text
        /// 字体文本
        /// </summary>
        public string FontFamilyText
        {
            get { return _FontFamilyText; }
            set
            {
                _FontFamilyText = value;
                NotifyPropertyChanged();
            }
        }

        private FamilyTypeface _FamilyTypeFace = new FamilyTypeface();
        /// <summary>
        /// font family Typeface
        /// 字体样式
        /// </summary>
        public FamilyTypeface FamilyTypeFace
        {
            get { return _FamilyTypeFace; }
            set
            {
                _FamilyTypeFace = value;
                NotifyPropertyChanged();
            }
        }
    }
}
