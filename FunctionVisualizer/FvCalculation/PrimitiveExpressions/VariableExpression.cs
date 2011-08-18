using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace FvCalculation.PrimitiveExpressions
{
    class VariableExpression : RawExpression
    {
        public string Name { get; set; }

        public override double Execute(Dictionary<string, double> variables)
        {
            return variables[this.Name];
        }

        public override RawExpression Apply(Dictionary<string, double> variables)
        {
            double value = 0;
            if (variables.TryGetValue(this.Name, out value))
            {
                return new NumberExpression
                {
                    Number = value,
                };
            }
            else
            {
                return this;
            }
        }

        public override RawExpression Different(string variable)
        {
            if (this.Name == variable)
            {
                return new NumberExpression
                {
                    Number = 1,
                };
            }
            else
            {
                return new NumberExpression
                {
                    Number = 0,
                };
            }
        }

        public override bool ContainsVariable(string variable)
        {
            return this.Name == variable;
        }

        public override RawExpression SimplifyInternal()
        {
            return this;
        }

        public override Expression CompileInternal(Dictionary<string, Expression> parameters)
        {
            return parameters[this.Name];
        }

        public override string ToCode()
        {
            return this.Name;
        }
    }
}
