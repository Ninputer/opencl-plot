using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace FvCalculation.PrimitiveExpressions
{
    class NumberExpression : RawExpression
    {
        public double Number { get; set; }

        public override double Execute(Dictionary<string, double> variables)
        {
            return this.Number;
        }

        public override RawExpression Apply(Dictionary<string, double> variables)
        {
            return this;
        }

        public override RawExpression Different(string variable)
        {
            return new NumberExpression
            {
                Number = 0,
            };
        }

        public override bool ContainsVariable(string variable)
        {
            return false;
        }

        public override RawExpression SimplifyInternal()
        {
            return this;
        }

        public override Expression CompileInternal(Dictionary<string, Expression> parameters)
        {
            return Expression.Constant(this.Number);
        }

        public override string ToCode()
        {
            return "_F(" +  this.Number.ToString("#.0", CultureInfo.InvariantCulture) + ")";
        }
    }
}
