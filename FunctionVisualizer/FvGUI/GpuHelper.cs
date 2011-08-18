using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloo;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;


namespace FvGUI
{
    class GpuHelper<TFP> : FvGUI.IGpuHelper where TFP : struct
    {
        private ComputeContext context;
        private ComputeCommandQueue commands;
        ICollection<ComputeEventBase> events = new Collection<ComputeEventBase>();

        FPType fpType;

        public GpuHelper(ComputeContext context, FPType fptype)
        {
            this.context = context;
            commands = new ComputeCommandQueue(context, context.Devices[0], ComputeCommandQueueFlags.None);
            this.fpType = fptype;
        }

        public void FillPoints(int imageWidth, int imageHeight, int unit, double originx, double originy, string funcCode, string funcdxCode, string funcdyCode, double[, ,] points)
        {
            var gpuResult = Compute(imageWidth, imageHeight, unit, (float)originx, (float)originy, funcCode, funcdxCode, funcdyCode);

            int w = imageWidth - 2;
            int h = imageHeight - 2;
            int cx = imageWidth / 2;
            int cy = imageHeight / 2;

            for (int y = 1; y <= h; y++)
            {
                double py = originy + (double)(y - cy) / unit;
                for (int x = 1; x <= w; x++)
                {
                    double px = originx + (double)(cx - x) / unit;

                    int loc = (y - 1) * w + (x - 1);
                    points[y - 1, x - 1, 0] = Convert.ToDouble(gpuResult.Item1[loc]);
                    points[y - 1, x - 1, 1] = py;
                    points[y - 1, x - 1, 2] = px;
                    points[y - 1, x - 1, 3] = Convert.ToDouble(gpuResult.Item2[loc]);
                }
            }
        }

        private Tuple<TFP[], TFP[]> Compute(int imageWidth, int imageHeight, int unit, float originx, float originy, string funcCode, string funcdxCode, string funcdyCode)
        {

            int w = imageWidth - 2;
            int h = imageHeight - 2;
            int cx = imageWidth / 2;
            int cy = imageHeight / 2;

            int bufferSize = w * h;

            ComputeBuffer<TFP> points = new ComputeBuffer<TFP>(context, ComputeMemoryFlags.WriteOnly, bufferSize);

            string source = Encoding.ASCII.GetString(Properties.Resources.function_vis);

            source = source.Replace("{func}", funcCode);
            source = source.Replace("{dfuncdx}", funcdxCode);
            source = source.Replace("{dfuncdy}", funcdyCode);

            if (fpType == FPType.FP64AMD)
            {
                source = "#define AMDFP64\n" + source;
            }
            else if (fpType == FPType.FP64)
            {
                source = "#define FP64\n" + source;
            }

            ComputeProgram program = new ComputeProgram(context, source);
            try
            {
                program.Build(null, null, null, IntPtr.Zero);
            }
            catch (Exception)
            {
                var log = program.GetBuildLog(context.Devices[0]);
                Debugger.Break();
            }

            ComputeKernel kernelx = program.CreateKernel("ComputeX");
            ComputeKernel kernely = program.CreateKernel("ComputeY");

            TFP[] pointsArrayX = RunKernal(unit, w, h, cx, cy, originx, originy, bufferSize, points, kernelx);
            TFP[] pointsArrayY = RunKernal(unit, w, h, cx, cy, originx, originy, bufferSize, points, kernely);

            kernelx.Dispose();
            kernely.Dispose();
            program.Dispose();
            points.Dispose();

            return Tuple.Create(pointsArrayX, pointsArrayY);
        }

        private TFP[] RunKernal(int unit, int w, int h, int cx, int cy, float originx, float originy, int bufferSize, ComputeBuffer<TFP> points, ComputeKernel kernel)
        {
            kernel.SetMemoryArgument(0, points);
            kernel.SetValueArgument(1, unit);
            kernel.SetValueArgument(2, w);
            kernel.SetValueArgument(3, cx);
            kernel.SetValueArgument(4, cy);
            kernel.SetValueArgument(5, originx);
            kernel.SetValueArgument(6, originy);

            // BUG: ATI Stream v2.2 crash if event list not null.
            commands.Execute(kernel, null, new long[] { w, h }, null, events);
            //commands.Execute(kernel, null, new long[] { count }, null, null);

            TFP[] pointsArray = new TFP[bufferSize];
            GCHandle arrCHandle = GCHandle.Alloc(pointsArray, GCHandleType.Pinned);

            commands.Read(points, true, 0, bufferSize, arrCHandle.AddrOfPinnedObject(), events);

            arrCHandle.Free();
            return pointsArray;
        }
    }
}
