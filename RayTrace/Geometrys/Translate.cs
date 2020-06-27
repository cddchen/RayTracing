using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    //平移
    public class Translate : GeometryObject
    {
        GeometryObject ptr;
        Vector3D offset;

        public Translate(GeometryObject p, Vector3D displacement)
        {
            ptr = p;
            offset = displacement;
        }
        public override bool bounding_box(double t0, double t1, out aabb box)
        {
            if (ptr.bounding_box(t0, t1, out box))
            {
                box = new aabb(box.Min + offset, box.Max + offset);
                return true;
            }
            else return false;
        }

        public override bool Hit(Ray ray, double tMin, double tMax, out ShadeRec shadeRec)
        {
            Ray movedRay = new Ray(ray.Origin - offset, ray.Direction);
            if (ptr.Hit(movedRay, tMin, tMax, out shadeRec))
            {
                shadeRec.HitPoint = shadeRec.HitPoint + offset;
                return true;
            }
            else return false;
        }

        public override bool ShadowHit(Ray ray)
        {
            return false;
        }
    }
}
