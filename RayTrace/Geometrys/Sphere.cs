using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    public class Sphere : GeometryObject
    {
        private Point3D _center;
        private double _radius;
        public Sphere()
        {
            this.Center = new Point3D(0, 0, 0);
            this.Radius = 0;
        }
        public Sphere(Point3D center, double radius)
        {
            this.Center = center;
            this.Radius = radius;
        }
        public Sphere(Point3D center, double radius, GeoMaterial geoMat)
        {
            this.Center = center;
            this.Radius = radius;
            this.GeoMaterial = geoMat;
        }

        public double Radius { get => _radius; set => _radius = value; }
        public Point3D Center { get => _center; set => _center = value; }

        public Vector3D GetNormalVector(Point3D p)
        {
            Vector3D x = p - this.Center;
            x.Normalize();
            return x;
        }
        private void getSphereUV(Vector3D p, out double u, out double v)
        {
            double phi = Math.Atan2(p.Z, p.X);
            double theta = Math.Asin(p.Y);
            u = 1.0 - (phi + Math.PI) / (2.0 * Math.PI);
            v = (theta + Math.PI / 2.0) / Math.PI;
        }
        public override bool Hit(Ray ray, double tMin, double tMax, out ShadeRec shadeRec)
        {
            Vector3D OC = ray.Origin - Center;
            shadeRec = new ShadeRec();
            double a = ray.Direction * ray.Direction;
            double b = ray.Direction * OC;
            double c = OC * OC - Radius * Radius;
            double delta = b * b - a * c;
            if (delta > 0)
            {
                double t = (-b - Math.Sqrt(delta)) / a;
                if (tMin < t && t < tMax)
                {
                    shadeRec.IsHit = true;
                    shadeRec.HitT = t;
                    shadeRec.HitPoint = ray.getPointAtRay(t);
                    shadeRec.HitObjMat = this.Mat;
                    shadeRec.HitObjGloMat = GeoMaterial;
                    shadeRec.Normal = this.GetNormalVector(shadeRec.HitPoint);
                    double u, v;
                    getSphereUV(shadeRec.Normal, out u, out v);
                    shadeRec.U = u; shadeRec.V = v;
                    return true;
                }
                t = (-b + Math.Sqrt(delta)) / a;
                if (tMin < t && t < tMax)
                {
                    shadeRec.IsHit = true;
                    shadeRec.HitT = t;
                    shadeRec.HitPoint = ray.getPointAtRay(t);
                    shadeRec.HitObjMat = this.Mat;
                    shadeRec.HitObjGloMat = GeoMaterial;
                    shadeRec.Normal = this.GetNormalVector(shadeRec.HitPoint);
                    double u, v;
                    getSphereUV(shadeRec.Normal, out u, out v);
                    shadeRec.U = u; shadeRec.V = v;
                    return true;
                }
            }
            shadeRec.IsHit = false;
            shadeRec.HitPoint = new Point3D();
            shadeRec.Normal = new Vector3D();
            return false;
        }
        public override bool bounding_box(double t0, double t1, out aabb box)
        {
            box = new aabb(Center - new Vector3D(Radius, Radius, Radius), Center + new Vector3D (Radius, Radius, Radius));
            return true;
        }
        public override bool ShadowHit(Ray ray)
        {
            Vector3D OC = ray.Origin - Center;
            double a = ray.Direction * ray.Direction;
            double b = 2.0 * ray.Direction * OC;
            double c = OC * OC - Radius * Radius;
            double delta = b * b - 4 * a * c;
            if (delta > 0)
            {
                double t = (-b - Math.Sqrt(delta)) / (2 * a);
                if (t > 1e-5 && t < 1.0)
                    return true;
                
            }
            return false;
        }
    }
}
