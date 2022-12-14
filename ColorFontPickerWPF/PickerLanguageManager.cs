using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows;

namespace ColorFontPickerWPF
{
    public class PickerLanguageManager
    {
        static Dictionary<string, ResourceDictionary> languages = new Dictionary<string, ResourceDictionary>() {
            {"zh",  new ResourceDictionary { Source = new Uri($"/ColorFontPickerWPF;component/Language/Chinese.xaml", UriKind.Relative) } },
            {"en",  new ResourceDictionary { Source = new Uri($"/ColorFontPickerWPF;component/Language/English.xaml", UriKind.Relative) } },
            {"ar",  new ResourceDictionary { Source = new Uri($"/ColorFontPickerWPF;component/Language/Arabic.xaml", UriKind.Relative) } },
            {"cs",  new ResourceDictionary { Source = new Uri($"/ColorFontPickerWPF;component/Language/Czech.xaml", UriKind.Relative) } },
            {"fr",  new ResourceDictionary { Source = new Uri($"/ColorFontPickerWPF;component/Language/French.xaml", UriKind.Relative) } },
            {"de",  new ResourceDictionary { Source = new Uri($"/ColorFontPickerWPF;component/Language/German.xaml", UriKind.Relative) } },
            {"hu",  new ResourceDictionary { Source = new Uri($"/ColorFontPickerWPF;component/Language/Hungarian.xaml", UriKind.Relative) } },
            {"it",  new ResourceDictionary { Source = new Uri($"/ColorFontPickerWPF;component/Language/Italian.xaml", UriKind.Relative) } },
            {"ja",  new ResourceDictionary { Source = new Uri($"/ColorFontPickerWPF;component/Language/Japanese.xaml", UriKind.Relative) } },
            {"pt",  new ResourceDictionary { Source = new Uri($"/ColorFontPickerWPF;component/Language/Portuguese.xaml", UriKind.Relative) } },
            {"ro",  new ResourceDictionary { Source = new Uri($"/ColorFontPickerWPF;component/Language/Romanian.xaml", UriKind.Relative) } },
            {"ru",  new ResourceDictionary { Source = new Uri($"/ColorFontPickerWPF;component/Language/Russian.xaml", UriKind.Relative) } },
            {"es",  new ResourceDictionary { Source = new Uri($"/ColorFontPickerWPF;component/Language/Spanish.xaml", UriKind.Relative) } },
            {"sv",  new ResourceDictionary { Source = new Uri($"/ColorFontPickerWPF;component/Language/Swedish.xaml", UriKind.Relative) } },
        };
        public static Settings settings = new Settings() { UIculture = System.Threading.Thread.CurrentThread.CurrentUICulture };

        private static void SwitchLanguage(CultureInfo cultureInfo)
        {
            if (cultureInfo.Name.Length >= 2)
            {
                var lan = cultureInfo.Name.Substring(0, 2).ToLower();
                if (!languages.ContainsKey(lan))
                    SwitchToExist("en");
                else
                    SwitchToExist(lan);
            }
            else
                SwitchToExist("en");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        }
        private static void SwitchToExist(string lan)
        {
            var dic = Application.Current.Resources.MergedDictionaries;
            foreach (var item in languages)
            {
                if (item.Key.Equals(lan)) continue;
                if (dic.Contains(item.Value))
                    dic.Remove(item.Value);
            }
            if (!dic.Contains(languages[lan]))
                dic.Add(languages[lan]);
        }
        public class Settings
        {
            private CultureInfo culture;
            public CultureInfo UIculture
            {
                get { return culture; }
                set
                {
                    culture = value;
                    SwitchLanguage(value);
                }
            }
        }
    }
}
