using System;
namespace FvGUI
{
    interface IGpuHelper
    {
        void FillPoints(int imageWidth, int imageHeight, int unit, double originx, double originy, string funcCode, string funcdxCode, string funcdyCode, double[, ,] points);
    }
}
