using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FvCalculation.OperatorExpressions;
using System.Linq.Expressions;

namespace FvCalculation.FunctionExpressions
{
    [FunctionName("exp")]
    class ExpExpression : FunctionExpression
    {
        public override double Execute(Dictionary<string, double> variables)
        {
            return Math.Exp(this.Op.Execute(variables));
        }

        public override RawExpression Different(string variable)
        {
            return new MulExpression
            {
                Left = this,
                Right = this.Op.Different(variable),
            };
        }

        public override Expression CompileInternal(Dictionary<string, Expression> parameters)
        {
            return Expression.Call(typeof(Math).GetMethod("Exp", new Type[] { typeof(double) }), this.Op.CompileInternal(parameters));
        }
    }
}
