using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using FvCalculation.OperatorExpressions;

namespace FvCalculation.FunctionExpressions
{
    [FunctionName("sin")]
    class SinExpression : FunctionExpression
    {
        public override double Execute(Dictionary<string, double> variables)
        {
            return Math.Sin(this.Op.Execute(variables));
        }

        public override RawExpression Different(string variable)
        {
            return new MulExpression
            {
                Left = new CosExpression
                {
                    Op = this.Op,
                },
                Right = this.Op.Different(variable),
            };
        }

        public override Expression CompileInternal(Dictionary<string, System.Linq.Expressions.Expression> parameters)
        {
            return Expression.Call(typeof(Math).GetMethod("Sin", new Type[] { typeof(double) }), this.Op.CompileInternal(parameters));
        }
    }
}
