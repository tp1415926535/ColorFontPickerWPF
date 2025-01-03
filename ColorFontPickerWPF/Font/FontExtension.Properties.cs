using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Documents;
using System.Drawing;
using System.Windows.Media;
using System.ComponentModel;

namespace ColorFontPickerWPF
{
    public static class FontExtension
    {
        static Dictionary<DependencyObject, FontUpdateMananger> managerDict { get; set; } = new Dictionary<DependencyObject, FontUpdateMananger>();

        public static readonly DependencyProperty FontDataProperty = DependencyProperty.RegisterAttached("FontData", typeof(Font), typeof(FontExtension), new PropertyMetadata(null, OnFontDataChanged));

        public static Font GetFontData(DependencyObject obj)
        {
            return (Font)obj.GetValue(FontDataProperty);
        }
        public static void SetFontData(DependencyObject obj, Font value)
        {
            obj.SetValue(FontDataProperty, value);
        }

        private static void OnFontDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (managerDict.ContainsKey(d))
            {
                managerDict[d].Dispose();
                managerDict.Remove(d);
            }
            if (e.NewValue != null)
                managerDict.Add(d, new FontUpdateMananger(d, (Font)e.NewValue));
        }
    }


    public class FontUpdateMananger
    {
        DependencyObject dependency { get; set; }
        Font font { get; set; }
        public FontUpdateMananger(DependencyObject d, Font f)
        {
            font = f;
            dependency = d;

            font.PropertyChanged += OnFontPropertyChanged;
            UpdateFont();
        }

        private void OnFontPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateFont();
        }

        private void UpdateFont()
        {
            FontHelper.SetFont(dependency, font);
        }

        public void Dispose()
        {
            font.PropertyChanged -= OnFontPropertyChanged;
            font = null;
            dependency = null;
        }
    }
}
