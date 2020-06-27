using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    //体数据
    public class Isotropic : GeoMaterial
    {
        public Isotropic(Texture texture) : base(texture) { }
        public override SColor emit(double u, double v, Point3D p)
        {
            return new SColor(0, 0, 0);
        }

        public override bool scatter(Ray rayIn, ShadeRec sr, out SColor Attenuation, out Ray rayScatter)
        {
            rayScatter = new Ray(sr.HitPoint, Vector3D.RandomInUnitSphere());
            Attenuation = texture.getColor(sr.U, sr.V, sr.HitPoint);
            return true;
        }

    }
}
