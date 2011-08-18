using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FvCalculation.PrimitiveExpressions;
using System.Linq.Expressions;

namespace FvCalculation.OperatorExpressions
{
    class MulExpression : RawExpression
    {
        public RawExpression Left { get; set; }
        public RawExpression Right { get; set; }

        public override double Execute(Dictionary<string, double> variables)
        {
            return this.Left.Execute(variables) * this.Right.Execute(variables);
        }

        public override RawExpression Apply(Dictionary<string, double> variables)
        {
            return new MulExpression
            {
                Left = this.Left.Apply(variables),
                Right = this.Right.Apply(variables),
            };
        }

        public override RawExpression Different(string variable)
        {
            return new AddExpression
            {
                Left = new MulExpression
                {
                    Left = this.Left.Different(variable),
                    Right = this.Right,
                },
                Right = new MulExpression
                {
                    Left = this.Left,
                    Right = this.Right.Different(variable),
                },
            };
        }

        public override bool ContainsVariable(string variable)
        {
            return this.Left.ContainsVariable(variable) || this.Right.ContainsVariable(variable);
        }

        public override RawExpression SimplifyInternal()
        {
            RawExpression sleft = this.Left.Simplify();
            RawExpression sright = this.Right.Simplify();
            NumberExpression nleft = sleft as NumberExpression;
            NumberExpression nright = sright as NumberExpression;
            if (nleft != null)
            {
                if (nleft.Number == 0)
                {
                    return new NumberExpression
                    {
                        Number = 0,
                    };
                }
                else if (nleft.Number == 1)
                {
                    return sright;
                }
            }
            else if (nright != null)
            {
                if (nright.Number == 0)
                {
                    return new NumberExpression
                    {
                        Number = 0,
                    };
                }
                else if (nright.Number == 1)
                {
                    return sleft;
                }
            }
            return new MulExpression
            {
                Left = sleft,
                Right = sright,
            };
        }

        public override Expression CompileInternal(Dictionary<string, Expression> parameters)
        {
            return Expression.Multiply(this.Left.CompileInternal(parameters), this.Right.CompileInternal(parameters));
        }

        public override string ToCode()
        {
            return "(" + this.Left.ToCode() + " * " + this.Right.ToCode() + ")";
        }
    }
}
