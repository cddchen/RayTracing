using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    public class Perlin
    {
        private double[] ranDouble;
        private int[] permX, permY, permZ;
        public Perlin()
        {
            ranDouble = PerlinGenerate();
            permX = PerlinGeneratePerm();
            permY = PerlinGeneratePerm();
            permZ = PerlinGeneratePerm();
        }
        public double noise(Point3D p)
        {
            double u = p.X - Math.Floor(p.X);
            double v = p.Y - Math.Floor(p.Y);
            double w = p.Z - Math.Floor(p.Z);

            u = u * u * (3 - 2 * u);
            v = v * v * (3 - 2 * v);
            w = w * w * (3 - 2 * w);

            int i = (int)Math.Floor(p.X);
            int j = (int)Math.Floor(p.Y);
            int k = (int)Math.Floor(p.Z);

            double[,,] c = new double[2, 2, 2];
            for (int di = 0; di < 2; ++di)
            {
                for (int dj = 0; dj < 2; ++dj)
                {
                    for (int dk = 0; dk < 2; ++dk)
                    {
                        c[di, dj, dk] = ranDouble[permX[(i + di) & 255] ^ permY[(j + dj) & 255] ^ permZ[(k + dk) & 255]];
                    }
                }
            }

            return trilinearInterp(c, u, v, w);
        }
        public static double[] PerlinGenerate()
        {
            double[] p = new double[256];
            for (int i = 0; i < 256; ++i) p[i] = Form2.random();
            return p;
        }
        public double Turb(Point3D p, int depth = 7)
        {
            double accum = 0, weight = 1;
            Point3D tmpP = p;
            for (int i = 0; i < depth; ++i)
            {
                accum += weight * noise(tmpP);
                weight *= 0.5;
                tmpP = new Point3D(tmpP.X * 2, tmpP.Y * 2, tmpP.Z * 2);
            }
            return Math.Abs(accum);
        }
        public static void permute(ref int[] p, int n)
        {
            for (int i = n - 1; i > 0; --i)
            {
                int target = (int)(Form2.random() * (i + 1));
                int tmp = p[i];
                p[i] = p[target];
                p[target] = tmp;
            }
        }
        public static int[] PerlinGeneratePerm()
        {
            int[] p = new int[256];
            for (int i = 0; i < 256; ++i)
            {
                p[i] = i;
            }

            permute(ref p, 256);
            return p;
        }
        public static double trilinearInterp(double[,,] c, double u, double v, double w)
        {
            double accum = 0;
            for (int i = 0; i < 2; ++i)
                for (int j = 0; j < 2; ++j)
                    for (int k = 0; k < 2; ++k)
                        accum += (i * u + (1 - i) * (1 - u)) 
                            * (j * v + (1 - j) * (1 - v)) 
                            * (k * w + (1 - k) * (1 - w)) 
                            * c[i, j, k];

            return accum;
        }
    }
}
