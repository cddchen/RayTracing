using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    public class Camera
    {
        Vector3D up;
        Point3D _eye, _start;
        Vector3D _horizon, _vertical, u, v, w;
        double _lens_radius;
        //lookfrom相机的位置，lookat目标物体的位置，vup相机的倾斜方向，vfov相机的张角theta，aspect图片宽高比，aperture相机光圈的值，focus_dist成像位置
        public Camera(Vector3D lookfrom, Vector3D lookat, Vector3D vup, double vfov, double aspect, double aperture, double focus_dist)
        {
            _lens_radius = aperture / 2;

            double theta = vfov * Math.PI / 180.0;
            double half_height = Math.Tan(theta / 2);
            double half_width = aspect * half_height;
            w = (lookfrom - lookat).getNormalize(); //与视野反向的基向量
            u = (vup ^ w).getNormalize(); //平行于x轴
            v = w ^ u;
            /*
            Start = new Point3D(-half_width, half_height, -1);
            Horizon = new Vector3D(2 * half_width, 0, 0);
            Vertical = new Vector3D(0, 2 * half_height, 0);
            */
            Eye = new Point3D(lookfrom.X, lookfrom.Y, lookfrom.Z);
            Start = Eye - half_width * focus_dist * u + half_height * focus_dist * v - focus_dist * w;
            Horizon = 2 * half_width * focus_dist * u;
            Vertical = 2 * half_height * focus_dist * v;
        }
        public Point3D Eye { get => _eye; set => _eye = value; }
        public Point3D Start { get => _start; set => _start = value; }
        public Vector3D Horizon { get => _horizon; set => _horizon = value; }
        public Vector3D Vertical { get => _vertical; set => _vertical = value; }

        public Ray getRay(double i, double j)
        {
            Vector3D rd = _lens_radius * Vector3D.RandomInUnitSphere();
            Vector3D offset = u * rd.X + v * rd.Y;
            return new Ray(Eye + offset, Start + i * Horizon - j * Vertical - (Eye + offset));
            /*
            Point3D p = Start + i * Horizon - j * Vertical;
            Vector3D dir = p - Eye; dir.Normalize();
            return new Ray(Eye, dir);
            */
        }
        public void Left(double degrees)
        {
            Matrix r = Matrix.rotate(-degrees, up);
            Matrix eye = new Matrix(1, 3, new double[1, 3] { { Eye.X, Eye.Y, Eye.Z } });
            eye = eye * r;
            Eye.X = eye.Mat[0, 0]; Eye.Y = eye.Mat[0, 1]; Eye.Z = eye.Mat[0, 2];
        }
        public void Up(double degrees)
        {
            Vector3D eye = Eye - new Point3D(0, 0, 0);
            Matrix r = Matrix.rotate(degrees, eye ^ up);
            Matrix _rT = Matrix.transpose(Matrix.inverse(r));
            Matrix upMat = new Matrix(1, 3, new double[1, 3] { { up.X, up.Y, up.Z } });
            Matrix res = upMat * _rT;
            up.X = res.Mat[0, 0]; up.Y = res.Mat[0, 1]; up.Z = res.Mat[0, 2];
            Matrix eyeMat = new Matrix(1, 3, new double[1, 3] { { Eye.X, Eye.Y, Eye.Z } });
            eyeMat = eyeMat * r;
            Eye.X = eyeMat.Mat[0, 0]; Eye.Y = eyeMat.Mat[0, 1]; Eye.Z = eyeMat.Mat[0, 2];
        }
    }
}
