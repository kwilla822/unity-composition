using System;
using System.Linq;
using System.Linq.Expressions;

using Newtonsoft.Json.Linq;

namespace FiveWattGroup.Services.Common.Serializers.Linq
{
    internal static class ExpressionSerializer<TEntity> where TEntity : class
    {
        public static Expression Deserialize(JToken expressionNode, ParameterExpression entity = null)
        {
            var nodeType = expressionNode["NodeType"].ToString();
            var exprType = (ExpressionType)Enum.Parse(typeof(ExpressionType), nodeType);
            var targetEntityType = typeof(TEntity);

            entity = entity ?? Expression.Parameter(targetEntityType, "x");

            switch (exprType)
            {
                case ExpressionType.Lambda:
                    var body = Deserialize(expressionNode["Body"], entity);
                    var returnTypeName = expressionNode["ReturnType"].ToString();
                    var returnType = Type.GetType(returnTypeName);
                    var funcDelegateType = typeof(Func<,>).MakeGenericType(targetEntityType, returnType);
                    return Expression.Lambda(funcDelegateType, body, entity);

                case ExpressionType.Equal:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.NotEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                    return DeserializeBinaryExpression(expressionNode, entity, exprType);

                case ExpressionType.MemberAccess:
                    var member = expressionNode["Member"];
                    var name = (string)member["Name"];
                    var className = member["ClassName"].ToString();
                    if (className == targetEntityType.FullName)
                    {
                        var properties = targetEntityType.GetProperties().Select((info, i) => new { Index = i, Name = info.Name, Info = info, PropExpression = Expression.Property(entity, info) }).ToArray();
                        return Expression.MakeMemberAccess(entity, properties.First(p => p.Name == name).Info);
                    }
                    var expression = expressionNode["Expression"];
                    return Expression.Constant(expression["Value"][name].ToString());

                case ExpressionType.Constant:
                    var value = expressionNode["Value"].ToString();
                    return Expression.Constant(value);

                case ExpressionType.Not:
                    var operand = Deserialize(expressionNode["Operand"], entity);
                    return Expression.Not(operand);

                default:
                    throw new NotImplementedException(exprType.ToString());
            }
        }

        private static BinaryExpression DeserializeBinaryExpression(JToken expressionNode, ParameterExpression entity, ExpressionType binaryType)
        {
            var left = Deserialize(expressionNode["Left"], entity);
            var right = Deserialize(expressionNode["Right"], entity);
            
            return Expression.MakeBinary(binaryType, left, right);
        }
    }
}