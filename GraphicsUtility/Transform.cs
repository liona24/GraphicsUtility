using System;

namespace GraphicsUtility
{
    public class Transform : Matrix4 
    {
        public Transform(double c)
            : base(c)
        { }
        public Transform(double[] values)
            : base(values)
        { }
        public Transform(double[][] values)
            : base(values)
        { }
        public Transform(Matrix4 mat)
            : base(mat)
        { }

        public void LoadIdentity()
        {
            v = new double[] { 1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1, };
        }
        public new object Clone()
        {
            var nMat = (Matrix4)base.Clone();
            return new Transform(nMat);
        }

        public void Rotate(double angle, double x, double y, double z)
        {
            double c = Math.Cos(angle);
            double s = Math.Sin(angle);

            var rot = new Matrix4(new double[] { x * x * (1 - c) + c, x * y * (1 - c) - z * s, x * z * (1 - c) + y * s, 0,
                                                        x * y * (1 - c) + z * s, y * y * (1 - c) + c, y * z * (1 - c) - x * s, 0,
                                                        x * z * (1 - c) - y * s, y * z * (1 - c) + x * s, z * z * (1 - c) + c, 0,
                                                        0, 0, 0, 1});
            Multiply(rot);
        }

        public void RotateX(double angle)
        {
            //x = 1, y = 0, z = 0
            double c = Math.Cos(angle);
            double s = Math.Sin(angle);

            var rotX = new Matrix4(new double[] { (1 - c) + c, 0, 0, 0,
                                                        0, c, -s, 0,
                                                        0, s, c, 0,
                                                        0, 0, 0, 1});
            Multiply(rotX);
        }

        public void RotateY(double angle)
        {
            //x = 0, y = 1, z = 0
            double c = Math.Cos(angle);
            double s = Math.Sin(angle);

            var rotY = new Matrix4(new double[] { c, 0, s, 0,
                                                        0, (1 - c) + c, 0, 0,
                                                        -s, 0, c, 0,
                                                        0, 0, 0, 1});
            Multiply(rotY);
        }

        public void RotateZ(double angle)
        {
            //x = 0, y = 0, z = 1
            double c = Math.Cos(angle);
            double s = Math.Sin(angle);

            var rotZ = new Matrix4(new double[] { c, -s, 0, 0,
                                                        s, c, 0, 0,
                                                        0, 0, (1 - c) + c, 0,
                                                        0, 0, 0, 1});
            Multiply(rotZ);
        }

        public void Translate(double x, double y, double z)
        {
            var trans = new Matrix4( new double[] { 1, 0, 0, x,
                                                        0, 1, 0, y,
                                                        0, 0, 1, z,
                                                        0, 0, 0, 1 });
            Multiply(trans);
        }
        public void Scale(double u)
        {
            Scale(u, u, u);
        }
        public void Scale(double x, double y, double z)
        {
            var sca = new Matrix4( new double[] { x, 0, 0, 0,
                                                        0, y, 0, 0,
                                                        0, 0, z, 0,
                                                        0, 0, 0, 1 });
            Multiply(sca);
        }

        public void Multiply(Vec4[] src, Vec4[] dst)
        {
            for (int i = 0; i < src.Length; i++)
                dst[i] = this * src[i];
        }
    }
}
