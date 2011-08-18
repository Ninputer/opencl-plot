using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloo;

namespace FvGUI
{
    enum FPType
    {
        Single,
        FP64,
        FP64AMD
    }

    static class GpuHelperFactory
    {
        
        public static IGpuHelper CreateHelper(ComputePlatform platform, ComputeDevice device, FPType fptype)
        {
            ComputeContextPropertyList properties = new ComputeContextPropertyList(platform);
            var context = new ComputeContext(new[] { device }, properties, null, IntPtr.Zero);

            if (fptype == FPType.Single)
            {
                return new GpuHelper<float>(context, fptype);
            }
            else
            {
                return new GpuHelper<double>(context, fptype);
            }
        }
    }
}
