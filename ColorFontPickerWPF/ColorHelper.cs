using ColorMine.ColorSpaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace ColorFontPickerWPF
{
    class RGB
    {
        Rgb rgb = new Rgb();
        public int R { get { return (int)Math.Round(rgb.R); } set { rgb.R = value; } }
        public int G { get { return (int)Math.Round(rgb.G); } set { rgb.G = value; } }
        public int B { get { return (int)Math.Round(rgb.B); } set { rgb.B = value; } }

        public RGB(int r, int g, int b)
        {
            rgb = new Rgb { R = r, G = g, B = b };
        }
        public RGB(Rgb Rgb)
        {
            rgb = Rgb;
        }
        public RGB(Color color)
        {
            rgb = new Rgb { R = color.R, G = color.G, B = color.B };
        }
        public Rgb ToRgb()
        {
            return rgb;
        }
        public Color ToColor()
        {
            return Color.FromRgb((byte)Math.Round(rgb.R), (byte)Math.Round(rgb.G), (byte)Math.Round(rgb.B));
        }
    }

    class HSL
    {
        Hsl hsl = new Hsl();
        public int H { get { return (int)Math.Round(hsl.H); } set { hsl.H = value; } }
        public int S { get { return (int)Math.Round(hsl.S * 100); } set { hsl.S = (double)value / 100; } }
        public int L { get { return (int)Math.Round(hsl.L * 100); } set { hsl.L = (double)value / 100; } }

        public HSL(int h, int s, int l)
        {
            hsl = new Hsl { H = h, S = ((double)s) / 100, L = ((double)l) / 100 };
        }
        public HSL(Hsl Hsl)
        {
            hsl = Hsl;
        }
        public Hsl ToHsl()
        {
            return hsl;
        }
        public Rgb ToRgb()
        {
            return hsl.To<Rgb>();
        }
    }

    static class Extension
    {
        public static Color ToColor(this Rgb rgb)
        {
            return Color.FromRgb((byte)Math.Round(rgb.R), (byte)Math.Round(rgb.G), (byte)Math.Round(rgb.B));
        }

        public static RGB ToRGB(this Rgb rgb)
        {
            return new RGB(rgb);
        }
        public static HSL ToHSL(this Hsl hsl)
        {
            return new HSL(hsl);
        }
    }

}
