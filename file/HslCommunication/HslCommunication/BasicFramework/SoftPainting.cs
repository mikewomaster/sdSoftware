namespace HslCommunication.BasicFramework
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class SoftPainting
    {
        public static float ComputePaintLocationY(int max, int min, int height, int value)
        {
            return (height - ((((value - min) * 1f) / ((float) (max - min))) * height));
        }

        public static float ComputePaintLocationY(float max, float min, int height, float value)
        {
            return (height - (((value - min) / (max - min)) * height));
        }

        public static Bitmap GetGraphicFromArray(Paintdata[] array, int width, int height, GraphicRender graphic)
        {
            if ((width < 10) && (height < 10))
            {
                throw new ArgumentException("长宽不能小于等于10");
            }
            Enumerable.Max<Paintdata>(array, (Func<Paintdata, int>) (m => m.Count));
            Action<Paintdata[], GraphicRender, Graphics> action = delegate (Paintdata[] array1, GraphicRender graphic1, Graphics g) {
            };
            return null;
        }

        public static Bitmap GetGraphicFromArray(int[] array, int width, int height, int degree, Color lineColor)
        {
            if ((width < 10) && (height < 10))
            {
                throw new ArgumentException("长宽不能小于等于10");
            }
            int max = array.Max();
            int min = 0;
            int length = array.Length;
            StringFormat sf = new StringFormat {
                Alignment = StringAlignment.Far
            };
            Pen penDash = new Pen(Color.LightGray, 1f) {
                DashStyle = DashStyle.Custom
            };
            penDash.DashPattern = new float[] { 5f, 5f };
            Font font = new Font("宋体", 9f);
            Bitmap image = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(image);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(Color.White);
            int left = 60;
            int right = 8;
            int num6 = 8;
            int down = 8;
            int num8 = (width - left) - right;
            int num9 = (height - num6) - down;
            Rectangle rectangle = new Rectangle(left - 1, num6 - 1, num8 + 1, num9 + 1);
            g.DrawLine(Pens.Gray, left - 1, num6, (left + num8) + 1, num6);
            g.DrawLine(Pens.Gray, (int) (left - 1), (int) ((num6 + num9) + 1), (int) ((left + num8) + 1), (int) ((num6 + num9) + 1));
            g.DrawLine(Pens.Gray, (int) (left - 1), (int) (num6 - 1), (int) (left - 1), (int) ((num6 + num9) + 1));
            PaintCoordinateDivide(g, Pens.DimGray, penDash, font, Brushes.DimGray, sf, degree, max, min, width, height, left, right, num6, down);
            PointF[] points = new PointF[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                points[i].X = (((num8 * 1f) / ((float) (array.Length - 1))) * i) + left;
                points[i].Y = (ComputePaintLocationY(max, min, num9, array[i]) + num6) + 1f;
            }
            Pen pen = new Pen(lineColor);
            g.DrawLines(pen, points);
            pen.Dispose();
            penDash.Dispose();
            font.Dispose();
            sf.Dispose();
            g.Dispose();
            return image;
        }

        public static void PaintCoordinateDivide(Graphics g, Pen penLine, Pen penDash, Font font, Brush brush, StringFormat sf, int degree, int max, int min, int width, int height, [Optional, DefaultParameterValue(60)] int left, [Optional, DefaultParameterValue(8)] int right, [Optional, DefaultParameterValue(8)] int up, [Optional, DefaultParameterValue(8)] int down)
        {
            for (int i = 0; i <= degree; i++)
            {
                int num2 = (((max - min) * i) / degree) + min;
                int num3 = (((int) ComputePaintLocationY(max, min, (height - up) - down, num2)) + up) + 1;
                g.DrawLine(penLine, left - 1, num3, left - 4, num3);
                if (i > 0)
                {
                    g.DrawLine(penDash, left, num3, width - right, num3);
                }
                g.DrawString(num2.ToString(), font, brush, new Rectangle(-5, num3 - (font.Height / 2), left, font.Height), sf);
            }
        }

        public static void PaintTriangle(Graphics g, Brush brush, Point point, int size, GraphDirection direction)
        {
            Point[] points = new Point[4];
            if (direction == GraphDirection.Ledtward)
            {
                points[0] = new Point(point.X, point.Y - size);
                points[1] = new Point(point.X, point.Y + size);
                points[2] = new Point(point.X - (2 * size), point.Y);
            }
            else if (direction == GraphDirection.Rightward)
            {
                points[0] = new Point(point.X, point.Y - size);
                points[1] = new Point(point.X, point.Y + size);
                points[2] = new Point(point.X + (2 * size), point.Y);
            }
            else if (direction == GraphDirection.Upward)
            {
                points[0] = new Point(point.X - size, point.Y);
                points[1] = new Point(point.X + size, point.Y);
                points[2] = new Point(point.X, point.Y - (2 * size));
            }
            else
            {
                points[0] = new Point(point.X - size, point.Y);
                points[1] = new Point(point.X + size, point.Y);
                points[2] = new Point(point.X, point.Y + (2 * size));
            }
            points[3] = points[0];
            g.FillPolygon(brush, points);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SoftPainting.<>c <>9 = new SoftPainting.<>c();
            public static Func<Paintdata, int> <>9__5_0;
            public static Action<Paintdata[], GraphicRender, Graphics> <>9__5_1;

            internal int <GetGraphicFromArray>b__5_0(Paintdata m)
            {
                return m.Count;
            }

            internal void <GetGraphicFromArray>b__5_1(Paintdata[] array1, GraphicRender graphic1, Graphics g)
            {
            }
        }
    }
}

