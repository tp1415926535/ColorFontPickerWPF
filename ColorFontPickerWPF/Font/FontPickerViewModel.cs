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
        /// font family text, to prevent empty or unrecognizable font names
        /// 字体文本，防止出现空的或者无法识别的字体名
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
        /// font family Typeface, used to bind font-native data and transform entity
        /// 字体样式，用于绑定字体原生数据并转换实体
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
