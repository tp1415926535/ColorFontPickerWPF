using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ColorFontPickerWPF
{
	public partial class FontPickerPopup
	{
		/// <summary>
		/// Text displayed outside
		/// 外面显示的文本
		/// </summary>
		public string FontText
		{
			get { return (string)GetValue(FontTextProperty); }
			set { SetValue(FontTextProperty, value); }
		}
		public static readonly DependencyProperty FontTextProperty = DependencyProperty.Register(nameof(FontText), typeof(string), typeof(FontPickerPopup), new PropertyMetadata(null));


		/// <summary>
		/// Current font information
		/// 当前的字体信息
		/// </summary>
		public Font SelectedFont
		{
			get { return (Font)GetValue(SelectedFontProperty); }
			set { SetValue(SelectedFontProperty, value); }
		}
		public static readonly DependencyProperty SelectedFontProperty = DependencyProperty.Register(nameof(SelectedFont), typeof(Font), typeof(FontPickerPopup), new PropertyMetadata(SelectionChangedEvent));
		/// <summary>
		/// 当前字体改变
		/// </summary>
		/// <param name="d"></param>
		/// <param name="e"></param>
		private static void SelectionChangedEvent(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var control = d as FontPickerPopup;
			if (control == null) return;
			//control.SelectedFont = (Font)e.NewValue;
			try
			{
				control.fontPicker.SelectedFont = control.SelectedFont;
                //valueChange
                if (control.ValueChanged != null)
                    control.ValueChanged(control, new RoutedPropertyChangedEventArgs<Font>((Font)e.OldValue, (Font)e.NewValue));
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
		public event RoutedPropertyChangedEventHandler<Font> ValueChanged;
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
		public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(FontPickerPopup), new PropertyMetadata(null));

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
		public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(FontPickerPopup), new PropertyMetadata(null));

	}
}
