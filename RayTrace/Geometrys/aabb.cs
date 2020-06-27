using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    public class aabb
    {
        Point3D _min, _max;
        public aabb(Point3D min, Point3D max)
        {
            Min = min; Max = max;
        }

        public Point3D Min { get => _min; set => _min = value; }
        public Point3D Max { get => _max; set => _max = value; }
        public bool hit(Ray ray)
        {
            double invD = 1.0 / ray.Direction.X;
            double t0 = (Min.X - ray.Origin.X) * invD;
            double t1 = (Max.X - ray.Origin.X) * invD;
            if (invD < 0)
            {
                double tmp = t0;
                t0 = t1;
                t1 = tmp;
            }
            if (t0 < 1e-5) t0 = 1e-5;
            if (t1 <= t0) return false;

            invD = 1.0 / ray.Direction.Y;
            t0 = (Min.Y - ray.Origin.Y) * invD;
            t1 = (Max.Y - ray.Origin.Y) * invD;
            if (invD < 0)
            {
                double tmp = t0;
                t0 = t1;
                t1 = tmp;
            }
            if (t0 < 1e-5) t0 = 1e-5;
            if (t1 <= t0) return false;

            invD = 1.0 / ray.Direction.Z;
            t0 = (Min.Z - ray.Origin.Z) * invD;
            t1 = (Max.Z - ray.Origin.Z) * invD;
            if (invD < 0)
            {
                double tmp = t0;
                t0 = t1;
                t1 = tmp;
            }
            if (t0 < 1e-5) t0 = 1e-5;
            if (t1 <= t0) return false;

            return true;
        }

        public static aabb surrounding_box(aabb box0, aabb box1)
        {
            Point3D min = new Point3D(
                Math.Min(box0.Min.X, box1.Min.X),
                Math.Min(box0.Min.Y, box1.Min.Y),
                Math.Min(box0.Min.Y, box1.Min.Y));

            Point3D max = new Point3D(
                Math.Max(box0.Max.X, box1.Max.X),
                Math.Max(box0.Max.Y, box1.Max.Y),
                Math.Max(box0.Max.Z, box1.Max.Z));

            return new aabb(min, max);
        }
    }
}
