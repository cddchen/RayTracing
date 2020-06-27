using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    class Light
    {
        Point3D _position;
        SColor _lightColor;

        public Point3D Position { get => _position; set => _position = value; }
        internal SColor LightColor { get => _lightColor; set => _lightColor = value; }

        public Light(Point3D position, SColor lightColor)
        {
            _position = position;
            _lightColor = lightColor;
        }
    }
}
