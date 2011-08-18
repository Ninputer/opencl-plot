using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FvCalculation.PrimitiveExpressions;
using FvCalculation.FunctionExpressions;
using System.Linq.Expressions;
using System.Reflection;

namespace FvCalculation.OperatorExpressions
{
    class PowerExpression : RawExpression
    {
        public RawExpression Left { get; set; }
        public RawExpression Right { get; set; }

        private static double Calculate(double a, double b)
        {
            double ib = (int)Math.Round(b);
            if (b >= 0 && -RawExpression.ZeroNumber <= (ib - b) && (ib - b) <= RawExpression.ZeroNumber)
            {
                if (ib == 0)
                {
                    return 1;
                }
                else
                {
                    double result = 1;
                    for (int i = 0; i < ib; i++)
                    {
                        result *= a;
                    }
                    return result;
                }
            }
            else
            {
                return Math.Exp(b * Math.Log(a));
            }
        }

        public override double Execute(Dictionary<string, double> variables)
        {
            double a = this.Left.Execute(variables);
            double b = this.Right.Execute(variables);
            return Calculate(a, b);
        }

        public override RawExpression Apply(Dictionary<string, double> variables)
        {
            return new PowerExpression
            {
                Left = this.Left.Apply(variables),
                Right = this.Right.Apply(variables),
            };
        }

        public override RawExpression Different(string variable)
        {
            bool lf = this.Left.ContainsVariable(variable);
            bool rf = this.Right.ContainsVariable(variable);
            if (lf)
            {
                if (rf)
                {
                    return new ExpExpression
                    {
                        Op = new MulExpression
                        {
                            Left = this.Right,
                            Right = new LnExpression
                            {
                                Op = this.Left,
                            },
                        },
                    }.Different(variable);
                }
                else
                {
                    return new MulExpression
                    {
                        Left = this.Right,
                        Right = new PowerExpression
                        {
                            Left = this.Left,
                            Right = new SubExpression
                            {
                                Left = this.Right,
                                Right = new NumberExpression
                                {
                                    Number = 1,
                                },
                            },
                        },
                    };
                }
            }
            else
            {
                if (rf)
                {
                    return new MulExpression
                    {
                        Left = this,
                        Right = new LnExpression
                        {
                            Op = this.Left,
                        },
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
            if (nright != null)
            {
                if (nright.Number == 0)
                {
                    return new NumberExpression
                    {
                        Number = 1,
                    };
                }
                else if (nright.Number == 1)
                {
                    return sleft;
                }
            }
            else if (nleft != null)
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
                    return new NumberExpression
                    {
                        Number = 1,
                    };
                }
            }
            return new PowerExpression
            {
                Left = sleft,
                Right = sright,
            };
        }

        public override Expression CompileInternal(Dictionary<string, Expression> parameters)
        {
            MethodInfo methodInfo = typeof(PowerExpression).GetMethod("Calculate", BindingFlags.NonPublic | BindingFlags.Static);
            return Expression.Call(methodInfo, this.Left.CompileInternal(parameters), this.Right.CompileInternal(parameters));
        }

        public override string ToCode()
        {
            return "pow(" + this.Left.ToCode() + ", " + this.Right.ToCode() + ")";
        }
    }
}
