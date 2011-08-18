using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using FvCalculation.OperatorExpressions;

namespace FvCalculation.FunctionExpressions
{
    [FunctionName("sec")]
    class SecExpression : FunctionExpression
    {
        public override double Execute(Dictionary<string, double> variables)
        {
            return 1/Math.Cos(this.Op.Execute(variables));
        }

        public override RawExpression Different(string variable)
        {
            return new MulExpression
            {
                Left = new MulExpression
                {
                    Left = new SecExpression
                    {
                        Op = this.Op,
                    },
                    Right = new TanExpression
                    {
                        Op = this.Op,
                    },
                },
                Right = this.Op.Different(variable),
            };
        }

        public override Expression CompileInternal(Dictionary<string, System.Linq.Expressions.Expression> parameters)
        {
            return Expression.Divide(
                Expression.Constant(1.0),
                Expression.Call(typeof(Math).GetMethod("Cos", new Type[] { typeof(double) }), this.Op.CompileInternal(parameters))
                );
        }

        public override string ToCode()
        {
            return "(_F(1.0)/cos(" + this.Op.ToCode() + "))";
        }
    }
}
