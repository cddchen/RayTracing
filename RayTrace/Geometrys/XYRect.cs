using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    public class XYRect : GeometryObject
    {
        private GeoMaterial geoMat;
        double x0, x1, y0, y1, k;
        public XYRect() { }
        public XYRect(double _x0, double _x1, double _y0, double _y1, double _k, GeoMaterial _geoMat)
        {
            x0 = _x0; x1 = _x1;
            y0 = _y0; y1 = _y1;
            k = _k; geoMat = _geoMat;
        }

        public override bool bounding_box(double t0, double t1, out aabb box)
        {
            box = new aabb(new Point3D(x0, y0, k - 1e-4), new Point3D(x1, y1, k + 1e-4));
            return true;
        }

        public override bool Hit(Ray ray, double tMin, double tMax, out ShadeRec shadeRec)
        {
            shadeRec = new ShadeRec(); shadeRec.IsHit = false;
            double t = (k - ray.Origin.Z) / ray.Direction.Z;
            if (t < tMin || t > tMax) return false;
            double x = ray.Origin.X + t * ray.Direction.X;
            double y = ray.Origin.Y + t * ray.Direction.Y;
            if (x < x0 || x > x1 || y < y0 || y > y1) return false;

            shadeRec.IsHit = true;
            shadeRec.U = (x - x0) / (x1 - x0);
            shadeRec.V = (y - y0) / (y1 - y0);
            shadeRec.HitT = t;
            shadeRec.HitObjGloMat = geoMat;
            shadeRec.HitPoint = ray.getPointAtRay(t);
            shadeRec.Normal = new Vector3D(0, 0, 1);
            return true;
        }

        public override bool ShadowHit(Ray ray)
        {
            return false;
        }
    }
    public class XZRect : GeometryObject
    {
        private GeoMaterial geoMat;
        double x0, x1, z0, z1, k;
        public XZRect() { }
        public XZRect(double _x0, double _x1, double _z0, double _z1, double _k, GeoMaterial _geoMat)
        {
            x0 = _x0; x1 = _x1;
            z0 = _z0; z1 = _z1;
            k = _k; geoMat = _geoMat;
        }

        public override bool bounding_box(double t0, double t1, out aabb box)
        {
            box = new aabb(new Point3D(x0, k - 1e-4, z0), new Point3D(x1, k + 1e-4, z1));
            return true;
        }

        public override bool Hit(Ray ray, double tMin, double tMax, out ShadeRec shadeRec)
        {
            shadeRec = new ShadeRec(); shadeRec.IsHit = false;
            double t = (k - ray.Origin.Y) / ray.Direction.Y;
            if (t < tMin || t > tMax) return false;
            double x = ray.Origin.X + t * ray.Direction.X;
            double z = ray.Origin.Z + t * ray.Direction.Z;
            if (x < x0 || x > x1 || z < z0 || z > z1) return false;

            shadeRec.IsHit = true;
            shadeRec.U = (z - z0) / (z1 - z0);
            shadeRec.V = (x - x0) / (x1 - x0);
            shadeRec.HitT = t;
            shadeRec.HitObjGloMat = geoMat;
            shadeRec.HitPoint = ray.getPointAtRay(t);
            shadeRec.Normal = new Vector3D(0, 1, 0);
            return true;
        }

        public override bool ShadowHit(Ray ray)
        {
            return false;
        }
    }
    public class YZRect : GeometryObject
    {
        private GeoMaterial geoMat;
        double y0, y1, z0, z1, k;
        public YZRect() { }
        public YZRect(double _y0, double _y1, double _z0, double _z1, double _k, GeoMaterial _geoMat)
        {
            y0 = _y0; y1 = _y1;
            z0 = _z0; z1 = _z1;
            k = _k; geoMat = _geoMat;
        }

        public override bool bounding_box(double t0, double t1, out aabb box)
        {
            box = new aabb(new Point3D(k - 1e-4, y0, z0), new Point3D(k + 1e-4, y1, z1));
            return true;
        }

        public override bool Hit(Ray ray, double tMin, double tMax, out ShadeRec shadeRec)
        {
            shadeRec = new ShadeRec(); shadeRec.IsHit = false;
            double t = (k - ray.Origin.X) / ray.Direction.X;
            if (t < tMin || t > tMax) return false;
            double y = ray.Origin.Y + t * ray.Direction.Y;
            double z = ray.Origin.Z + t * ray.Direction.Z;
            if (y < y0 || y > y1 || z < z0 || z > z1) return false;

            shadeRec.IsHit = true;
            shadeRec.U = (y - y0) / (y1 - y0);
            shadeRec.V = (z - z0) / (z1 - z0);
            shadeRec.HitT = t;
            shadeRec.HitObjGloMat = geoMat;
            shadeRec.HitPoint = ray.getPointAtRay(t);
            shadeRec.Normal = new Vector3D(1, 0, 0);
            return true;
        }

        public override bool ShadowHit(Ray ray)
        {
            return false;
        }
    }
    public class FilpNormals : GeometryObject
    {
        private GeometryObject obj;
        public FilpNormals(GeometryObject _obj)
        {
            obj = _obj;
        }

        public override bool bounding_box(double t0, double t1, out aabb box)
        {
            bool rhs = obj.bounding_box(t0, t1, out box);
            return rhs;
        }

        public override bool Hit(Ray ray, double tMin, double tMax, out ShadeRec shadeRec)
        {
            if (obj.Hit(ray, tMin, tMax, out shadeRec))
            {
                shadeRec.Normal = -1 * shadeRec.Normal;
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
