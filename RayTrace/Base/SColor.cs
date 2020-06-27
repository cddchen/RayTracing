using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    public class SColor
    {
        private double _r, _g, _b;
        public double R
        {
            get => _r;
            set => _r = value;
        }
        public double G
        {
            get => _g;
            set => _g = value;
        }
        public double B
        {
            get => _b;
            set => _b = value;
        }
        public SColor()
        {
            this.R = this.G = this.B = 0;
        }
        public SColor(double r, double g, double b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }
        double Check(double RGB)
        {
            if (RGB < 0) return 0;
            if (RGB > 1.0) return 1;
            return RGB;
        }
        public Color RGB255()
        {
            return Color.FromArgb((int)(Check(R) * 255), (int)(Check(G) * 255), (int)(Check(B) * 255));
        }
        public static SColor operator *(SColor color, double d)
        {
            return new SColor(color.R * d, color.G * d, color.B * d);
        }
        public static SColor operator *(SColor color1, SColor color2)
        {
            return new SColor(color1.R * color2.R, color1.G * color2.G, color1.B * color2.B);
        }
        public static SColor operator +(SColor color1, SColor color2)
        {
            return new SColor(color1.R + color2.R, color1.G + color2.G, color1.B + color2.B);
        }
    }
}
