using System;

namespace GraphicsUtility
{
    [Serializable]
    public struct Vec2I
    {
        public readonly int X, Y;

        public Vec2I(int x, int y)
        {
            X = x;
            Y = y;
        }

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y);
        }
        public int Length2()
        {
            return X * X + Y * Y;
        }

        public override string ToString()
        {
            return "X=" + X.ToString() + " Y=" + Y.ToString(); 
        }

        public static Vec2I operator + (Vec2I l, Vec2I r)
        {
            return new Vec2I(l.X + r.X, l.Y + r.Y);
        }
        public static Vec2I operator - (Vec2I l, Vec2I r)
        {
            return new Vec2I(l.X - r.X, l.Y - r.Y);
        }
        public static Vec2I operator * (Vec2I l, int scalar)
        {
            return new Vec2I(l.X * scalar, l.Y * scalar);
        }
        public static int operator * (Vec2I l, Vec2I r)
        {
            return l.X * r.X + l.Y * l.Y;
        }
        public static implicit operator Vec2(Vec2I p)
        {
            return new Vec2(p.X, p.Y);
        }
    }

    [Serializable]
    public struct Vec3I
    {
        public readonly int X, Y, Z;

        public Vec3I(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }
        public int Length2()
        {
            return X * X + Y * Y + Z * Z;
        }

        public override string ToString()
        {
            return "X=" + X.ToString("N5") + " Y=" + Y.ToString("N5") + " Z=" + Z.ToString("N5");
        }

        public static Vec3I Cross(Vec3I l, Vec3I r)
        {
            return new Vec3I(l.Y * r.Z - l.Z * r.Y, l.Z * r.X - l.X * r.Z, l.X * r.Y - l.Y * r.X);
        }

        public static Vec3I operator+ (Vec3I l, Vec3I r)
        {
            return new Vec3I(l.X + r.X, l.Y + r.Y, l.Z + l.Y);
        }
        public static Vec3I operator- (Vec3I l, Vec3I r)
        {
            return new Vec3I(l.X - r.X, l.Y - r.Y, l.Z - r.Y);
        }
        public static Vec3I operator* (Vec3I l, int scalar)
        {
            return new Vec3I(l.X * scalar, l.Y * scalar, l.Z * scalar);
        }
        public static int operator* (Vec3I l, Vec3I r)
        {
            return l.X * r.X + l.Y * r.Y + l.Z * r.Z;
        }
        public static implicit operator Vec3(Vec3I p)
        {
            return new Vec3(p.X, p.Y, p.Z);
        }
    }
}
