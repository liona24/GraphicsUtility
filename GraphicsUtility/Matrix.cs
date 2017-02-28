using System;

namespace GraphicsUtility
{
    public class Matrix2 : ICloneable
    {
        // v0 v1
        // v2 v3
        protected double[] v;

        public double this[int i] { get { return v[i]; } set { v[i] = value; } }
        public double this[int i, int j] { get { return v[j * 2 + i]; } set { v[j * 2 + i] = value; } }

        public int Width { get { return 2; } }
        public int Height { get { return 2; } }

        public Matrix2(double c)
        {
            v = new double[] { c, c, c, c }; 
        }
        public Matrix2(double[] values)
        {
            v = values;
        }
        public Matrix2(double v11, double v12, double v21, double v22)
        {
            v = new double[4];
            v[0] = v11;
            v[1] = v12;
            v[2] = v21;
            v[3] = v22;
        }

        public void Transpose()
        {
            double v21 = v[2];
            v[2] = v[1];
            v[1] = v21;
        }
        public override string ToString()
        {
            return "[" + v[0].ToString("N3") + " " + v[1].ToString("N3") + "]\n[" + v[2].ToString("N3") + " " + v[3].ToString("N3") + "]";
        }

        public double Det()
        {
            return v[0] * v[3] - v[1] * v[2];
        }
        public double Trace()
        {
            return v[0] + v[3];
        }
        public bool Inverse()
        {
            double det = Det();
            if (det == 0)
            {
                return false;
            }
            det = 1 / det;
            v = new double[] { v[3] * det, -v[1] * det, -v[2] * det, v[0] * det };
            return true;
        }
        public bool Inverse(out Matrix2 result)
        {
            double det = Det();
            if (det == 0)
            {
                result = null;
                return false;
            }
            det = 1 / det;
            result = new Matrix2(v[3] * det, -v[1] * det, -v[2] * det, v[0] * det);
            return true;
        }

        public void Add(Matrix2 r)
        {
            v[0] += r[0];
            v[1] += r[1];
            v[2] += r[2];
            v[3] += r[3];
        }
        public void Sub(Matrix2 r)
        {
            v[0] -= r[0];
            v[1] -= r[1];
            v[2] -= r[2];
            v[3] -= r[3];
        }

        public void Apply(Func<int, double> f)
        {
            for (int i = 0; i < 4; i++)
                v[i] = f(i);
        }
        public void Apply(Func<int, int, double> f)
        {
            v[0] = f(0, 0);
            v[1] = f(1, 0);
            v[2] = f(0, 1);
            v[3] = f(1, 1);
        }
        public void Apply(Func<double, double> f)
        {
            for (int i = 0; i < 4; i++)
                v[i] = f(v[i]);
        }

        public object Clone()
        {
            return new Matrix2(v[0], v[1], v[2], v[3]);
        }

        public static Matrix2 GetIdentity()
        {
            return new Matrix2(1, 0, 0, 1);
        }

        public static Matrix2 operator +(Matrix2 l, Matrix2 r)
        {
            return new Matrix2(l[0] + r[0], l[1] + r[1], l[2] + r[2], l[3] + r[3]);
        }
        public static Matrix2 operator -(Matrix2 l, Matrix2 r)
        {
            return new Matrix2(l[0] + r[0], l[1] + r[1], l[2] + r[2], l[3] + r[3]);
        }
        public static Matrix2 operator *(Matrix2 l, Matrix2 r)
        {
            return new Matrix2(l[0] * r[0] + l[1] * r[2], 
                                l[0] * r[1] + l[1] * r[3],  
                                l[2] * r[0] + l[3] * r[2],
                                l[2] * r[1] + l[3] * r[3]);
        }
        public static Matrix2 operator *(Matrix2 l, double s)
        {
            return new Matrix2(l[0] * s, l[1] * s, l[2] * s, l[3] * s);
        }
        public static Vec2 operator *(Matrix2 l, Vec2 r)
        {
            return new Vec2(l[0] * r.X + l[1] * r.Y, l[2] * r.X + l[3] * r.Y);
        }
    }
    public class Matrix3 : ICloneable
    {
        protected double[] v;

        public double this[int i] { get { return v[i]; } set { v[i] = value; } }
        public double this[int i, int j] { get { return v[i * 3 + j]; } set { v[i * 3 + j] = value; } }

        public int Width { get { return 3; } }
        public int Height { get { return 3; } }

        public Matrix3(double c)
        {
            v = new double[9];
            for (int i = 0; i < 9; i++)
                v[i] = c;
        }
        public Matrix3(double[] values)
        {
            v = values;
        }
        public Matrix3(double v11, double v12, double v13, double v21, double v22, double v23, double v31, double v32, double v33)
        {
            v = new double[9];
            v[0] = v11;
            v[1] = v12;
            v[2] = v13;
            v[3] = v21;
            v[4] = v22;
            v[5] = v23;
            v[6] = v31;
            v[7] = v32;
            v[8] = v33;
        }

        public double Det()
        {
            return v[0] * v[4] * v[8] + v[3] * v[7] * v[2] + v[6] * v[1] * v[5] - v[0] * v[7] * v[5] - v[6] * v[4] * v[2] - v[3] * v[1] * v[8];
        }
        public double Trace()
        {
            return v[0] + v[4] + v[8];
        }
        public void Transpose()
        {
            double[] v = new double[9];
            int k = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    v[k++] = v[j * 3 + i];
                }
            }
            this.v = v;
        }
        public bool Inverse()
        {
            double det = Det();
            if (det == 0)
            {
                return false;
            }
            det = 1 / det;
            v = new double[] {
                v[4]*v[8] - v[5]*v[7],  v[2]*v[7] - v[1]*v[8],  v[1]*v[5] - v[2]*v[4],
                v[5]*v[6] - v[3]*v[8],  v[0]*v[8] - v[2]*v[6],  v[2]*v[3] - v[0]*v[5],
                v[3]*v[7] - v[4]*v[6],  v[1]*v[6] - v[0]*v[7],  v[0]*v[4] - v[1]*v[3] };
            for (int i = 0; i < 9; i++)
                v[i] *= det;

            return true;
        }
        public bool Inverse(out Matrix3 result)
        {
            double det = Det();
            if (det == 0)
            {
                result = null;
                return false;
            }
            det = 1 / det;
            var newVal = new double[] {
                v[4]*v[8] - v[5]*v[7],  v[2]*v[7] - v[1]*v[8],  v[1]*v[5] - v[2]*v[4],
                v[5]*v[6] - v[3]*v[8],  v[0]*v[8] - v[2]*v[6],  v[2]*v[3] - v[0]*v[5],
                v[3]*v[7] - v[4]*v[6],  v[1]*v[6] - v[0]*v[7],  v[0]*v[4] - v[1]*v[3] };
            for (int i = 0; i < 9; i++)
                newVal[i] *= det;
            result = new Matrix3(newVal);
            return true;
        }
        
        public void Add(Matrix3 r)
        {
            for (int i = 0; i < 9; i++)
                v[i] += r[i];
        }
        public void Sub(Matrix3 r)
        {
            for (int i = 0; i < 9; i++)
                v[i] -= r[i];
        }

        public void Apply(Func<int, double> f)
        {
            for (int i = 0; i < 9; i++)
                v[i] = f(i);
        }
        public void Apply(Func<int, int, double> f)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                    v[j * 3 + i] = f(i, j);
            }
        }
        public void Apply(Func<double, double> f)
        {
            for (int i = 0; i < 9; i++)
                v[i] = f(v[i]);
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < 3; i++)
            {
                s += "[ ";
                for (int j = 0; j < 3; j++)
                {
                    s += v[i * 3 + j].ToString("N4") + " ";
                }
                s += "]\n";
            }
            return s;
        }

        public object Clone()
        {
            double[] nV = new double[9];
            for (int i = 0; i < 9; i++)
                nV[i] = v[i];
            return new Matrix3(nV);
        }

        public static Matrix3 operator +(Matrix3 l, Matrix3 r)
        {
            var v = new double[9];
            for (int i = 0; i < 9; i++)
                v[i] = l[i] + r[i];
            return new Matrix3(v);
        }
        public static Matrix3 operator -(Matrix3 l, Matrix3 r)
        {
            var v = new double[9];
            for (int i = 0; i < 9; i++)
                v[i] = l[i] - r[i];
            return new Matrix3(v);
        }
        public static Matrix3 operator *(Matrix3 l, double scalar)
        {
            var v = new double[9];
            for (int i = 0; i < 9; i++)
                v[i] = l[i] * scalar;
            return new Matrix3(v);
        }
        public static Matrix3 operator *(Matrix3 l, Matrix3 r)
        {
            var v = new double[9];
            int k = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    v[k++] = l[i, 0] * r[0, j] +
                            l[i, 1] * r[1, j] +
                            l[i, 2] * r[2, j];
                }
            }
            return new Matrix3(v);
        }
        public static Vec3 operator *(Matrix3 l, Vec3 r)
        {
            return new Vec3(l[0] * r.X + l[1] * r.Y + l[2] * r.Z, 
                l[3] * r.X + l[4] * r.Y + l[5] * r.Z, 
                l[6] * r.X + l[7] * r.Y + l[8] * r.Z);
        }

        public static Matrix3 GetIdentity()
        {
            return new Matrix3(1, 0, 0, 0, 1, 0, 0, 0, 1);
        }

    }
    public class Matrix4 : ICloneable
    {
        public double this[int i, int j]
        {
            get { return v[4 * i + j]; }
            set { v[4 * i + j] = value; }
        }
        public double this[int i]
        {
            get { return v[i]; }
            set { v[i] = value; }
        }

        public int Width { get { return 4; } }
        public int Height { get { return 4; } }

        //row major order
        protected double[] v;

        public Matrix4(double c)
        {
            v = new double[16];
            for (int i = 0; i < 16; i++)
                v[i] = c;
        }
        public Matrix4(double[][] values)
        {
            v = new double[16];
            int k = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    v[k++] = values[i][j];
                }
            }
        }
        public Matrix4(double[] values)
        {
            v = values;
        }
        public Matrix4(Matrix4 other)
        {
            v = other.v;
        }

        public void SetRow(int i, double[] row)
        {
            for (int j = 0; j < 4; j++)
            {
                v[i * 4 + j] = row[j];
            }
        }
        public void SetCollumn(int i, double[] col)
        {
            for (int j = 0; j < 4; j++)
            {
                v[j * 4 + i] = col[j];
            }
        }
        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < 4; i++)
            {
                s += "[ ";
                for (int j = 0; j < 4; j++)
                {
                    s += v[i * 4 + j].ToString("N4") + " ";
                }
                s += "]\n";
            }
            return s;
        }

        public void Transpose()
        {
            double[] v = new double[16];
            int k = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    v[k++] = this.v[j * 4 + i];
                }
            }
            this.v = v;
        }
        public bool Inverse()
        {
            double[] inv = new double[16];
            double det;
            int i;

            inv[0] = v[5]  * v[10] * v[15] -
                     v[5]  * v[11] * v[14] -
                     v[9]  * v[6]  * v[15] +
                     v[6]  * v[13]  * v[11] +
                     v[7] * v[9]  * v[14] -
                     v[13] * v[7]  * v[10];

            inv[1] = -v[1]  * v[10] * v[15] +
                      v[1]  * v[11] * v[14] +
                      v[2]  * v[9]  * v[15] -
                      v[2]  * v[13]  * v[11] -
                      v[3] * v[9]  * v[14] +
                      v[3] * v[13]  * v[10];

            inv[2] = v[1]  * v[6] * v[15] -
                     v[1]  * v[14] * v[7] -
                     v[2]  * v[5] * v[15] +
                     v[2]  * v[13] * v[7] +
                     v[3] * v[5] * v[14] -
                     v[3] * v[13] * v[6];

            inv[3] = -v[1]  * v[5] * v[11] +
                       v[1]  * v[10] * v[7] +
                       v[2]  * v[5] * v[11] -
                       v[2]  * v[9] * v[7] -
                       v[3] * v[5] * v[10] +
                       v[3] * v[9] * v[6];

            det = v[0] * inv[0] + v[4] * inv[1] + v[8] * inv[2] + v[12] * inv[3];

            if (det == 0)
            {
                return false;
            }


            inv[4] = -v[4]  * v[10] * v[15] +
                      v[4]  * v[14] * v[11] +
                      v[6]  * v[8] * v[15] -
                      v[6]  * v[12] * v[11] -
                      v[7] * v[8] * v[14] +
                      v[7] * v[12] * v[10];

            inv[5] = v[0]  * v[10] * v[15] -
                     v[0]  * v[11] * v[14] -
                     v[8]  * v[2] * v[15] +
                     v[8]  * v[12] * v[11] +
                     v[3] * v[8] * v[14] -
                     v[12] * v[3] * v[10];

            inv[6] = -v[0]  * v[6] * v[15] +
                      v[0]  * v[14] * v[7] +
                      v[2]  * v[4] * v[15] -
                      v[2]  * v[12] * v[7] -
                      v[3] * v[4] * v[14] +
                      v[3] * v[12] * v[6];

            inv[7] = v[0]  * v[6] * v[11] -
                      v[0]  * v[10] * v[7] -
                      v[2]  * v[4] * v[11] +
                      v[2]  * v[8] * v[7] +
                      v[3] * v[4] * v[10] -
                      v[3] * v[8] * v[6];

            inv[8] = v[4]  * v[9] * v[15] -
                     v[4]  * v[13] * v[11] -
                     v[5]  * v[8] * v[15] +
                     v[5]  * v[12] * v[11] +
                     v[7] * v[8] * v[13] -
                     v[7] * v[12] * v[9];

            inv[9] = -v[0]  * v[9] * v[15] +
                      v[0]  * v[13] * v[11] +
                      v[1]  * v[8] * v[15] -
                      v[1]  * v[12] * v[11] -
                      v[3] * v[8] * v[13] +
                      v[3] * v[12] * v[9];
            //check 10
            inv[10] = v[0]  * v[5] * v[15] -
                      v[0]  * v[7] * v[13] -
                      v[4]  * v[1] * v[15] +
                      v[4]  * v[12] * v[7] +
                      v[3] * v[4] * v[13] -
                      v[3] * v[12] * v[5];

            inv[11] = -v[0]  * v[5] * v[11] +
                       v[0]  * v[9] * v[7] +
                       v[4]  * v[1] * v[11] -
                       v[1]  * v[8] * v[7] -
                       v[3] * v[4] * v[9] +
                       v[3] * v[8] * v[5];

            inv[12] = -v[4] * v[9] * v[14] +
                      v[4] * v[13] * v[10] +
                      v[5] * v[8] * v[14] -
                      v[5] * v[12] * v[10] -
                      v[6] * v[8] * v[13] +
                      v[6] * v[12] * v[9];

            inv[13] = v[0] * v[9] * v[14] -
                     v[0] * v[13] * v[10] -
                     v[1] * v[8] * v[14] +
                     v[1] * v[12] * v[10] +
                     v[2] * v[8] * v[13] -
                     v[2] * v[12] * v[9];

            inv[14] = -v[0] * v[5] * v[14] +
                       v[0] * v[13] * v[6] +
                       v[4] * v[1] * v[14] -
                       v[1] * v[12] * v[6] -
                       v[2] * v[4] * v[13] +
                       v[2] * v[12] * v[5];

            inv[15] = v[0] * v[5] * v[10] -
                      v[0] * v[6] * v[9] -
                      v[4] * v[1] * v[10] +
                      v[1] * v[8] * v[6] +
                      v[2] * v[4] * v[9] -
                      v[2] * v[8] * v[5];

            det = 1.0 / det;

            for (i = 0; i < 16; i++)
                inv[i] = inv[i] * det;

            v = inv;

            return true;
        }
        public bool Inverse(out Matrix4 result)
        {

            double[] inv = new double[16];
            double det;

            inv[0] = v[5]  * v[10] * v[15] -
                     v[5]  * v[11] * v[14] -
                     v[9]  * v[6]  * v[15] +
                     v[6]  * v[13]  * v[11] +
                     v[7] * v[9]  * v[14] -
                     v[13] * v[7]  * v[10];

            inv[1] = -v[1]  * v[10] * v[15] +
                      v[1]  * v[11] * v[14] +
                      v[2]  * v[9]  * v[15] -
                      v[2]  * v[13]  * v[11] -
                      v[3] * v[9]  * v[14] +
                      v[3] * v[13]  * v[10];

            inv[2] = v[1]  * v[6] * v[15] -
                     v[1]  * v[14] * v[7] -
                     v[2]  * v[5] * v[15] +
                     v[2]  * v[13] * v[7] +
                     v[3] * v[5] * v[14] -
                     v[3] * v[13] * v[6];

            inv[3] = -v[1]  * v[5] * v[11] +
                       v[1]  * v[10] * v[7] +
                       v[2]  * v[5] * v[11] -
                       v[2]  * v[9] * v[7] -
                       v[3] * v[5] * v[10] +
                       v[3] * v[9] * v[6];

            det = v[0] * inv[0] + v[4] * inv[1] + v[8] * inv[2] + v[12] * inv[3];

            if (det == 0)
            {
                result = null;
                return false;
            }


            inv[4] = -v[4]  * v[10] * v[15] +
                      v[4]  * v[14] * v[11] +
                      v[6]  * v[8] * v[15] -
                      v[6]  * v[12] * v[11] -
                      v[7] * v[8] * v[14] +
                      v[7] * v[12] * v[10];

            inv[5] = v[0]  * v[10] * v[15] -
                     v[0]  * v[11] * v[14] -
                     v[8]  * v[2] * v[15] +
                     v[8]  * v[12] * v[11] +
                     v[3] * v[8] * v[14] -
                     v[12] * v[3] * v[10];

            inv[6] = -v[0]  * v[6] * v[15] +
                      v[0]  * v[14] * v[7] +
                      v[2]  * v[4] * v[15] -
                      v[2]  * v[12] * v[7] -
                      v[3] * v[4] * v[14] +
                      v[3] * v[12] * v[6];

            inv[7] = v[0]  * v[6] * v[11] -
                      v[0]  * v[10] * v[7] -
                      v[2]  * v[4] * v[11] +
                      v[2]  * v[8] * v[7] +
                      v[3] * v[4] * v[10] -
                      v[3] * v[8] * v[6];

            inv[8] = v[4]  * v[9] * v[15] -
                     v[4]  * v[13] * v[11] -
                     v[5]  * v[8] * v[15] +
                     v[5]  * v[12] * v[11] +
                     v[7] * v[8] * v[13] -
                     v[7] * v[12] * v[9];

            inv[9] = -v[0]  * v[9] * v[15] +
                      v[0]  * v[13] * v[11] +
                      v[1]  * v[8] * v[15] -
                      v[1]  * v[12] * v[11] -
                      v[3] * v[8] * v[13] +
                      v[3] * v[12] * v[9];
            //check 10
            inv[10] = v[0]  * v[5] * v[15] -
                      v[0]  * v[7] * v[13] -
                      v[4]  * v[1] * v[15] +
                      v[4]  * v[12] * v[7] +
                      v[3] * v[4] * v[13] -
                      v[3] * v[12] * v[5];

            inv[11] = -v[0]  * v[5] * v[11] +
                       v[0]  * v[9] * v[7] +
                       v[4]  * v[1] * v[11] -
                       v[1]  * v[8] * v[7] -
                       v[3] * v[4] * v[9] +
                       v[3] * v[8] * v[5];

            inv[12] = -v[4] * v[9] * v[14] +
                      v[4] * v[13] * v[10] +
                      v[5] * v[8] * v[14] -
                      v[5] * v[12] * v[10] -
                      v[6] * v[8] * v[13] +
                      v[6] * v[12] * v[9];

            inv[13] = v[0] * v[9] * v[14] -
                     v[0] * v[13] * v[10] -
                     v[1] * v[8] * v[14] +
                     v[1] * v[12] * v[10] +
                     v[2] * v[8] * v[13] -
                     v[2] * v[12] * v[9];

            inv[14] = -v[0] * v[5] * v[14] +
                       v[0] * v[13] * v[6] +
                       v[4] * v[1] * v[14] -
                       v[1] * v[12] * v[6] -
                       v[2] * v[4] * v[13] +
                       v[2] * v[12] * v[5];

            inv[15] = v[0] * v[5] * v[10] -
                      v[0] * v[6] * v[9] -
                      v[4] * v[1] * v[10] +
                      v[1] * v[8] * v[6] +
                      v[2] * v[4] * v[9] -
                      v[2] * v[8] * v[5];

            det = 1.0 / det;

            for (int i = 0; i < 16; i++)
                inv[i] = inv[i] * det;

            result = new Matrix4(inv);
            return true;
            
        }
        public double Det()
        {
            double i0 = v[5] * v[10] * v[15] -
                     v[5] * v[11] * v[14] -
                     v[9] * v[6] * v[15] +
                     v[6] * v[13] * v[11] +
                     v[7] * v[9] * v[14] -
                     v[13] * v[7] * v[10];

            double i1 = -v[1] * v[10] * v[15] +
                      v[1] * v[11] * v[14] +
                      v[2] * v[9] * v[15] -
                      v[2] * v[13] * v[11] -
                      v[3] * v[9] * v[14] +
                      v[3] * v[13] * v[10];

            double i2 = v[1] * v[6] * v[15] -
                     v[1] * v[14] * v[7] -
                     v[2] * v[5] * v[15] +
                     v[2] * v[13] * v[7] +
                     v[3] * v[5] * v[14] -
                     v[3] * v[13] * v[6];

            double i3 = -v[1] * v[5] * v[11] +
                       v[1] * v[10] * v[7] +
                       v[2] * v[5] * v[11] -
                       v[2] * v[9] * v[7] -
                       v[3] * v[5] * v[10] +
                       v[3] * v[9] * v[6];

            return v[0] * i0 + v[4] * i1 + v[8] * i2 + v[12] * i3;
        }
        public double Trace()
        {
            double t = 0;
            for (int i = 0; i < 4; i++)
                t += v[i * 4 + i];
            return t;
        }

        public void Add(Matrix4 r)
        {
            for (int i = 0; i < 16; i++)
                v[i] += r[i];
        }
        public void Sub(Matrix4 r)
        {
            for (int i = 0; i < 16; i++)
                v[i] += r[i];
        }
        public void Multiply(Matrix4 r)
        {
            double[] v2 = new double[16];
            int k = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    v2[k++] = v[i * 4] * r[0, j] +
                            v[i * 4 + 1] * r[1, j] +
                            v[i * 4 + 2] * r[2, j] +
                            v[i * 4+  3] * r[3, j];
                }
            }
            v = v2;
        }

        public void Apply(Func<int, double> f)
        {
            for (int i = 0; i < 16; i++)
                v[i] = f(i);
        }
        public void Apply(Func<int, int, double> f)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                    v[j * 4 + i] = f(i, j);
            }
        }
        public void Apply(Func<double, double> f)
        {
            for (int i = 0; i < 16; i++)
                v[i] = f(v[i]);
        }

        public object Clone()
        {
            double[] nVals = new double[16];
            for (int i = 0; i < 16; i++)
                nVals[i] = v[i];
            return new Matrix4(nVals);
        }

        public static Vec4 operator* (Matrix4 l, Vec4 r)
        {
            return new Vec4(l[0] * r.X + l[1] * r.Y + l[2] * r.Z + l[3] * r.W,
                            l[4] * r.X + l[5] * r.Y + l[6] * r.Z + l[7] * r.W,
                            l[8] * r.X + l[9] * r.Y + l[10] * r.Z + l[11] * r.W,
                            l[12] * r.X + l[13] * r.Y + l[14] * r.Z + l[15] * r.W);
        }
        public static Matrix4 operator* (Matrix4 l, Matrix4 r)
        {
            double[] v = new double[16];
            int k = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    v[k++] = l[i, 0] * r[0, j] +
                            l[i, 1] * r[1, j] +
                            l[i, 2] * r[2, j] +
                            l[i, 3] * r[3, j];
                }
            }
            return new Matrix4(v);
        }
        public static Matrix4 operator* (Matrix4 l, double scalar)
        {
            double[] v = new double[16];
            for (int i = 0; i < 16; i++)
                v[i] = l[i] * scalar;
            return new Matrix4(v);
        }
        public static Matrix4 operator+ (Matrix4 l, Matrix4 r)
        {
            double[] v = new double[16];
            for (int i = 0; i < 16; i++)
                v[i] = l[i] + r[i];
            return new Matrix4(v);
        }
        public static Matrix4 operator- (Matrix4 l, Matrix4 r)
        {
            double[] nV = new double[16];
            for (int i = 0; i < 16; i++)
                nV[i] = l[i] - r[i];
            return new Matrix4(nV);
        }

        public static Matrix4 GetIdentity()
        {
            double[] v = new double[16] { 1, 0, 0, 0,
                                        0, 1, 0, 0,
                                        0, 0, 1, 0,
                                        0, 0, 0, 1, };
            return new Matrix4(v);
        }
    }
}
