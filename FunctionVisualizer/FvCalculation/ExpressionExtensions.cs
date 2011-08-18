using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FvCalculation
{
    public static class ExpressionExtensions
    {
        public static double Execute(this RawExpression e, string name, double value)
        {
            return e.Execute(new Dictionary<string, double> { { name, value } });
        }

        public static RawExpression Apply(this RawExpression e, string name, double value)
        {
            return e.Apply(new Dictionary<string, double> { { name, value } });
        }

        public static double Solve(this RawExpression e, string name, double start, int maxCount = 1000)
        {
            RawExpression f = e.Simplify();
            RawExpression df = f.Different(name).Simplify();
            return f.Solve(df, name, start, maxCount);
        }

        public static double Solve(this RawExpression f, RawExpression df, string name, double start, int maxCount = 1000)
        {
            return Solve((x) => f.Execute(name, x), (x) => df.Execute(name, x), start, maxCount);
        }

        public static double Solve(this Func<double, double> f, Func<double, double> df, double start, int maxCount = 1000)
        {
            for (int i = 0; i < maxCount; i++)
            {
                double result = f(start);
                if (-RawExpression.ZeroNumber <= result && result <= RawExpression.ZeroNumber)
                {
                    return start;
                }

                double d = df(start);
                if (-RawExpression.ZeroNumber <= d && d <= RawExpression.ZeroNumber)
                {
                    return double.NaN;
                }
                else
                {
                    start -= result / d;
                }
            }
            return double.NaN;
        }
    }
}
