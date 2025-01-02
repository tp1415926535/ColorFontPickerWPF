using System;
using System.Collections.Generic;
using System.Text;

namespace ColorFontPickerWPF
{
    /// <summary>
    /// Color format
    /// 颜色类型枚举
    /// </summary>
    internal enum ColorFormat
    {
        None,
        RGB,
        HSL,
        HEX,
        //Picker,
    }


    /// <summary>
    /// Type of color text to display
    /// 显示颜色文本的类型
    /// </summary>
    public enum ColorTextFormat
    {
        None,
        RGB,
        HEX,
        HSL,
    }

    /// <summary>
    /// Text Decoration Type
    /// 文本装饰类型
    /// </summary>
    public enum TextDecorationType
    {
        None,
        OverLine,
        Strikethrough,
        Baseline,
        Underline,
    }
}
