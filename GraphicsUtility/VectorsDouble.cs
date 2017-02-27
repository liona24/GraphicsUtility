using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsUtility
{
    [Serializable]
    public struct Vec2 
    {
        public readonly double X, Y; 

        public Vec2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y);
        }
        public double Length2()
        {
            return X * X + Y * Y;
        }

        public override string ToString()
        {
            return "X=" + X.ToString("N5") + " Y=" + Y.ToString("N5");
        }

        public static Vec2 operator+ (Vec2 l, Vec2 r)
        {
            return new Vec2(l.X + r.X, l.Y + r.Y);
        }
        public static Vec2 operator- (Vec2 l, Vec2 r)
        {
            return new Vec2(l.X - r.X, l.Y - r.Y);
        }
        public static Vec2 operator* (Vec2 l, double scalar)
        {
            return new Vec2(l.X * scalar, l.Y * scalar);
        }
        public static Vec2 operator* (double scalar, Vec2 l)
        {
            return new Vec2(l.X * scalar, l.Y * scalar);
        }
        public static double operator* (Vec2 l, Vec2 r)
        {
            return l.X * r.X + l.Y * r.Y;
        }
        public static explicit operator Vec2I(Vec2 p)
        {
            return new Vec2I((int)p.X, (int)p.Y);
        }

        public static Vec2 FromHomo(Vec3 homo)
        {
            return new Vec2(homo.X / homo.Z, homo.Y / homo.Z);
        }
        public static Vec2 Normalize(Vec2 vec)
        {
            double v = vec.X * vec.X + vec.Y * vec.Y;
            if (v != 0)
            {
                v = Math.Sqrt(v);
                return new Vec2(vec.X / v, vec.Y / v);
            }
            return new Vec2(0, 0);
        }

    }
    [Serializable]
    public struct Vec3
    {
        public readonly double X,Y,Z;

        public Vec3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vec3(Vec2 p, double z)
        {
            X = p.X;
            Y = p.Y;
            Z = z;
        }
        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }
        public double Length2()
        {
            return X * X + Y * Y + Z * Z;
        }

        public override string ToString()
        {
            return "X=" + X.ToString("N5") + " Y=" + Y.ToString("N5") + " Z=" + Z.ToString("N5");
        }

        public static Vec3 Cross(Vec3 l, Vec3 r)
        {
            return new Vec3(l.Y * r.Z - l.Z * r.Y, l.Z * r.X - l.X * r.Z, l.X * r.Y - l.Y * r.X);
        }

        public Vec3 WeightThis(Vec3 w)
        {
            return new Vec3(X * w.X, Y * w.Y, Z * w.Z);
        }

        public static Vec3 FromHomo(Vec4 h)
        {
            return new Vec3(h.X / h.W, h.Y / h.W, h.Z / h.W);
        }

        public static Vec3 operator+ (Vec3 l, Vec3 r)
        {
            return new Vec3(l.X + r.X, l.Y + r.Y, l.Z + r.Z);
        }
        public static Vec3 operator- (Vec3 l, Vec3 r)
        {
            return new Vec3(l.X - r.X, l.Y - r.Y, l.Z - r.Z);
        }
        public static Vec3 operator* (Vec3 v, double scalar)
        {
            return new Vec3(v.X * scalar, v.Y * scalar, v.Z * scalar);
        }
        public static Vec3 operator *(double scalar, Vec3 v)
        {
            return new Vec3(v.X * scalar, v.Y * scalar, v.Z * scalar);
        }
        public static double operator* (Vec3 l, Vec3 r)
        {
            return l.X * r.X + l.Y * r.Y + l.Z * r.Z;
        }
        public static explicit operator Vec3I(Vec3 p)
        {
            return new Vec3I((int)p.X, (int)p.Y, (int)p.Z);
        }

        public static Vec3 Normalize(Vec3 vec)
        {
            double v = vec.X * vec.X + vec.Y * vec.Y + vec.Z * vec.Z;
            if (v != 0)
            {
                v = Math.Sqrt(v);
                return new Vec3(vec.X / v, vec.Y / v, vec.Z / v);
            }
            return new Vec3(0, 0, 0);
        }
    }
    [Serializable]
    public struct Vec4
    {
        public readonly double X, Y, Z, W;

        public Vec4(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        public Vec4(Vec3 v, double w)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
            W = w ;
        }

        public double Length()
        {
            return Math.Sqrt(Length2());
        }
        public double Length2()
        {
            if (Math.Abs(W) < 0.0001)
                return X * X + Y * Y + Z * Z;
            double n = 1 / W / W;
            return X * X * n + Y * Y * n + Z * Z * n;
        }

        /// <summary>
        /// Normalizes a homogenous vector to have W = 1
        /// </summary>
        public static Vec4 NormW(Vec4 vec)
        {
            if (vec.W == 0 || vec.W == 1)
                return new Vec4(vec.X, vec.Y, vec.Z, vec.W);
            return new Vec4(vec.X / vec.W, vec.Y / vec.W, vec.Z / vec.W, 1);
        }
        /// <summary>
        /// Normalizes a homogenous vector to unit length
        /// </summary>
        public static Vec4 Normalize(Vec4 vec)
        {
            vec = NormW(vec);
            double l = Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y + vec.Z * vec.Z);
            if (l > 0)
            {
                l = 1 / l;
                return new Vec4(vec.X * l, vec.Y * l, vec.Z * l, 1);
            }
            return new Vec4(0, 0, 0, vec.W);
        }

        public Vec3 ToCartesian()
        {
            if (W == 0)
                return new Vec3(X, Y, Z);
            return new Vec3(X / W, Y / W, Z / W);
        }

        public override string ToString()
        {
            return "X=" + X.ToString("N5") + " Y=" + Y.ToString("N5") + " Z=" + Z.ToString("N5") + " W=" + W.ToString("N5");
        }

        public static Vec4 operator +(Vec4 l, Vec4 r)
        {
            return new Vec4(l.X + r.X, l.Y + r.Y, l.Z + r.Z, l.W + r.W);
        }
        public static Vec4 operator -(Vec4 l, Vec4 r)
        {
            return new Vec4(l.X - r.X, l.Y - r.Y, l.Z - r.Z, l.W - r.W);
        }
        public static Vec4 operator *(Vec4 v, double scalar)
        {
            return new Vec4(v.X, v.Y, v.Z, v.W / scalar);
        }
        public static Vec4 operator *(double scalar, Vec4 v)
        {
            return new Vec4(v.X, v.Y, v.Z, v.W / scalar);
        }
    }
}
