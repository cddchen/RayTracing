using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    class Dielectric : GeoMaterial
    {
        private double _RI;
        public Dielectric(double RI) : base(new ConstantTexture(new SColor(1, 1, 1))) {
            _RI = RI;
        }
        private bool refract(Vector3D v, Vector3D n, double eta, out Vector3D refracted)
        {
            v.Normalize(); n.Normalize();
            double cos1 = v * n;
            double cos2 = 1.0 - eta * eta * (1 - cos1 * cos1);
            if (cos2 > 0)
            {
                refracted = eta * (v - cos1 * n) - Math.Sqrt(cos2) * n;
                return true;
            }
            refracted = new Vector3D();
            return false;
        }
        private double schlick(double cosine, double ref_idx)
        {
            double r0 = (1 - ref_idx) / (1 + ref_idx);
            r0 = r0 * r0;
            return r0 + (1 - r0) * Math.Pow((1 - cosine), 5);
        }
        public override bool scatter(Ray rayIn, ShadeRec sr, out SColor Attenuation, out Ray rayScatter)
        {
            Attenuation = new SColor(1, 1, 1);
            Vector3D outward_normal;
            Vector3D refracted;
            Vector3D reflected = Ray.getReflectDir(rayIn.Direction, sr.Normal);
            double eta, cos, reflect_prob;
            if (rayIn.Direction * sr.Normal > 0)
            {
                outward_normal = -1 * sr.Normal;
                eta = _RI;
                cos = _RI * (rayIn.Direction * sr.Normal) / rayIn.Direction.Magnitude();
            }
            else
            {
                outward_normal = sr.Normal;
                eta = 1.0 / _RI;
                cos = -1.0 * (rayIn.Direction * sr.Normal) / rayIn.Direction.Magnitude();
            }
            if (refract(rayIn.Direction, outward_normal, eta, out refracted))
            {
                reflect_prob = schlick(cos, eta);
                rayScatter = new Ray(sr.HitPoint, refracted);
            }
            else
            {
                reflect_prob = 1.0;
                rayScatter = new Ray(sr.HitPoint, reflected);
            }
            if (Form2.random() < reflect_prob)
                rayScatter = new Ray(sr.HitPoint, reflected);
            else
                rayScatter = new Ray(sr.HitPoint, refracted);
            return true;
        }

        public override SColor emit(double u, double v, Point3D p)
        {
            return new SColor(0, 0, 0);
        }
    }
}
