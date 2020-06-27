using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    public abstract class GeoMaterial
    {
        //private SColor attenuation;
        private Texture _texture;

        //public SColor Attenuation { get => attenuation; set => attenuation = value; }
        public Texture texture { get => _texture; set => _texture = value; }
        public GeoMaterial(Texture texture)
        {
            _texture = texture;
        }
        public abstract bool scatter(Ray rayIn, ShadeRec sr, out SColor Attenuation, out Ray rayScatter);
        public abstract SColor emit(double u, double v, Point3D p);
    }
}
