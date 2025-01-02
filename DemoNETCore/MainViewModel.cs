using CommunityToolkit.Mvvm.Input;
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
    }
}
