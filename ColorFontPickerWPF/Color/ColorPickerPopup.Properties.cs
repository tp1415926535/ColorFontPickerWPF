using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ColorFontPickerWPF
{
	public partial class ColorPickerPopup
	{
		/// <summary>
		/// Type of color text to display
		/// 显示颜色文本的类型
		/// </summary>
		public ColorTextFormat ColorText
		{
			get { return (ColorTextFormat)GetValue(ColorTextProperty); }
			set { SetValue(ColorTextProperty, value); }
		}
		public static readonly DependencyProperty ColorTextProperty = DependencyProperty.Register("ColorText", typeof(ColorTextFormat), typeof(ColorPickerPopup), new PropertyMetadata(null));

		/// <summary>
		/// Selected Color
		/// 选中的颜色
		/// </summary>
		public Color SelectedColor
		{
			get { return (Color)GetValue(SelectedColorProperty); }
			set { SetValue(SelectedColorProperty, value); }
		}
		public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ColorPickerPopup), new PropertyMetadata(SelectionChangedEvent));

		/// <summary>
		/// Selected colour property change events
		/// 选中颜色属性变更事件
		/// </summary>
		/// <param name="d"></param>
		/// <param name="e"></param>
		private static void SelectionChangedEvent(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var control = d as ColorPickerPopup;
			if (control == null || control.ValueChanged == null) return;
			try
			{
				//control.SelectedColor = control.colorPicker.SelectedColor = (Color)e.NewValue;
				control.colorPicker.SelectedColor = (Color)e.NewValue;
				//valueChange
				control.ValueChanged(control, new RoutedPropertyChangedEventArgs<Color>((Color)e.OldValue, (Color)e.NewValue));
				//Command
				if (control.Command != null && control.Command.CanExecute(control.CommandParameter))
					control.Command.Execute(control.CommandParameter);
			}
			catch { }
		}

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
		public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(ColorPickerPopup), new PropertyMetadata(null));

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
		public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(ColorPickerPopup), new PropertyMetadata(null));

	}
}
