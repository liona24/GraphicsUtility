using System;

namespace GraphicsUtility
{
    [Serializable]
    public struct RectI
    {
        public readonly int L, T, R, B;
        public RectI(int l, int t, int r, int b)
        {
            L = l;
            T = t;
            R = r;
            B = b;
        }

        public int Area { get { return (R - L) * (B - T); } }
        public int Width { get { return R - L; } }
        public int Height { get { return B - T; } }
        public Vec2I Origin { get { return new Vec2I(L, T); } }

        public Vec2I Position { get { return new Vec2I(L, T); } }

        public bool IntersectsWith(RectI other)
        {
            return !(R < other.L || other.R < L || B < other.T || other.B < T);
        }
        public bool Contains(RectI small)
        {
            return (R >= small.R && L <= small.L && T <= small.T && B >= small.B);
        }
        public bool Contains(Vec2I p)
        {
            return (L <= p.X && R >= p.X && T <= p.Y && B >= p.Y);
        }
        public bool Continues(RectI other)
        {
            return (R == other.R && L == other.L && ( B == other.T || T == other.B )) ||
                (B == other.B && T == other.T && ( L == other.R || R == other.L ));
        }

        public override string ToString()
        {
            return string.Format("Rect L={0} T={1} R={2} B={3}", L, T, R, B);
        }

        public static RectI FromXYWH(int x, int y, int w, int h)
        {
            return new RectI(x, y, x+w, y+h);
        }
        public static implicit operator Rect(RectI r)
        {
            return new Rect(r.L, r.T, r.R, r.B);
        }
    }
    [Serializable]
    public struct Rect
    {
        public readonly double L, T, R, B;
        public Rect(double l, double t, double r, double b)
        {
            L = l;
            T = t;
            R = r;
            B = b;
        }

        public double Area { get { return (R - L) * (B - T); } }
        public double Width { get { return R - L; } }
        public double Height { get { return B - T; } }
        public Vec2 Origin { get { return new Vec2(L, T); } }

        public Vec2 Position { get { return new Vec2(L, T); } }

        public bool IntersectsWith(Rect other)
        {
            return !(R < other.L || other.R < L || B < other.T || other.B < T);
        }
        public bool Contains(Rect small)
        {
            return (R >= small.R && L <= small.L && T <= small.T && B >= small.B);
        }
        public bool Contains(Vec2 p)
        {
            return (L <= p.X && R >= p.X && T <= p.Y && B >= p.Y);
        }
        public bool Contains(double x, double y)
        {
            return (L <= x && R >= x && T <= y && B >= y);
        }

        public static Rect FromXYWH(double x, double y, double w, double h)
        {
            return new Rect(x, y, x+w, y+h);
        }
        public static explicit operator RectI(Rect r)
        {
            return new RectI((int)r.L, (int)r.T, (int)r.R, (int)r.B);
        }
    }
    [Serializable]
    public struct Cuboid
    {
        public readonly double L; //Left
        public readonly double T; //Top
        public readonly double N; //Near
        public readonly double R; //Right
        public readonly double B; //Bot
        public readonly double F; //Far

        public double Width { get { return R - L; } }
        public double Height { get { return B - T; } }
        public double Depth { get { return F - N; } }

        public double X { get { return L; } }
        public double Y { get { return T; } }
        public double Z { get { return N; } }
        
        public Cuboid(double l, double t, double n, double r, double b, double f)
        {
            L = l;
            T = t;
            N = n;
            R = r;
            B = b;
            F = f;
        }
        public Cuboid(Vec3 pos, Vec3 size)
        {
            L = pos.X;
            T = pos.Y;
            N = pos.Z;
            R = pos.X + size.X;
            B = pos.Y + size.Y;
            F = pos.Z + size.Z;
        }

        public bool Contains(Cuboid small)
        {
            return (L <= small.L && R >= small.R &&
                T <= small.T && B >= small.B &&
                N <= small.N && F >= small.F);
        }
        public bool Contains(Vec3 point)
        {
            return (L <= point.X && R >= point.X &&
                T <= point.Y && B >= point.Y &&
                N <= point.Z && F >= point.Z);
        }

        public bool IntersectsWith(Cuboid other)
        {
            return !(R < other.L || other.R < L || B < other.T || other.B < T ||
                N < other.F || other.N < F);
        }

    }
}
