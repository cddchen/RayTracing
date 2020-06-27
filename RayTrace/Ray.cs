using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    public class Ray
    {
        private Point3D _origin;
        private Vector3D _direction;
        public Point3D Origin
        {
            get => _origin;
            set => _origin = value;
        }
        public Vector3D Direction
        {
            get => _direction;
            set => _direction = value;
        }
        public Ray() { }
        public Ray(Point3D origin, Vector3D direction)
        {
            this.Origin = origin;
            this.Direction = direction;
        }

        public Point3D getPointAtRay(double t)
        {
            return this.Origin + t * this.Direction;
        }
        public static Vector3D getReflectDir(Vector3D v, Vector3D n)
        {
            v.Normalize();
            n.Normalize();
            return v - 2 * (v * n) * n;
        }
    }
}
