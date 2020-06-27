using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    public class ShadeRec
    {
        bool _isHit;
        double _HitT;
        double _u, _v;
        Point3D _hitPoint;
        Vector3D _normal;
        Material _hitObjMat;
        GeoMaterial _hitObjGloMat;

        public bool IsHit { get => _isHit; set => _isHit = value; }
        public Point3D HitPoint { get => _hitPoint; set => _hitPoint = value; }
        public Vector3D Normal { get => _normal; set => _normal = value; }
        public double HitT { get => _HitT; set => _HitT = value; }
        public GeoMaterial HitObjGloMat { get => _hitObjGloMat; set => _hitObjGloMat = value; }
        public double U { get => _u; set => _u = value; }
        public double V { get => _v; set => _v = value; }
        internal Material HitObjMat { get => _hitObjMat; set => _hitObjMat = value; }
    }
}
