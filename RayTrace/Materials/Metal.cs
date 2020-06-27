using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    class Metal : GeoMaterial
    {
        private double fuzz;
        public Metal(Texture texture, double f = 0) : base(texture) {
            if (f < 1 && f >= 0) fuzz = f;
            else fuzz = 1;
        }

        public override SColor emit(double u, double v, Point3D p)
        {
            return new SColor(0, 0, 0);
        }

        public override bool scatter(Ray rayIn, ShadeRec sr, out SColor Attenuation, out Ray rayScatter)
        {
            Vector3D reflectDir = Ray.getReflectDir(rayIn.Direction, sr.Normal);
            reflectDir.Normalize();
            rayScatter = new Ray(sr.HitPoint, reflectDir + fuzz * Vector3D.RandomInUnitSphere());
            Attenuation = texture.getColor(sr.U, sr.V, sr.HitPoint);
            return (rayScatter.Direction * sr.Normal) > 0;
        }
    }
}
