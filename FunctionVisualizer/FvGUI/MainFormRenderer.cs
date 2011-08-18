using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FvCalculation;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace FvGUI
{
    partial class MainForm
    {
        private void UpdateMessage(string message)
        {
            Invoke(new MethodInvoker(() =>
            {
                labelErrorMessage.Text = message;
            }));
        }

        private void RenderAsnyc(Action renderer)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(o =>
            {
                Invoke(new MethodInvoker(() =>
                {
                    textBoxFunction.Enabled = false;
                    textBoxUnitPixels.Enabled = false;
                    textBoxOriginX.Enabled = false;
                    textBoxOriginY.Enabled = false;
                    buttonRender.Enabled = false;
                }));

                renderer();

                Invoke(new MethodInvoker(() =>
                {
                    textBoxFunction.Enabled = true;
                    textBoxUnitPixels.Enabled = true;
                    textBoxOriginX.Enabled = true;
                    textBoxOriginY.Enabled = true;
                    buttonRender.Enabled = true;
                    panelImage.Refresh();
                }));
            }), null);
        }

        private void RenderFunction()
        {
            int w = this.imageBuffer.Width - 2;
            int h = this.imageBuffer.Height - 2;
            int cx = this.imageBuffer.Width / 2;
            int cy = this.imageBuffer.Height / 2;
            double[, ,] points = new double[h, w, 4];

            RawExpression efx = this.function.Simplify();
            var edfdx = efx.Different("x").Simplify();
            var edfdy = efx.Different("y").Simplify();

            var fx = efx.ToCode();
            var dfdx = edfdx.ToCode();
            var dfdy = edfdy.ToCode();

            UpdateMessage("Computing...");

            gpu.FillPoints(this.imageBuffer.Width, this.imageBuffer.Height, this.unitPixels, this.originX, this.originY, fx, dfdx, dfdy, points);

            //int done = 0;
            //int max = w + h;
            //int total = Environment.ProcessorCount * 8;
            //Parallel.For(0, total, (i) =>
            //{
            //    int dh = (h + total - h % total) / total;
            //    int dw = (w + total - w % total) / total;

            //    int starty = 1 + i * dh;
            //    int endy = Math.Min(h + 1, starty + dh);
            //    int startx = 1 + i * dw;
            //    int endx = Math.Min(w + 1, startx + dw);
            //    int loops = (endy - starty + 1) + (endx - startx + 1);

            //    for (int y = starty; y < endy; y++)
            //    {
            //        double py = this.originY + (double)(y - cy) / this.unitPixels;
            //        RawExpression efx = this.function.Apply("y", py).Simplify();
            //        RawExpression edfx = efx.Different("x").Simplify();
            //        Func<double, double> fx = efx.Compile("x");
            //        Func<double, double> dfx = edfx.Compile("x");

            //        for (int x = 1; x <= w; x++)
            //        {
            //            points[y - 1, x - 1, 0] = fx.Solve(dfx, points[y - 1, x - 1, 0]);
            //        }

            //        int current = Interlocked.Increment(ref done);
            //        if (current % 10 == 0)
            //        {
            //            UpdateMessage(current.ToString() + "/" + max.ToString());
            //        }
            //    }

            //    for (int x = startx; x < endx; x++)
            //    {
            //        double px = this.originX + (double)(cx - x) / this.unitPixels;
            //        RawExpression efy = this.function.Apply("x", px).Simplify();
            //        RawExpression edfy = efy.Different("y").Simplify();
            //        Func<double, double> fy = efy.Compile("y");
            //        Func<double, double> dfy = edfy.Compile("y");

            //        for (int y = 1; y <= h; y++)
            //        {
            //            points[y - 1, x - 1, 3] = fy.Solve(dfy, points[y - 1, x - 1, 3]);
            //        }

            //        int current = Interlocked.Increment(ref done);
            //        if (current % 10 == 0)
            //        {
            //            UpdateMessage(current.ToString() + "/" + max.ToString());
            //        }
            //    }
            //});

            UpdateMessage("Rendering...");

            BitmapData data = this.imageBuffer.LockBits(new Rectangle(0, 0, this.imageBuffer.Width, this.imageBuffer.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            for (int y = 1; y <= h; y++)
            {
                for (int x = 1; x <= w; x++)
                {
                    double x1 = points[y - 1, x - 1, 0];
                    double y1 = points[y - 1, x - 1, 1];
                    double x2 = points[y - 1, x - 1, 2];
                    double y2 = points[y - 1, x - 1, 3];

                    if (!double.IsInfinity(x1) && !double.IsNaN(x1))
                    {
                        Fill(data, x1, y1, cx, cy, w, h);
                    }
                    if (!double.IsInfinity(y2) && !double.IsNaN(y2))
                    {
                        Fill(data, x2, y2, cx, cy, w, h);
                    }
                }
            }
            this.imageBuffer.UnlockBits(data);

            UpdateMessage("(Ready)");
        }
    }
}
