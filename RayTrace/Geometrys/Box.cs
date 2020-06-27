using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    public class Box : GeometryObject
    {
        Point3D pmin, pmax;
        GeometryObject list_ptr;
        public Box(Point3D _pmin, Point3D _pmax, GeoMaterial ptr)
        {
            pmin = _pmin; pmax = _pmax;
            List<GeometryObject> list = new List<GeometryObject>();
            list.Add(new XYRect(pmin.X, pmax.X, pmin.Y, pmax.Y, pmax.Z, ptr));
            list.Add(new FilpNormals(new XYRect(pmin.X, pmax.X, pmin.Y, pmax.Y, pmin.Z, ptr)));
            list.Add(new XZRect(pmin.X, pmax.X, pmin.Z, pmax.Z, pmax.Y, ptr));
            list.Add(new FilpNormals(new XZRect(pmin.X, pmax.X, pmin.Z, pmax.Z, pmin.Y, ptr)));
            list.Add(new YZRect(pmin.Y, pmax.Y, pmin.Z, pmax.Z, pmax.X, ptr));
            list.Add(new FilpNormals(new YZRect(pmin.Y, pmax.Y, pmin.Z, pmax.Z, pmin.X, ptr)));
            list_ptr = new GlobalGeometryList(list);
        }
        public override bool bounding_box(double t0, double t1, out aabb box)
        {
            box = new aabb(pmin, pmax);
            return true;
        }

        public override bool Hit(Ray ray, double tMin, double tMax, out ShadeRec shadeRec)
        {
            return list_ptr.Hit(ray, tMin, tMax, out shadeRec);
        }

        public override bool ShadowHit(Ray ray)
        {
            return false;
        }
    }
}
