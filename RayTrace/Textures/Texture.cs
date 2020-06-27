using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    public abstract class Texture
    {
        public abstract SColor getColor(double u, double v, Point3D p);
    }
}
