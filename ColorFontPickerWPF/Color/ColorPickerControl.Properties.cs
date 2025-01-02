using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ColorFontPickerWPF
{
    /// <summary>
    /// ColorPicker Porperties
    /// </summary>
    public partial class ColorPickerControl
    {
        /// <summary>
        /// Selected Color
        /// 选中的颜色
        /// </summary>
        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        /// <summary>
        /// Selected colour properties
        /// 选中颜色属性
        /// </summary>
        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ColorPickerControl), new PropertyMetadata(OnValuePropertyChanged));
        /// <summary>
        /// Selected colour property change events
        /// 选中颜色属性变更事件
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ColorPickerControl;
            if (control != null && control.ValueChanged != null)
            {
                try
                {
                    //valueChange
                    control.ValueChanged(control, new RoutedPropertyChangedEventArgs<Color>((Color)e.OldValue, (Color)e.NewValue));
                    //Command
                    if (control.Command != null && control.Command.CanExecute(control.CommandParameter))
                        control.Command.Execute(control.CommandParameter);
                    //Text
                    if (!control.changing)
                        control.ApplyChangeToText(ColorFormat.None);
                }
                catch { }
            }
        }


        /// <summary>
        /// Whether to display preset colour grids, saved colours, etc.
        /// 是否显示预设颜色格和保存的颜色等
        /// </summary>
        public bool WithoutColorCells
        {
            get { return (bool)GetValue(WithoutColorCellsProperty); }
            set { SetValue(WithoutColorCellsProperty, value); }
        }
        /// <summary>
        /// Whether to display the colour grid attribute
        /// 是否显示颜色格子属性
        /// </summary>
        public static readonly DependencyProperty WithoutColorCellsProperty = DependencyProperty.Register("WithoutColorCells", typeof(bool), typeof(ColorPickerControl), new PropertyMetadata(null));


        /// <summary>
        /// Defining value change events
        /// 定义值改变事件
        /// </summary>
        public event RoutedPropertyChangedEventHandler<Color> ValueChanged;




        /// <summary>
        /// Command
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        /// <summary>
        /// The value change provides the Command
        /// 值改变提供Command
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(ColorPickerControl), new PropertyMetadata(null));

        /// <summary>
        /// CommandParameter
        /// </summary>
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        /// <summary>
        /// Allow incoming command parameters
        /// 允许附带命令参数
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(ColorPickerControl), new PropertyMetadata(null));
    }
}
