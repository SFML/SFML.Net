using System;
using System.Linq.Expressions;

namespace SFML
{
    namespace System
    {
        static class Operator<T>
        {
            public static T Add(T lhs, T rhs)
            {
                return addFunction(lhs, rhs);
            }

            public static T Subtract(T lhs, T rhs)
            {
                return subtractFunction(lhs, rhs);
            }

            public static T Multiply(T lhs, T rhs)
            {
                return multiplyFunction(lhs, rhs);
            }

            public static T Divide(T lhs, T rhs)
            {
                return divideFunction(lhs, rhs);
            }

            public static T Negate(T arg)
            {
                return negateFunction(arg);
            }

            public static bool Equal(T lhs, T rhs)
            {
                return equalFunction(lhs, rhs);
            }

            private static readonly Func<T, T, T> addFunction;
            private static readonly Func<T, T, T> subtractFunction;
            private static readonly Func<T, T, T> multiplyFunction;
            private static readonly Func<T, T, T> divideFunction;

            private static readonly Func<T, T> negateFunction;

            private static readonly Func<T, T, bool> equalFunction;

            static Operator()
            {
                addFunction = ExpressionUtility.CreateExpression<T, T, T>(Expression.Add);
                subtractFunction = ExpressionUtility.CreateExpression<T, T, T>(Expression.Subtract);
                multiplyFunction = ExpressionUtility.CreateExpression<T, T, T>(Expression.Multiply);
                divideFunction = ExpressionUtility.CreateExpression<T, T, T>(Expression.Divide);

                negateFunction = ExpressionUtility.CreateExpression<T, T>(Expression.Negate);

                equalFunction = ExpressionUtility.CreateExpression<T, T, bool>(Expression.Equal);
            }
        }
    }
}
