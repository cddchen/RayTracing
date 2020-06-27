using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    public class ImageTexture : Texture
    {
        Bitmap bmp;
        int hRes, vRes;

        public Bitmap Bmp { get => bmp; set => bmp = value; }
        public int HRes { get => hRes; set => hRes = value; }
        public int VRes { get => vRes; set => vRes = value; }

        public ImageTexture()
        {
            Bmp = new Bitmap(100, 100);
            HRes = VRes = 100;
        }
        public ImageTexture(Bitmap bitmap)
        {
            bmp = bitmap;
            hRes = bitmap.Width; vRes = bitmap.Height;
        }
        /*
        public void getTextCoordinate(Vector3D pHit, out int row, out int column)
        {
            double delta = Math.Acos(pHit.Y);
            double phi = Math.Atan2(pHit.X, pHit.Z);

            if (phi < 0)
            {
                phi += 2.0 * Math.PI;
            }

            double u = phi / (2.0 * Math.PI);
            double v = 1.0 - delta / Math.PI;

            column = (int)((HRes - 1) * u);
            row = (int)((VRes - 1) * v);
        }
        public Color getColor(Vector3D pHit)
        {
            int row, column;
            getTextCoordinate(pHit, out row, out column);
            return Bmp.GetPixel(column, vRes - row - 1);
        }
        */
        public override SColor getColor(double u, double v, Point3D p)
        {
            int i = (int)(u * hRes);
            int j = (int)((1 - v) * vRes);
            i = i < 0 ? 0 : i > hRes - 1 ? hRes - 1 : i;
            j = j < 0 ? 0 : j > vRes - 1 ? vRes - 1 : j;
            double R = Bmp.GetPixel(i, j).R / 255.0;
            double G = Bmp.GetPixel(i, j).G / 255.0;
            double B = Bmp.GetPixel(i, j).B / 255.0;

            return new SColor(R, G, B);
        }
    }
}
