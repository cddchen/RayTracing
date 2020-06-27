using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    public class ConstantMedium : GeometryObject
    {
        GeometryObject boundary;
        double density;
        GeoMaterial phase_function;
        public ConstantMedium(GeometryObject obj, double _density, Texture texture)
        {
            boundary = obj;
            density = _density;
            phase_function = new Isotropic(texture);
        }
        public override bool bounding_box(double t0, double t1, out aabb box)
        {
            return boundary.bounding_box(t0, t1, out box);
        }

        public override bool Hit(Ray ray, double tMin, double tMax, out ShadeRec shadeRec)
        {
            ShadeRec rec1, rec2; shadeRec = new ShadeRec();
            if (boundary.Hit(ray, -1e15, 1e15, out rec1))
            {
                if (boundary.Hit(ray, rec1.HitT + 1e-4, 1e15, out rec2))
                {
                    if (rec1.HitT < tMin) rec1.HitT = tMin;
                    if (rec2.HitT > tMax) rec2.HitT = tMax;
                    if (rec1.HitT >= rec2.HitT) return false;
                    if (rec1.HitT < 0) rec1.HitT = 0;
                    double distanceInsideBoundary = (rec2.HitT - rec1.HitT) * ray.Direction.Magnitude();
                    double hitDistance = -(1.0 / density) * Math.Log(Form2.random());
                    if (hitDistance < distanceInsideBoundary)
                    {
                        shadeRec.IsHit = true;
                        shadeRec.HitT = rec1.HitT + hitDistance / ray.Direction.Magnitude();
                        shadeRec.HitPoint = ray.getPointAtRay(shadeRec.HitT);
                        shadeRec.Normal = new Vector3D(Form2.random(), Form2.random(), Form2.random());
                        shadeRec.HitObjGloMat = phase_function;
                        return true;
                    }
                }
            }
            return false;
        }

        public override bool ShadowHit(Ray ray)
        {
            throw new NotImplementedException();
        }
    }
}
