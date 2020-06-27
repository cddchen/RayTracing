using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    class Material
    {
        private double _ka, _ks, _kd, _ns;
        private SColor _matColor;
        private Texture _txture;
        private bool _isTexture;
        //参数：Lambert，Pong，高光指数，颜色
        public Material(double kd, double ks, double ka, double ns, SColor matColor)
        {
            _kd = kd;
            _ks = ks;
            _ka = ka;
            _ns = ns;
            _matColor = matColor;
            _isTexture = false;
        }
        public Material(Texture texture)
        {
            _txture = texture;
            _isTexture = true;
        }

        public double Ka { get => _ka; set => _ka = value; }
        public double Ks { get => _ks; set => _ks = value; }
        public double Kd { get => _kd; set => _kd = value; }
        public double Ns { get => _ns; set => _ns = value; }
        public Texture Txture { get => _txture; set => _txture = value; }
        public bool IsTexture { get => _isTexture; set => _isTexture = value; }
        internal SColor MatColor { get => _matColor; set => _matColor = value; }
    }
}
