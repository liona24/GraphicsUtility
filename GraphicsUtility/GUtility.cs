using System.Collections.Generic;

namespace GraphicsUtility
{
    public static class GUtility
    {
        /// <summary>
        /// Checks whether the given point lays within the given triangle
        /// </summary>
        /// <param name="p">The point</param>
        /// <param name="t1">The first vertex of the triangle</param>
        /// <param name="t2">The second vertex of the triangle</param>
        /// <param name="t3">The third vertex of the triangle</param>
        /// <returns>Returns true if the point is within, else false</returns>
        public static bool PointInTri(Vec2 p, Vec2 t1,
                                        Vec2 t2, Vec2 t3)
        {
            double a = ((t2.Y - t3.Y) * (p.X - t3.X) + (t3.X - t2.X) * (p.Y - t3.Y)) /
                   ((t2.Y - t3.Y) * (t1.X - t3.X) + (t3.X - t2.X) * (t1.Y - t3.Y));
            double b = ((t3.Y - t1.Y) * (p.X - t3.X) + (t1.X - t3.X) * (p.Y - t3.Y)) /
                   ((t2.Y - t3.Y) * (t1.X - t3.X) + (t3.X - t2.X) * (t1.Y - t3.Y));

            return a > 0 && b > 0 && 1 - a - b > 0;
        }

        /// <summary>
        /// Checks whether the given point lays within the given polygon
        /// </summary>
        /// <param name="point">The point</param>
        /// <param name="poly">The vertices of the polygon</param>
        /// <returns>Returns true if the point is within, else false</returns>
        public static bool PointInPoly(Vec2 point, Vec2[] poly)
        {
            int res = rayIntersection(point, poly[poly.Length - 1].X, 
                                            poly[poly.Length - 1].Y, 
                                            poly[0].X, 
                                            poly[0].Y);
            for (int i = 0; i < poly.Length - 1; i++)
            {
                res *=  rayIntersection(point, poly[i].X, 
                                            poly[i].Y, 
                                            poly[i + 1].X, 
                                            poly[i + 1].Y);
            }

            return res > 0;
        }

        // Used for the point-in-polygon check
        private static int rayIntersection(Vec2 point, double x1, double y1, double x2, double y2)
        {
            if (point.Y == y1 && y1 == y2)
            {
                if (point.X <= x2 && x1 <= point.X || point.X <= x1 && x2 <= point.X)
                    return 1;
                return -1;
            }
            if (y1 > y2)
            {
                double tmp = y1;
                y1 = y2;
                y2 = tmp;
                tmp = x1;
                x1 = x2;
                x2 = tmp;
            }
            if (point.Y <= y1 || point.Y >= y2)
                return 1;
            double delta = (x1 - point.X) * (y2 - point.Y) - (y1 - point.Y) * (x2 - point.X);
            if (delta >= 0)
                return -1;
            return 1;
        }

        
        /// <summary>
        /// Calculates the point of intersection of two lines 
        /// </summary>
        /// <param name="from1">One point on the first line</param>
        /// <param name="to1">Another point on the first line</param>
        /// <param name="from2">One point on the second line</param>
        /// <param name="to2">Another point on the second line</param>
        public static Vec2 IntersectionLineLine(Vec2 from1, Vec2 to1, Vec2 from2, Vec2 to2)
        {
            var dir1 = to1 - from1;
            var m = new Matrix2(-dir1.X, to2.X - from2.X, -dir1.Y, to2.Y - from2.Y);
            m.Inverse();
            var res = m * (from1 - from2);
            return new Vec2(from1.X + dir1.X * res.X, from1.Y + dir1.Y * res.X);
        }

        /// <summary>
        /// Returns point of intersection between a plane and a line:
        /// planeOrigin + a * p + b * q = from + (to - from) * c
        /// </summary>
        /// <param name="planeOrigin">Plane support vector</param>
        /// <param name="p">plane direction 1</param>
        /// <param name="q">plane direction 2</param>
        /// <param name="from">Line support vector</param>
        /// <param name="to">Line through</param>
        /// <returns>Returns the point of intersection, empty point if no intersection occurs</returns>
        public static Vec3 IntersectionPlaneLine(Vec3 planeOrigin, Vec3 p, Vec3 q, Vec3 from, Vec3 to)
        {
            var dir = to - from;
            var m = new Matrix3(planeOrigin.X - p.X, planeOrigin.X - q.X, dir.X, 
                                planeOrigin.Y - p.Y, planeOrigin.Y - q.Y, dir.Y,
                                planeOrigin.Z - p.Z, planeOrigin.Z - q.Z, dir.Z);
            m.Inverse();
            var res = m * (planeOrigin - from);
            return new Vec3(from.X + dir.X * res.Z, from.Y + dir.Y * res.Z, from.Z + dir.Z * res.Z);
        }

        /// <summary>
        /// Tessellates the given polygon with triangles
        /// </summary>
        /// <param name="poly">The polygon to be tessellated</param>
        /// <returns>Returns the calculated triangles</returns>
        public static Vec2[][] Polygon2Triangles(Vec2[] poly)
        {
            var tris = new Vec2[poly.Length - 2][];
            int k = 0;

            var list = new LinkedList<Vec2>();
            for (int i = 0; i < poly.Length; i++)
                list.AddLast(poly[i]);

            var node = list.First;
            while (node.Next != null &&
                    node.Next.Next != null)
            {
                var p1 = node.Value;
                var p2 = node.Next.Value;
                var p3 = node.Next.Next.Value;
                bool doRemove = true;
                var forward = node.Next.Next;
                while (forward.Next != null)
                {
                    forward = forward.Next;
                    if (PointInTri(forward.Value, p1, p2, p3))
                    {
                        doRemove = false;
                        break;
                    }
                }
                if (doRemove)
                {
                    var backward = node;
                    while (backward.Previous != null)
                    {
                        backward = backward.Previous;
                        if (PointInTri(backward.Value, p1, p2, p3))
                        {
                            doRemove = false;
                            break;
                        }
                    }

                    if (doRemove)
                    {
                        list.Remove(node.Next);
                        tris[k++] = new Vec2[] { p1, p2, p3 };
                    }
                }
                node = node.Next;
            }
            return tris;
        }

        /// <summary>
        /// Tessellates the given polygon with triangles
        /// </summary>
        /// <param name="poly">The polygon to be tessellated</param>
        /// <returns>Returns the calculated triangles</returns>
        public static Vec2I[][] Polygon2Triangles(Vec2I[] poly)
        {
            var tris = new Vec2I[poly.Length - 2][];
            int k = 0;

            var list = new LinkedList<Vec2I>();
            for (int i = 0; i < poly.Length; i++)
                list.AddLast(poly[i]);

            var node = list.First;
            while (node.Next != null &&
                    node.Next.Next != null)
            {
                var p1 = node.Value;
                var p2 = node.Next.Value;
                var p3 = node.Next.Next.Value;
                bool doRemove = true;
                var forward = node.Next.Next;
                while (forward.Next != null)
                {
                    forward = forward.Next;
                    if (PointInTri(forward.Value, p1, p2, p3))
                    {
                        doRemove = false;
                        break;
                    }
                }
                if (doRemove)
                {
                    var backward = node;
                    while (backward.Previous != null)
                    {
                        backward = backward.Previous;
                        if (PointInTri(backward.Value, p1, p2, p3))
                        {
                            doRemove = false;
                            break;
                        }
                    }

                    if (doRemove)
                    {
                        list.Remove(node.Next);
                        tris[k++] = new Vec2I[] { p1, p2, p3 };
                    }
                }
                node = node.Next;
            }
            return tris;
        }

        /// <summary>
        /// Returns the boundaries of a given collection of vectors
        /// </summary>
        public static Rect GetBounds(Vec2[] coll)
        {
            double l = double.MaxValue;
            double r = double.MinValue;
            double t = double.MaxValue;
            double b = double.MinValue;
            for (int i = 0; i < coll.Length; i++)
            {
                if (coll[i].X < l)
                    l = coll[i].X;
                if (coll[i].X > r)
                    t = coll[i].Y;
                if (coll[i].Y > b)
                    b = coll[i].Y;
            }
            return new Rect(l, t, r, b);
        }

        /// <summary>
        /// Returns the boundaries of a given collection of vectors
        /// </summary>
        public static RectI GetBounds(Vec2I[] coll)
        {
            int l = int.MaxValue;
            int r = int.MinValue;
            int t = int.MaxValue;
            int b = int.MinValue;
            for (int i = 0; i < coll.Length; i++)
            {
                if (coll[i].X < l)
                    l = coll[i].X;
                else if (coll[i].X > r)
                    r = coll[i].X;
                if (coll[i].Y < t)
                    t = coll[i].Y;
                else if (coll[i].Y > b)
                    b = coll[i].Y;
            }
            return new RectI(l, t, r, b);
        }
    }
}
