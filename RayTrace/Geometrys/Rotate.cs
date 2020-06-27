using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    //绕Y轴旋转
    public class RotateY : GeometryObject
    {
        GeometryObject ptr;
        double sinTheta, cosTheta;
        bool hasBox;
        aabb bbox;
        public RotateY(GeometryObject p, double angle)
        {
            ptr = p;
            double radians = (Math.PI / 180.0) * angle;
            sinTheta = Math.Sin(radians);
            cosTheta = Math.Cos(radians);
            hasBox = ptr.bounding_box(0, 1, out bbox);
            Point3D min = new Point3D(1e15, 1e15, 1e15);
            Point3D max = new Point3D(-1e15, -1e15, -1e15);
            for (int i = 0; i < 2; ++i)
            {
                for (int j = 0; j < 2; ++j)
                {
                    for (int k = 0; k < 2; ++k)
                    {
                        double x = i * bbox.Max.X + (1 - i) * bbox.Min.X;
                        double y = j * bbox.Max.Y + (1 - j) * bbox.Min.Y;
                        double z = k * bbox.Max.Z + (1 - k) * bbox.Min.Z;
                        double newX = cosTheta * x + sinTheta * z;
                        double newZ = -sinTheta * x + cosTheta * z;
                        Point3D tster = new Point3D(newX, y, newZ);
                        if (tster.X > max.X) max.X = tster.X;
                        if (tster.X < min.X) min.X = tster.X;
                        if (tster.Y > max.Y) max.Y = tster.Y;
                        if (tster.Y < min.Y) min.Y = tster.Y;
                        if (tster.Z > max.Z) max.Z = tster.Z;
                        if (tster.Z < min.Z) min.Z = tster.Z;
                    }
                }
            }
            bbox = new aabb(min, max);
        }
        public override bool bounding_box(double t0, double t1, out aabb box)
        {
            box = new aabb(bbox.Min, bbox.Max); return hasBox;
        }

        public override bool Hit(Ray ray, double tMin, double tMax, out ShadeRec shadeRec)
        {
            Point3D Origin = ray.Origin;
            Vector3D Direction = ray.Direction;
            Origin.X = cosTheta * ray.Origin.X - sinTheta * ray.Origin.Z;
            Origin.Z = sinTheta * ray.Origin.X + cosTheta * ray.Origin.Z;
            Direction.X = cosTheta * ray.Direction.X - sinTheta * ray.Direction.Z;
            Direction.Z = sinTheta * ray.Direction.X + cosTheta * ray.Direction.Z;
            Ray rotatedRay = new Ray(Origin, Direction);
            if (ptr.Hit(rotatedRay, tMin, tMax, out shadeRec))
            {
                Point3D p = shadeRec.HitPoint;
                Vector3D normal = shadeRec.Normal;
                p.X = cosTheta * shadeRec.HitPoint.X + sinTheta * shadeRec.HitPoint.Z;
                p.Z = -sinTheta * shadeRec.HitPoint.X + cosTheta * shadeRec.HitPoint.Z;
                normal.X = cosTheta * shadeRec.Normal.X + sinTheta * shadeRec.Normal.Z;
                normal.Z = -sinTheta * shadeRec.Normal.X + cosTheta * shadeRec.Normal.Z;
                shadeRec.HitPoint = p;
                shadeRec.Normal = normal;
                return true;
            }
            else return false;
        }

        public override bool ShadowHit(Ray ray)
        {
            return false;
        }
    }
    public class RotateX : GeometryObject
    {
        GeometryObject ptr;
        double sinTheta, cosTheta;
        bool hasBox;
        aabb bbox;
        public RotateX(GeometryObject p, double angle)
        {
            ptr = p;
            double radians = (Math.PI / 180.0) * angle;
            sinTheta = Math.Sin(radians);
            cosTheta = Math.Cos(radians);
            hasBox = ptr.bounding_box(0, 1, out bbox);
            Point3D min = new Point3D(1e9, 1e9, 1e9);
            Point3D max = new Point3D(-1e9, -1e9, -1e9);
            for (int i = 0; i < 2; ++i)
            {
                for (int j = 0; j < 2; ++j)
                {
                    for (int k = 0; k < 2; ++k)
                    {
                        double x = i * bbox.Max.X + (1 - i) * bbox.Min.X;
                        double y = j * bbox.Max.Y + (1 - j) * bbox.Min.Y;
                        double z = k * bbox.Max.Z + (1 - k) * bbox.Min.Z;
                        double newY = cosTheta * y + sinTheta * z;
                        double newZ = -sinTheta * y + cosTheta * z;
                        Point3D tster = new Point3D(x, newY, newZ);
                        if (tster.X > max.X) max.X = tster.X;
                        if (tster.X < min.X) min.X = tster.X;
                        if (tster.Y > max.Y) max.Y = tster.Y;
                        if (tster.Y < min.Y) min.Y = tster.Y;
                        if (tster.Z > max.Z) max.Z = tster.Z;
                        if (tster.Z < min.Z) min.Z = tster.Z;
                    }
                }
            }
            bbox = new aabb(min, max);
        }
        public override bool bounding_box(double t0, double t1, out aabb box)
        {
            box = bbox; return hasBox;
        }

        public override bool Hit(Ray ray, double tMin, double tMax, out ShadeRec shadeRec)
        {
            Point3D Origin = ray.Origin;
            Vector3D Direction = ray.Direction;
            Origin.Y = cosTheta * ray.Origin.Y - sinTheta * ray.Origin.Z;
            Origin.Z = sinTheta * ray.Origin.Y + cosTheta * ray.Origin.Z;
            Direction.Y = cosTheta * ray.Direction.Y - sinTheta * ray.Direction.Z;
            Direction.Z = sinTheta * ray.Direction.Y + cosTheta * ray.Direction.Z;
            Ray ratoteRay = new Ray(Origin, Direction);
            if (ptr.Hit(ratoteRay, tMin, tMax, out shadeRec))
            {
                Point3D p = shadeRec.HitPoint;
                Vector3D normal = shadeRec.Normal;
                p.Y = cosTheta * shadeRec.HitPoint.Y + sinTheta * shadeRec.HitPoint.Z;
                p.Z = -sinTheta * shadeRec.HitPoint.Y + cosTheta * shadeRec.HitPoint.Z;
                normal.Y = cosTheta * shadeRec.Normal.Y + sinTheta * shadeRec.Normal.Z;
                normal.Z = -sinTheta * shadeRec.Normal.Y + cosTheta * shadeRec.Normal.Z;
                shadeRec.HitPoint = p;
                shadeRec.Normal = normal;
                return true;
            }
            else return false;
        }

        public override bool ShadowHit(Ray ray)
        {
            return false;
        }
    }
    public class RotateZ : GeometryObject
    {
        GeometryObject ptr;
        double sinTheta, cosTheta;
        bool hasBox;
        aabb bbox;
        public RotateZ(GeometryObject p, double angle)
        {
            ptr = p;
            double radians = (Math.PI / 180.0) * angle;
            sinTheta = Math.Sin(radians);
            cosTheta = Math.Cos(radians);
            hasBox = ptr.bounding_box(0, 1, out bbox);
            Point3D min = new Point3D(1e30, 1e30, 1e30);
            Point3D max = new Point3D(-1e30, -1e30, -1e30);
            for (int i = 0; i < 2; ++i)
            {
                for (int j = 0; j < 2; ++j)
                {
                    for (int k = 0; k < 2; ++k)
                    {
                        double x = i * bbox.Max.X + (1 - i) * bbox.Min.X;
                        double y = j * bbox.Max.Y + (1 - j) * bbox.Min.Y;
                        double z = k * bbox.Max.Z + (1 - k) * bbox.Min.Z;
                        double newX = cosTheta * x + sinTheta * y;
                        double newY = -sinTheta * x + cosTheta * y;
                        Point3D tster = new Point3D(newX, newY, z);
                        if (tster.X > max.X) max.X = tster.X;
                        if (tster.X < min.X) min.X = tster.X;
                        if (tster.Y > max.Y) max.Y = tster.Y;
                        if (tster.Y < min.Y) min.Y = tster.Y;
                        if (tster.Z > max.Z) max.Z = tster.Z;
                        if (tster.Z < min.Z) min.Z = tster.Z;
                    }
                }
            }
            bbox = new aabb(min, max);
        }
        public override bool bounding_box(double t0, double t1, out aabb box)
        {
            box = new aabb(bbox.Min, bbox.Max); return hasBox;
        }

        public override bool Hit(Ray ray, double tMin, double tMax, out ShadeRec shadeRec)
        {
            Point3D Origin = ray.Origin;
            Vector3D Direction = ray.Direction;
            Origin.X = cosTheta * ray.Origin.X - sinTheta * ray.Origin.Y;
            Origin.Y = sinTheta * ray.Origin.X + cosTheta * ray.Origin.Y;
            Direction.X = cosTheta * ray.Direction.X - sinTheta * ray.Direction.Y;
            Direction.Y = sinTheta * ray.Direction.X + cosTheta * ray.Direction.Y;
            Ray ratoteRay = new Ray(Origin, Direction);
            if (ptr.Hit(ratoteRay, tMin, tMax, out shadeRec))
            {
                Point3D p = shadeRec.HitPoint;
                Vector3D normal = shadeRec.Normal;
                p.X = cosTheta * shadeRec.HitPoint.X + sinTheta * shadeRec.HitPoint.Y;
                p.Y = -sinTheta * shadeRec.HitPoint.X + cosTheta * shadeRec.HitPoint.Y;
                normal.X = cosTheta * shadeRec.Normal.X + sinTheta * shadeRec.Normal.Y;
                normal.Y = -sinTheta * shadeRec.Normal.X + cosTheta * shadeRec.Normal.Y;
                shadeRec.HitPoint = p;
                shadeRec.Normal = normal;
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
