using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using FvCalculation.OperatorExpressions;
using FvCalculation.PrimitiveExpressions;

namespace FvCalculation.FunctionExpressions
{
    [FunctionName("cot")]
    class CotExpression : FunctionExpression
    {
        public override double Execute(Dictionary<string, double> variables)
        {
            return 1 / Math.Tan(this.Op.Execute(variables));
        }

        public override RawExpression Different(string variable)
        {
            return new MulExpression
            {
                Left = new NegExpression
                {
                    Op = new PowerExpression
                    {
                        Left = new CscExpression
                        {
                            Op = this.Op,
                        },
                        Right = new NumberExpression
                        {
                            Number = 2,
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
                Expression.Call(typeof(Math).GetMethod("Tan", new Type[] { typeof(double) }), this.Op.CompileInternal(parameters))
                );
        }

        public override string ToCode()
        {
            return "(_F(1.0)/tan(" + this.Op.ToCode() + "))";
        }
    }
}
