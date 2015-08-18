using System;
using System.Linq.Expressions;

namespace SFML
{
    namespace System
    {
        internal static class ExpressionUtility
        {
            public static Func<TArg, TResult> CreateExpression<TArg, TResult>(Func<Expression, UnaryExpression> Body)
            {
                ParameterExpression arg = Expression.Parameter(typeof(TArg), "arg");

                return Expression.Lambda<Func<TArg, TResult>>(Body(arg), arg).Compile();
            }

            public static Func<TLhs, TRhs, TResult> CreateExpression<TLhs, TRhs, TResult>(Func<Expression, Expression, BinaryExpression> Body)
            {
                ParameterExpression lhs = Expression.Parameter(typeof(TLhs), "lhs");
                ParameterExpression rhs = Expression.Parameter(typeof(TRhs), "rhs");

                return Expression.Lambda<Func<TLhs, TRhs, TResult>>(Body(lhs, rhs), lhs, rhs).Compile();
            }
        }
    }
}
