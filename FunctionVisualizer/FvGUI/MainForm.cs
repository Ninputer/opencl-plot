using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FvCalculation;
using System.Reflection;
using System.Globalization;
using System.Drawing.Imaging;
using System.Threading;

namespace FvGUI
{
    /*
     * exp(sin(x)+cos(y)) - sin(exp(x+y))
     * sin(sin(x)+cos(y)) - cos(sin(x*y)+cos(x))
     * sin(x^2+y^2) - cos(x*y)
     */
    public partial class MainForm : Form
    {
        private const int SuggestedUnitPixel = 100;

        private RawExpression function = null;
        private int unitPixels = 0;
        private double originX = 0;
        private double originY = 0;
        private Bitmap imageBuffer = null;

        private IGpuHelper gpu;

        private void TextBoxChanged()
        {
            RawExpression tempFunction = null;
            int tempUnitPixels = 0;
            double tempOriginX = 0;
            double tempOriginY = 0;

            try
            {
                tempFunction = RawExpression.Parse(textBoxFunction.Text);
            }
            catch (Exception e)
            {
                buttonRender.Enabled = false;
                labelErrorMessage.Text = "[Function]" + e.Message;
                return;
            }

            try
            {
                tempUnitPixels = int.Parse(textBoxUnitPixels.Text, NumberStyles.None);
            }
            catch (Exception e)
            {
                buttonRender.Enabled = false;
                labelErrorMessage.Text = "[UnitPixels]" + e.Message;
                return;
            }

            try
            {
                tempOriginX = double.Parse(textBoxOriginX.Text, NumberStyles.AllowDecimalPoint);
            }
            catch (Exception e)
            {
                buttonRender.Enabled = false;
                labelErrorMessage.Text = "[OriginX]" + e.Message;
                return;
            }

            try
            {
                tempOriginY = double.Parse(textBoxOriginY.Text, NumberStyles.AllowDecimalPoint);
            }
            catch (Exception e)
            {
                buttonRender.Enabled = false;
                labelErrorMessage.Text = "[OriginY]" + e.Message;
                return;
            }

            this.function = tempFunction;
            this.unitPixels = tempUnitPixels;
            this.originX = tempOriginX;
            this.originY = tempOriginY;
            buttonRender.Enabled = true;
            labelErrorMessage.Text = "(Ready)";
        }

        private void RebuildBuffer()
        {
            if (this.imageBuffer != null)
            {
                this.imageBuffer.Dispose();
            }
            Size size = panelImage.Size;
            this.imageBuffer = new Bitmap(size.Width, size.Height);
        }

        private void RenderAxis()
        {
            if (this.unitPixels == 0) return;
            int w = this.imageBuffer.Width;
            int h = this.imageBuffer.Height;
            using (Graphics g = Graphics.FromImage(this.imageBuffer))
            {
                g.FillRectangle(Brushes.White, 0, 0, w, h);

                int cx = (int)Math.Round(w / 2 - this.originX * this.unitPixels);
                int cy = (int)Math.Round(h / 2 - this.originY * this.unitPixels);
                int up = this.unitPixels;
                if (this.unitPixels < SuggestedUnitPixel)
                {
                    up = this.unitPixels * (SuggestedUnitPixel / this.unitPixels);
                    if (up < SuggestedUnitPixel)
                    {
                        up += this.unitPixels;
                    }
                }
                else if (this.unitPixels > SuggestedUnitPixel)
                {
                    up = this.unitPixels / (this.unitPixels / SuggestedUnitPixel);
                }
                double u = (double)up / this.unitPixels;

                int sx = cx - (cx / up * up);
                int sy = cy - (cy / up * up);
                for (int x = sx; x < w; x += up)
                {
                    g.DrawLine((x == cx ? Pens.Black : Pens.LightGray), x, 0, x, h);
                    g.DrawString(((x - cx) / up * u).ToString(), panelImage.Font, Brushes.Black, x, cy);
                }
                for (int y = sy; y < h; y += up)
                {
                    g.DrawLine((y == cy ? Pens.Black : Pens.LightGray), 0, y, w, y);
                    if (y != cy)
                    {
                        g.DrawString(((cy - y) / up * u).ToString(), panelImage.Font, Brushes.Black, cx, y);
                    }
                }

                g.DrawRectangle(Pens.Black, 0, 0, w - 1, h - 1);
            }
        }

        private void Fill(BitmapData data, double x, double y, int cx, int cy, int w, int h)
        {
            int ix = (int)Math.Round((x - this.originX) * this.unitPixels) + cx;
            int iy = (int)Math.Round((this.originY - y) * this.unitPixels) + cy;
            if (1 <= ix && ix <= w && 1 <= iy && iy <= h)
            {
                unsafe
                {
                    byte* color = (byte*)data.Scan0 + iy * data.Stride + ix * 3;
                    color[0] = 255;
                    color[1] = 0;
                    color[2] = 0;
                }
            }
        }

        public MainForm()
        {
            InitializeComponent();
            panelImage.GetType()
                .GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(panelImage, true, new object[] { });
            TextBoxChanged();
            RebuildBuffer();
            RenderAxis();
        }

        private void buttonRender_Click(object sender, EventArgs e)
        {
            RenderAxis();
            RenderAsnyc(RenderFunction);
        }

        private void textBoxFunction_TextChanged(object sender, EventArgs e)
        {
            TextBoxChanged();
        }

        private void textBoxUnitPixels_TextChanged(object sender, EventArgs e)
        {
            TextBoxChanged();
        }

        private void textBoxOriginX_TextChanged(object sender, EventArgs e)
        {
            TextBoxChanged();
        }

        private void textBoxOriginY_TextChanged(object sender, EventArgs e)
        {
            TextBoxChanged();
        }

        private void panelImage_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(this.imageBuffer, 0, 0);
        }

        private void panelImage_SizeChanged(object sender, EventArgs e)
        {
            RebuildBuffer();
            RenderAxis();
            panelImage.Refresh();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            OpenClSetting oclSetting = new OpenClSetting();
            oclSetting.ShowDialog(this);

            gpu = oclSetting.Gpu;
        }
    }
}
