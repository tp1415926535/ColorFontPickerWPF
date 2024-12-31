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
        /// <summary>
        /// Defining Language Resources
        /// 定义语言资源
        /// </summary>
        static Dictionary<string, ResourceDictionary> languages { get; set; } = new Dictionary<string, ResourceDictionary>() {
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

        /// <summary>
        /// Externally available settings
        /// 对外提供的设置
        /// </summary>
        public static PickerSettings Settings { get; set; } = new PickerSettings() { UIculture = System.Threading.Thread.CurrentThread.CurrentUICulture };

        /// <summary>
        /// Switch to target language
        /// 切换到目标语言
        /// </summary>
        /// <param name="cultureInfo"></param>
        private static void SwitchLanguage(CultureInfo cultureInfo)
        {
            if (cultureInfo.Name.Length < 2)
                SetLanguageResource("en");
            else
            {
                var lan = cultureInfo.Name.Substring(0, 2).ToLower();
                if (!languages.ContainsKey(lan))
                    SetLanguageResource("en");
                else
                    SetLanguageResource(lan);
            }
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        }

        /// <summary>
        /// Add or remove resource
        /// 添加或移除资源
        /// </summary>
        /// <param name="lan"></param>
        private static void SetLanguageResource(string lan)
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

        /// <summary>
        /// Entities used for setting
        /// 设置的实体
        /// </summary>
        public class PickerSettings
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
