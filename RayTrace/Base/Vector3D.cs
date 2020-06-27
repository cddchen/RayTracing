using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    public class Matrix
    {
        int _row, _column;
        double[,] _mat;
        public Matrix(int row, int column)
        {
            _row = row; _column = column;
            Mat = new double[row, column];
            for (int i = 0; i < row; ++i)
                for (int j = 0; j < column; ++j)
                    Mat[i, j] = 0;
        }
        public Matrix(int row, int column, double[,] mat)
        {
            _row = row; _column = column;
            Mat = new double[row, column];
            for (int i = 0; i < row; ++i)
                for (int j = 0; j < column; ++j)
                    Mat[i, j] = mat[i, j];
        }

        public double[,] Mat { get => _mat; set => _mat = value; }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            Matrix res = new Matrix(a._row, b._column);
            for (int i = 0; i < a._row; ++i)
            {
                for (int j = 0; j < b._column; ++j)
                {
                    for (int k = 0; k < a._column; ++k)
                    {
                        res.Mat[i, j] += a.Mat[i, k] * b.Mat[k, j];
                    }
                }
            }
            return res;
        }
        public static Matrix operator +(Matrix a, Matrix b)
        {
            Matrix res = new Matrix(a._row, a._column);
            for (int i = 0; i < a._row; ++i)
            {
                for (int j = 0; j < a._column; ++j)
                {
                    res.Mat[i, j] = a.Mat[i, j] + b.Mat[i, j];
                }
            }
            return res;
        }
        public static Matrix operator -(Matrix a, Matrix b)
        {
            Matrix res = new Matrix(a._row, a._column);
            for (int i = 0; i < a._row; ++i)
            {
                for (int j = 0; j < a._column; ++j)
                {
                    res.Mat[i, j] = a.Mat[i, j] - b.Mat[i, j];
                }
            }
            return res;
        }
        public static Matrix operator *(Matrix a, double b)
        {
            Matrix res = new Matrix(a._row, a._column);
            for (int i = 0; i < a._row; ++i)
            {
                for (int j = 0; j < a._column; ++j)
                {
                    res.Mat[i, j] = a.Mat[i, j] * b;
                }
            }
            return res;
        }
        public static Matrix inverse(Matrix a)
        {
            int m = a._row;
            int n = a._column;
            double[,] array = new double[2 * m + 1, 2 * n + 1];
            for (int k = 0; k < 2 * m + 1; k++)  //初始化数组
            {
                for (int t = 0; t < 2 * n + 1; t++)
                {
                    array[k, t] = 0.00000000;
                }
            }
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    array[i, j] = a.Mat[i, j];
                }
            }

            for (int k = 0; k < m; k++)
            {
                for (int t = n; t <= 2 * n; t++)
                {
                    if ((t - k) == m)
                    {
                        array[k, t] = 1.0;
                    }
                    else
                    {
                        array[k, t] = 0;
                    }
                }
            }
                //得到逆矩阵
            for (int k = 0; k < m; k++)
            {
                if (array[k, k] != 1)
                {
                    double bs = array[k, k];
                    array[k, k] = 1;
                    for (int p = k + 1; p < 2 * n; p++)
                    {
                        array[k, p] /= bs;
                    }
                }
                for (int q = 0; q < m; q++)
                {
                    if (q != k)
                    {
                        double bs = array[q, k];
                        for (int p = 0; p < 2 * n; p++)
                        {
                            array[q, p] -= bs * array[k, p];
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            Matrix res = new Matrix(m, n);
            for (int x = 0; x < m; x++)
            {
                for (int y = n; y < 2 * n; y++)
                {
                    res.Mat[x, y - n] = array[x, y];
                }
            }
            return res;
        }
        public static Matrix transpose(Matrix a)
        {
            Matrix res = new Matrix(a._column, a._row);
            for (int i = 0; i < a._column; ++i)
            {
                for (int j = 0; j < a._row; ++j)
                {
                    res.Mat[i, j] = a.Mat[j, i];
                }
            }
            return res;
        }

        public static Matrix rotate(double degrees, Vector3D axis)
        {
            Vector3D _axis = axis.getNormalize();
            double theta = degrees / 180 * Math.PI;
            Matrix a_ta = new Matrix(3, 3, 
                new double[3, 3]{ { _axis.X * _axis.X, _axis.X * _axis.Y, _axis.X * _axis.Z },
                {_axis.Y * _axis.X, _axis.Y * _axis.Y, _axis.Y * _axis.Z },
                {_axis.Z * _axis.X, _axis.Z * _axis.Y, _axis.Z * _axis.Z }});
            Matrix I = new Matrix(3, 3, new double[3, 3]{ { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
            Matrix Astar = new Matrix(3, 3, new double[3, 3] { { 0, -_axis.Z, _axis.Y }, { _axis.Z, 0, -_axis.X }, { -_axis.Y, _axis.X, 0 } });
            return a_ta + (I - a_ta) * Math.Cos(theta) + Astar * Math.Sin(theta);
        }
    }
    public class Vector3D
    {
        private double _x, _y, _z;
        public double X
        {
            get => _x;
            set => _x = value;
        }
        public double Y
        {
            get => _y;
            set => _y = value;
        }
        public double Z
        {
            get => _z;
            set => _z = value;
        }
        public Vector3D() {
            this.X = this.Y = this.Z = 0;
        }
        public Vector3D(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public static Vector3D operator -(Vector3D p1, Vector3D p2)
        {
            return new Vector3D(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }

        public static Vector3D operator +(Vector3D p1, Vector3D p2)
        {
            return new Vector3D(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        }
        //点乘
        public static double operator *(Vector3D p1, Vector3D p2)
        {
            return p1.X * p2.X + p1.Y * p2.Y + p1.Z * p2.Z;
        }

        public static Vector3D operator *(double d, Vector3D p1)
        {
            return new Vector3D(d * p1.X, d * p1.Y, d * p1.Z);
        }
        public static Vector3D operator *(Vector3D p1, double d)
        {
            return new Vector3D(d * p1.X, d * p1.Y, d * p1.Z);
        }
        //叉乘
        public static Vector3D operator ^(Vector3D p1, Vector3D p2)
        {
            return new Vector3D(p1.Y * p2.Z - p2.Y * p1.Z, p1.Z * p2.X - p1.X * p2.Z, p1.X * p2.Y - p1.Y * p2.X);
        }
        public double Magnitude()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }
        public double Sqr()
        {
            return X * X + Y * Y + Z * Z;
        }
        public void Normalize()
        {
            double d = Magnitude();
            X /= d;
            Y /= d;
            Z /= d;
        }
        public Vector3D getNormalize()
        {
            double d = Magnitude();
            return new Vector3D(X / d, Y / d, Z / d);
        }
        public Vector3D Rotate(double beta)
        {
            Matrix vec = new Matrix(1, 4, new double[1, 4] { { this.X, this.Y, this.Z, 1 } });
            Matrix trans = new Matrix(4, 4);
            trans.Mat[0, 0] = Math.Cos(beta); trans.Mat[0, 2] = -Math.Sin(beta);
            trans.Mat[1, 1] = 1;
            trans.Mat[2, 0] = Math.Sin(beta); trans.Mat[2, 2] = Math.Cos(beta);
            trans.Mat[3, 3] = 1;
            vec = vec * trans;
            return new Vector3D(vec.Mat[0, 0], vec.Mat[0, 1], vec.Mat[0, 2]);
        }
        public static Vector3D RandomInUnitSphere()
        {
            Vector3D rhs;
            do
            {
                Vector3D p = new Vector3D(Form2.random(), Form2.random(), Form2.random());
                Vector3D unitV = new Vector3D(1, 1, 1);
                rhs = 2.0 * p - unitV;
            } while (rhs.Sqr() >= 1.0);
            return rhs;
        }
    }
}
