namespace RayTrace
{
    public class DiffuseLight : GeoMaterial
    {
        private Texture _emit;
        public DiffuseLight(Texture texture) : base(texture)
        {
            _emit = texture;
        }

        public override SColor emit(double u, double v, Point3D p)
        {
            return _emit.getColor(u, v, p);
        }

        public override bool scatter(Ray rayIn, ShadeRec sr, out SColor Attenuation, out Ray rayScatter)
        {
            Attenuation = null;
            rayScatter = null;
            return false;
        }
    }
}
