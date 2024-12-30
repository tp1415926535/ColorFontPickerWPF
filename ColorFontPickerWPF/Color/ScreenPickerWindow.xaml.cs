using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ColorFontPickerWPF
{
    /// <summary>
    /// ScreenPickerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ScreenPickerWindow : Window
    {
        public Color pickColor = Colors.Transparent;
        public ScreenPickerWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                Point p = this.PointToScreen(Mouse.GetPosition(this));
                pickColor = GetPosColor(p);
                this.Close();
            }
            else if (e.Key.Equals(Key.Escape))
                this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Point p = this.PointToScreen(Mouse.GetPosition(this));
                pickColor = GetPosColor(p);
                this.Close();
            }
            else if (e.ChangedButton == MouseButton.Right)
                this.Close();
        }


        [DllImport("gdi32")]
        public static extern uint GetPixel(IntPtr hDC, int nXPos, int nYPos);
        [DllImport("user32")]
        public static extern IntPtr GetDC(IntPtr hWnd);
        [DllImport("user32")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        private IntPtr _hdc = IntPtr.Zero;
        Color GetPosColor(Point point)
        {
            _hdc = GetDC(IntPtr.Zero);
            uint color = GetPixel(_hdc, (int)(point.X), (int)(point.Y));
            ReleaseDC(IntPtr.Zero, _hdc);
            var R = (byte)color;
            var G = (byte)(((ushort)(color)) >> 8);
            var B = (byte)(color >> 16);
            return Color.FromRgb(R, G, B);
        }

    }
}
