using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    public class CheckerTexture : Texture
    {
        private Texture odd, even;
        public CheckerTexture() { }
        public CheckerTexture(Texture t0, Texture t1)
        {
            even = t0;
            odd = t1;
        }
        public override SColor getColor(double u, double v, Point3D p)
        {
            //Console.WriteLine(p.X + ", " + p.Y + ", " + p.Z);
            double sines = Math.Sin(p.X) * Math.Sin(p.Y) * Math.Sin(p.Z);
            if (sines < 0) return odd.getColor(u, v, p);
            else return even.getColor(u, v, p);
        }
    }
}
