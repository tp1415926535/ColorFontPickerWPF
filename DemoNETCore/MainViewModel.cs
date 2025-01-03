using ColorFontPickerWPF;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DemoNETCore
{
    [AddINotifyPropertyChangedInterface]
    public partial class MainViewModel
    {

        public Color SelectedColor { get; set; } = Colors.LightGreen;


        public Color PopupSelectedColor { get; set; } = Colors.SkyBlue;


        public Font SelectedFont { get; set; }


        public Font PopupSelectedFont { get; set; } = new Font() { FontFamily = new FontFamily("Arial"), FontSize = 20 };


        [RelayCommand]
        public void ColorChange()
        {
            Debug.WriteLine(SelectedColor.ToString());
        }


        [RelayCommand]
        public void PopupColorChange()
        {
            Debug.WriteLine(PopupSelectedColor.ToString());
        }


        [RelayCommand]
        public void FontChange()
        {
            Debug.WriteLine(JsonConvert.SerializeObject(SelectedFont));
        }
    }
}
