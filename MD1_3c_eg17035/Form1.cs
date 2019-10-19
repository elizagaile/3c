using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MD1_3c_eg17035
{
    // Izstrādāt datorprogrammu, kas realizē taisnes nogriežņa un patvaļīga slīpuma taisnstūra
    // parametrisko apcirpšanu.


    public partial class OffsetForm : Form
    {
        Point moving = new Point();

        private PointF[] P = new PointF[4];
        PointF start = new PointF();
        PointF end = new PointF();

        public OffsetForm()
        {
            InitializeComponent();

            P[0] = new PointF(50, 100);
            P[1] = new PointF(300, 100);
            P[2] = new PointF(300, 200);
            P[3] = new PointF(50, 200);

            start = new PointF(10, 10);
            end = new PointF(300, 300);

            moving = new Point(-1, -1);
        }

        private void Canva_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // grafiski attēlo līnijas gludākas

            e.Graphics.DrawLines(Pens.Black, P);
            e.Graphics.DrawLine(Pens.Black, P[3], P[0]);

            e.Graphics.FillEllipse(Brushes.Black, start.X - 2, start.Y - 2, 2 * 2, 2 * 2);
            e.Graphics.FillEllipse(Brushes.Black, end.X - 2, end.Y - 2, 2 * 2, 2 * 2);

            PointF realStart = GetPointFromT(GetRealCrossPoint(0));
            PointF realEnd = GetPointFromT(GetRealCrossPoint(1));

            e.Graphics.DrawLine(Pens.Blue, realStart, realEnd);

            lbLineStart.Text = "P1: (" + start.X + ", " + start.Y + ")";
            lbLineEnd.Text = "P1: (" + end.X + ", " + end.Y + ")";

            lbQuad1.Text = "P1: (" + P[0].X + ", " + P[0].Y + ")";
            lbQuad2.Text = "P2: (" + P[1].X + ", " + P[1].Y + ")";
            lbQuad3.Text = "P3: (" + P[2].X + ", " + P[2].Y + ")";
            lbQuad4.Text = "P4: (" + P[3].X + ", " + P[3].Y + ")";
        }

        private double GetRealCrossPoint(int type)
        {
            List<double> tIe = new List<double>();
            List<double> tIz = new List<double>();

            PointF lineVector = new PointF
            {
                X = end.X - start.X,
                Y = end.Y - start.Y
            };

            for (int i = 0; i < 4; i++)
            {
                PointF normal = new PointF
                {
                    X = P[i].Y - P[(i + 1) % 4].Y,
                    Y = P[(i + 1) % 4].X - P[i].X
                };

                PointF startToP = new PointF
                {
                    X = P[i].X - start.X,
                    Y = P[i].Y - start.Y
                };


                double num = DotProduct(normal, startToP);
                double den = DotProduct(normal, lineVector);

                if (den > 0)
                {
                    tIe.Add(num / den);
                }
                else if (den < 0)
                {
                    tIz.Add(num / den);
                }
            }

            tIe.Add(0);
            tIe.Sort();
            tIz.Add(1);
            tIz.Sort();

            double tStart = tIe[tIe.Count - 1];
            double tEnd = tIz[0];

            if(tStart > tEnd)
            {
                return -1;
            }

            else if (type == 0)
            {
                return tStart;
            }

            else
            {
                return tEnd;
            }
        }

        private double DotProduct(PointF a, PointF b)
        {
            return (a.X * b.X + a.Y * b.Y);
        }

        private PointF GetPointFromT(double t)
        {
            double x = start.X + (end.X - start.X) * t;
            double y = start.Y + (end.Y - start.Y) * t;

            return new PointF((float)x, (float)y);
        }

        private Point FindLocalPoint(PointF mouseLocation)
        {
            const int localRadius = 7; // apkārtnes rādiusa izmērs pikseļos

            if (GetLength(mouseLocation, start) < localRadius)
            {
                return new Point(0, 0);
            }

            if (GetLength(mouseLocation, end) < localRadius)
            {
                return new Point(0, 1);
            }

            for (int i = 0; i < P.Length; i++)
            {
                if (GetLength(mouseLocation, P[i]) < localRadius)
                {
                    return new Point(1, i);
                }
            }
            return new Point(-1, -1);
        }

        private float GetLength(PointF firstPoint, PointF secondPoint)
        {
            return (float)Math.Sqrt(Math.Pow(firstPoint.X - secondPoint.X, 2) + Math.Pow(firstPoint.Y - secondPoint.Y, 2));
        }

        private void Canva_MouseDown(object sender, MouseEventArgs e)
        {
            moving = FindLocalPoint(e.Location);
        }

        private void Canva_MouseMove(object sender, MouseEventArgs e)
        {
            if(moving.X == 0)
            {
                if (moving.Y == 0)
                {
                    start = e.Location;
                }
                else if (moving.Y == 1)
                {
                    end = e.Location;
                }
                Canva.Invalidate();
            }

            else if (moving.X == 1)
            {
                P[moving.Y] = e.Location;
                Canva.Invalidate();
            }
        }

        private void Canva_MouseUp(object sender, MouseEventArgs e)
        {
            moving = new Point(-1, -1);
        }

    }
}
