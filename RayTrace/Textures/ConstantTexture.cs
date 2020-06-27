using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    public class ConstantTexture : Texture
    {
        private SColor _color;
        public ConstantTexture() { }
        public ConstantTexture(SColor color)
        {
            _color = color;
        }

        public override SColor getColor(double u, double v, Point3D p)
        {
            return _color;
        }
    }
}
