using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FvCalculation.OperatorExpressions;
using System.Linq.Expressions;

namespace FvCalculation.FunctionExpressions
{
    [FunctionName("ln")]
    class LnExpression : FunctionExpression
    {
        public override double Execute(Dictionary<string, double> variables)
        {
            return Math.Log(this.Op.Execute(variables));
        }

        public override RawExpression Different(string variable)
        {
            return new DivExpression
            {
                Left = this.Op.Different(variable),
                Right = this.Op,
            };
        }

        public override Expression CompileInternal(Dictionary<string, Expression> parameters)
        {
            return Expression.Call(typeof(Math).GetMethod("Log", new Type[] { typeof(double) }), this.Op.CompileInternal(parameters));
        }

        public override string ToCode()
        {
            return "log(" + Op.ToCode() + ")";
        }
    }
}
