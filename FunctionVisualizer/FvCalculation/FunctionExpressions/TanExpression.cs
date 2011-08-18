using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using FvCalculation.OperatorExpressions;
using FvCalculation.PrimitiveExpressions;

namespace FvCalculation.FunctionExpressions
{
    [FunctionName("tan")]
    class TanExpression : FunctionExpression
    {
        public override double Execute(Dictionary<string, double> variables)
        {
            return Math.Tan(this.Op.Execute(variables));
        }

        public override RawExpression Different(string variable)
        {
            return new MulExpression
            {
                Left = new PowerExpression
                {
                    Left = new SecExpression
                    {
                        Op = this.Op,
                    },
                    Right = new NumberExpression
                    {
                        Number = 2,
                    },
                },
                Right = this.Op.Different(variable),
            };
        }

        public override Expression CompileInternal(Dictionary<string, System.Linq.Expressions.Expression> parameters)
        {
            return Expression.Call(typeof(Math).GetMethod("Tan", new Type[] { typeof(double) }), this.Op.CompileInternal(parameters));
        }
    }
}
