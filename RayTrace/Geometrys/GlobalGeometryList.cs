using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    public class GlobalGeometryList : GeometryObject
    {
        List<GeometryObject> list;
        public GlobalGeometryList(List<GeometryObject> _list)
        {
            list = _list;
        }
        public override bool bounding_box(double t0, double t1, out aabb box)
        {
            box = new aabb(new Point3D(0, 0, 0), new Point3D(1, 1, 1));
            return true;
        }

        public override bool Hit(Ray ray, double tMin, double tMax, out ShadeRec shadeRec)
        {
            shadeRec = new ShadeRec(); shadeRec.IsHit = false;
            double closestT = tMax;
            foreach (GeometryObject geometry in list)
            {
                ShadeRec tmp;
                if (geometry.Hit(ray, tMin, closestT, out tmp))
                {
                    shadeRec = tmp;
                    closestT = tmp.HitT;
                }
            }
            return shadeRec.IsHit;
        }

        public override bool ShadowHit(Ray ray)
        {
            return false;
        }
    }
}
