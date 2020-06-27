using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    public abstract class GeometryObject
    {
        private Material _mat;
        private GeoMaterial _geoMaterial;

        public GeoMaterial GeoMaterial { get => _geoMaterial; set => _geoMaterial = value; }
        internal Material Mat { get => _mat; set => _mat = value; }
        public abstract bool Hit(Ray ray, double tMin, double tMax, out ShadeRec shadeRec);
        public abstract bool bounding_box(double t0, double t1, out aabb box);
        public abstract bool ShadowHit(Ray ray);
    }
}
