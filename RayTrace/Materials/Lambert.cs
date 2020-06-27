using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    class Lambert : GeoMaterial
    {
        public Lambert(Texture texture) : base(texture) { }

        public override SColor emit(double u, double v, Point3D p)
        {
            return new SColor(0, 0, 0);
        }

        public override bool scatter(Ray rayIn, ShadeRec sr, out SColor Attenuation, out Ray rayScatter)
        {
            Point3D target = (sr.HitPoint + sr.Normal) + Vector3D.RandomInUnitSphere();
            rayScatter = new Ray(sr.HitPoint, target - sr.HitPoint);
            Attenuation = texture.getColor(sr.U, sr.V, sr.HitPoint);
            return true;
        }
    }
}
