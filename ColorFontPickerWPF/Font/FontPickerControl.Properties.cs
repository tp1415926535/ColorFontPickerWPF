using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ColorFontPickerWPF
{
    public partial class FontPickerControl
    {
        private static Font beforeFont;

        /// <summary>
        /// Current font information
        /// 当前的字体信息
        /// </summary>
        public Font SelectedFont
        {
            get { return (Font)GetValue(SelectedFontProperty); }
            set { SetValue(SelectedFontProperty, value); }
        }
        public static readonly DependencyProperty SelectedFontProperty = DependencyProperty.Register(nameof(SelectedFont), typeof(Font), typeof(FontPickerControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValuePropertyChanged));
        /// <summary>
        /// Selected font property change events
        /// 选中字体属性变更事件
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FontPickerControl;
            if (control == null) return;
            try
            {
                beforeFont = e.OldValue as Font;
                //valueChange
                if (control.ValueChanged != null)
                    control.ValueChanged(control, new RoutedPropertyChangedEventArgs<Font>((Font)e.OldValue, (Font)e.NewValue));
                //Command
                if (control.Command != null && control.Command.CanExecute(control.CommandParameter))
                    control.Command.Execute(control.CommandParameter);

                control.UpdateFontChanged();

                //property change
                if (e.OldValue is Font oldFont)
                    oldFont.PropertyChanged -= control.OnSelectedFontPropertyChanged;
                if (e.NewValue is Font newFont)
                    newFont.PropertyChanged += control.OnSelectedFontPropertyChanged;

                beforeFont = e.NewValue as Font;
            }
            catch { }
        }
        /// <summary>
        /// Font Subproperty Change Event
        /// 字体子属性变更事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void OnSelectedFontPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                //valueChange
                if (ValueChanged != null)
                    ValueChanged(this, new RoutedPropertyChangedEventArgs<Font>(beforeFont, SelectedFont));

                //Command
                if (Command != null && Command.CanExecute(CommandParameter))
                    Command.Execute(CommandParameter);

                UpdateFontChanged();

                beforeFont = SelectedFont;
            }
            catch { }
        }

        /// <summary>
        /// Whether to hide text decoration options
        /// 是否隐藏文本装饰选项
        /// </summary>
        public bool WithoutDecorations
        {
            get { return (bool)GetValue(WithoutDecorationsProperty); }
            set { SetValue(WithoutDecorationsProperty, value); }
        }
        public static readonly DependencyProperty WithoutDecorationsProperty = DependencyProperty.Register(nameof(WithoutDecorations), typeof(bool), typeof(FontPickerControl), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// Whether to hide the preview
        /// 是否隐藏预览
        /// </summary>
        public bool WithoutPreviewRow
        {
            get { return (bool)GetValue(WithoutPreviewProperty); }
            set { SetValue(WithoutPreviewProperty, value); }
        }
        public static readonly DependencyProperty WithoutPreviewProperty = DependencyProperty.Register(nameof(WithoutPreviewRow), typeof(bool), typeof(FontPickerControl), new FrameworkPropertyMetadata(false,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));



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
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(FontPickerControl), new PropertyMetadata(null));

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
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(FontPickerControl), new PropertyMetadata(null));

    }
}
