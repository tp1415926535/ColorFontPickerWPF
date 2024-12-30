//using ColorMine.ColorSpaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace ColorFontPickerWPF
{
    class RGB
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public RGB(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }
        public RGB(Color color)
        {
            R = color.R;
            G = color.G;
            B = color.B;
        }

        public Color ToColor()
        {
            return Color.FromRgb((byte)R, (byte)G, (byte)B);
        }

        public HSL ToHSL()
        {
            var max = Math.Max(R, Math.Max(G, B));
            var min = Math.Min(R, Math.Min(G, B));

            double h, s, l;

            //saturation
            var cnt = (max + min) / 2d;
            if (cnt <= 127d)
            {
                s = max + min == 0 ? 0 : ((double)(max - min) / (double)(max + min));
            }
            else
            {
                s = ((double)(max - min) / (double)(510d - max - min));
            }

            //lightness
            l = (double)((max + min) / 2d) / 255d;

            //hue
            if (Math.Abs(max - min) <= float.Epsilon)
            {
                h = 0d;
                s = 0d;
            }
            else
            {
                double diff = max - min;

                if (Math.Abs(max - R) <= float.Epsilon)
                {
                    h = 60d * (G - B) / diff;
                }
                else if (Math.Abs(max - G) <= float.Epsilon)
                {
                    h = 60d * (B - R) / diff + 120d;
                }
                else
                {
                    h = 60d * (R - G) / diff + 240d;
                }

                if (h < 0d)
                {
                    h += 360d;
                }
            }
            return new HSL((int)Math.Round(h), (int)Math.Round(s * 100), (int)Math.Round(l * 100));
        }
        public HEX ToHEX()
        {
            return new HEX("#" + R.ToString("X2") + G.ToString("X2") + B.ToString("X2"));
        }
    }

    class HSL
    {
        public int H { get; set; }
        public int S { get; set; }
        public int L { get; set; }

        public HSL(int h, int s, int l)
        {
            H = h;
            S = s;
            L = l;
        }
        public RGB ToRGB()
        {
            var rangedH = (double)H / 360.0;
            var s = (double)S / 100.0;
            var l = (double)L / 100.0;
            var r = 0.0;
            var g = 0.0;
            var b = 0.0;

            if (!l.BasicallyEqualTo(0))
            {
                if (s.BasicallyEqualTo(0))
                {
                    r = g = b = l;
                }
                else
                {
                    var temp2 = (l < 0.5) ? l * (1.0 + s) : l + s - (l * s);
                    var temp1 = 2.0 * l - temp2;

                    r = GetColorComponent(temp1, temp2, rangedH + 1.0 / 3.0);
                    g = GetColorComponent(temp1, temp2, rangedH);
                    b = GetColorComponent(temp1, temp2, rangedH - 1.0 / 3.0);
                }
            }
            return new RGB((int)Math.Round(r * 255), (int)Math.Round(g * 255), (int)Math.Round(b * 255));
        }
        private static double GetColorComponent(double temp1, double temp2, double temp3)
        {
            temp3 = MoveIntoRange(temp3);
            if (temp3 < 1.0 / 6.0)
            {
                return temp1 + (temp2 - temp1) * 6.0 * temp3;
            }

            if (temp3 < 0.5)
            {
                return temp2;
            }

            if (temp3 < 2.0 / 3.0)
            {
                return temp1 + ((temp2 - temp1) * ((2.0 / 3.0) - temp3) * 6.0);
            }

            return temp1;
        }
        private static double MoveIntoRange(double temp3)
        {
            if (temp3 < 0.0) return temp3 + 1.0;
            if (temp3 > 1.0) return temp3 - 1.0;
            return temp3;
        }
    }
    internal static class DoubleExtension
    {
        private const double DefaultPrecision = .0001;

        internal static bool BasicallyEqualTo(this double a, double b)
        {
            return a.BasicallyEqualTo(b, DefaultPrecision);
        }

        internal static bool BasicallyEqualTo(this double a, double b, double precision)
        {
            return Math.Abs(a - b) <= precision;
        }
    }

    class HEX
    {
        private string _R;
        public string R
        {
            get { return _R; }
            set
            {
                _R = SetterCheck(value);
            }
        }
        private string _G;
        public string G
        {
            get { return _G; }
            set
            {
                _G = SetterCheck(value);
            }
        }
        private string _B;
        public string B
        {
            get { return _B; }
            set
            {
                _B = SetterCheck(value);
            }
        }

        private string SetterCheck(string s)
        {
            var regex = new Regex(@"^[0-9A-Fa-f]{1,2}$");
            if (!regex.IsMatch(s))
            {
                throw new FormatException();
            }
            return s;
        }

        public string Code
        {
            get { return "#" + R + G + B; }
            set
            {
                SetCode(value);
            }
        }
        public HEX(string code)
        {
            SetCode(code);
        }
        private void SetCode(string value)
        {
            var regex1 = new Regex(@"^#{0,1}([0-9A-Fa-f]{1})([0-9A-Fa-f]{1})([0-9A-Fa-f]{1})$");
            var m = regex1.Match(value);
            if (m.Success)
            {
                this.R = string.Format("{0}{0}", m.Groups[1].Value);
                this.G = string.Format("{0}{0}", m.Groups[2].Value);
                this.B = string.Format("{0}{0}", m.Groups[3].Value);
                return;
            }

            var regex2 = new Regex(@"^#{0,1}([0-9A-Fa-f]{2})([0-9A-Fa-f]{2})([0-9A-Fa-f]{2})$");
            m = regex2.Match(value);
            if (m.Success)
            {
                this.R = m.Groups[1].Value;
                this.G = m.Groups[2].Value;
                this.B = m.Groups[3].Value;
                return;
            }

            throw new FormatException();
        }
        public RGB ToRGB()
        {
            return new RGB((int)Convert.ToInt32(R, 16), (int)Convert.ToInt32(G, 16), (int)Convert.ToInt32(B, 16));
        }
    }

}
