using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows;

namespace ColorFontPickerWPF
{
    class LanguageManager
    {
        static ResourceDictionary chinese = new ResourceDictionary { Source = new Uri($"/ColorFontPickerWPF;component/Language/ChineseDictionary.xaml", UriKind.Relative) };
        static ResourceDictionary english = new ResourceDictionary { Source = new Uri($"/ColorFontPickerWPF;component/Language/EnglishDictionary.xaml", UriKind.Relative) };

        public static void SwitchLanguage(string culture)
        {
            var dic = Application.Current.Resources.MergedDictionaries;
            if (culture.ToLower().StartsWith("zh"))
            {
                if (dic.Contains(english))
                    dic.Remove(english);
                if (!dic.Contains(chinese))
                    dic.Add(chinese);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            }
            else
            {
                if (dic.Contains(chinese))
                    dic.Remove(chinese);
                if (!dic.Contains(english))
                    dic.Add(english);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            }
        }
    }
}
