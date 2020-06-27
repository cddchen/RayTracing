using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    public class NoiseTexture : Texture
    {
        private double _scale;
        private Perlin perlin;
        public NoiseTexture(double scale = 5) {
            perlin = new Perlin();
            _scale = scale;
        }
        public override SColor getColor(double u, double v, Point3D p)
        {
            return new SColor(1, 1, 1) * 0.5 * (1 + Math.Sin(_scale * p.Z + 10 * perlin.Turb(p)));
        }
    }
}
