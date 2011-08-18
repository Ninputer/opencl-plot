using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FvCalculation.PrimitiveExpressions;
using System.Linq.Expressions;

namespace FvCalculation.OperatorExpressions
{
    class NegExpression : RawExpression
    {
        public RawExpression Op { get; set; }

        public override double Execute(Dictionary<string, double> variables)
        {
            return -this.Op.Execute(variables);
        }

        public override RawExpression Apply(Dictionary<string, double> variables)
        {
            return new NegExpression
            {
                Op = this.Op.Apply(variables),
            };
        }

        public override RawExpression Different(string variable)
        {
            return new NegExpression
            {
                Op = this.Op.Different(variable),
            };
        }

        public override bool ContainsVariable(string variable)
        {
            return this.Op.ContainsVariable(variable);
        }

        public override RawExpression SimplifyInternal()
        {
            return new NegExpression
            {
                Op = this.Op.Simplify(),
            };
        }

        public override Expression CompileInternal(Dictionary<string, Expression> parameters)
        {
            return Expression.Negate(this.Op.CompileInternal(parameters));
        }

        public override string ToCode()
        {
            return "(-" + this.Op.ToCode() + ")";
        }
    }
}
