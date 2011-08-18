using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using FvCalculation.OperatorExpressions;

namespace FvCalculation.FunctionExpressions
{
    [FunctionName("csc")]
    class CscExpression : FunctionExpression
    {
        public override double Execute(Dictionary<string, double> variables)
        {
            return 1 / Math.Sin(this.Op.Execute(variables));
        }

        public override RawExpression Different(string variable)
        {
            return new MulExpression
            {
                Left = new NegExpression
                {
                    Op = new MulExpression
                    {
                        Left = new CscExpression
                        {
                            Op = this.Op,
                        },
                        Right = new CotExpression
                        {
                            Op = this.Op,
                        },
                    },
                },
                Right = this.Op.Different(variable),
            };
        }

        public override Expression CompileInternal(Dictionary<string, System.Linq.Expressions.Expression> parameters)
        {
            return Expression.Divide(
                Expression.Constant(1.0),
                Expression.Call(typeof(Math).GetMethod("Sin", new Type[] { typeof(double) }), this.Op.CompileInternal(parameters))
                );
        }

        public override string ToCode()
        {
            return "(_F(1.0)/sin(" + this.Op.ToCode() + "))";
        }
    }
}
