using Abp.Application.Services.Dto;
using MESCloud.Entities.WMS.ProduceData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MESCloud.CommonDto
{
    public class PagedResultRequestMESDto : IPagedResultRequest
    {
        public int SkipCount { get; set; }
        public int MaxResultCount { get; set; }


        public List<RequestMESDto> RequestMESDtos { get; set; }

        public string SortName { get; set; }

        public bool Desc { get; set; }

    }

    public class RequestMESDto
    {
        public string PropertyName { get; set; }

        public Operation Operation { get; set; }

        public object QueryValue { get; set; }

        public LinkOperation LinkOperation { get; set; }

        public List<RequestMESDto> RequestMESDtos { get; set; }
    }

    public enum LinkOperation
    {
        And = 0,
        Or
    }

    /// <summary>
    /// 操作符
    /// </summary>
    public enum Operation
    {
        Equal = 0,
        NotEqual,
        Contains,
        StartsWith,
        EndsWith,
        NotContains,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual,
        Empry,
        NotEmpty,
        Null,
        NotNull,
        RegEx
    }



    public static class MESPagedResult
    {
        public static IQueryable<T> GetMESPagedResult<T>(PagedResultRequestMESDto pagedResultRequestMESDto, IQueryable<T> sourceData)
        {

            Expression totalExpr = null;
            Type queryConditionType = typeof(T);

            ParameterExpression param = Expression.Parameter(queryConditionType, "n");
            var properties = queryConditionType.GetProperties();

            if (pagedResultRequestMESDto.RequestMESDtos != null)
            {
                totalExpr = GetWhereExpression(pagedResultRequestMESDto.RequestMESDtos, param, properties);
            }
            if (totalExpr == null)
            {
                totalExpr = Expression.Constant(true);
            }

            //Where部分条件
            Expression pred = Expression.Lambda(totalExpr, param);

            Expression whereExpression = Expression.Call(typeof(Queryable), "Where", new Type[] { queryConditionType }, Expression.Constant(sourceData), pred);

            //OrderBy部分排序
            if (pagedResultRequestMESDto.SortName != null)
            {
                var orderBy = properties.Where(p => p.Name.ToLower() == pagedResultRequestMESDto.SortName.ToLower()).FirstOrDefault();

                if (orderBy == null)
                {
                    sourceData = sourceData.Provider.CreateQuery<T>(whereExpression);
                }
                else
                {
                    MethodCallExpression orderByCallExpression = Expression.Call
                    (
                       typeof(Queryable),
                       pagedResultRequestMESDto.Desc ? "OrderByDescending" : "OrderBy",
                       new Type[] { queryConditionType, orderBy.PropertyType },
                       whereExpression,
                       Expression.Lambda(Expression.Property(param, orderBy.Name), param)
                    );

                    sourceData = sourceData.Provider.CreateQuery<T>(orderByCallExpression);
                }
            }
            else
            {
                sourceData = sourceData.Provider.CreateQuery<T>(whereExpression);
            }
            // 获取 SQL 语句已注释
            // var sql = sourceData.ToSql();

            // 分页已移动到外面
            // sourceData = sourceData.Skip(pagedResultRequestMESDto.SkipCount).Take(pagedResultRequestMESDto.MaxResultCount);    

            return sourceData;
        }

        private static Expression GetWhereExpression(List<RequestMESDto> requestMESDtos, ParameterExpression param, PropertyInfo[] properties)
        {

            Expression totalExpr = null;
            foreach (var requestMESDto in requestMESDtos)
            {
                Expression filter = null;
                if (requestMESDto.RequestMESDtos == null || requestMESDto.RequestMESDtos.Count == 0)
                {
                    if (requestMESDto.PropertyName == null)
                    {
                        continue;
                    }

                    var propertie = properties.Where(p => p.Name.ToLower() == requestMESDto.PropertyName.ToLower()).FirstOrDefault();
                    if (propertie == null)
                    {
                        continue;
                    }

                    try
                    {
                        Type type = propertie.PropertyType;
                        Expression left = null;
                        Expression right = null;

                        if (IsNullableType(type))
                        {
                            left = Expression.Property(Expression.Property(param, propertie), "Value");
                            type = type.GetProperty("Value").PropertyType;
                        }
                        else
                        {
                            left = Expression.Property(param, propertie);
                        }

                        //propertie.PropertyType
                        //等式右边的值    并将右边的数据类型强转为左边的   
                        if (type.BaseType.Name == "Enum")
                        {
                            right = Expression.Constant(System.Enum.Parse(type, requestMESDto.QueryValue.ToString(), true));
                        }
                        else
                        {

                            if (type.Name == "DateTime")
                            {
                                // requestMESDto.QueryValue = ((DateTime)requestMESDto.QueryValue).ToLocalTime();
                                right = Expression.Constant(Convert.ChangeType(((DateTime)requestMESDto.QueryValue).ToLocalTime(), type));
                            }
                            else
                            {
                                right = Expression.Constant(Convert.ChangeType(requestMESDto.QueryValue, type));
                            }
                        }



                        //各种操作的具体处理                  
                        switch (requestMESDto.Operation)
                        {
                            case Operation.Contains:
                                if (requestMESDto.QueryValue.ToString() != "")
                                {
                                    filter = Expression.Call(left, type.GetMethod(requestMESDto.Operation.ToString()), right);
                                }
                                break;
                            case Operation.NotContains:
                            case Operation.RegEx:
                            case Operation.StartsWith:
                            case Operation.EndsWith:
                                filter = Expression.Call(typeof(MESPagedResult).GetMethod(requestMESDto.Operation.ToString()), left, right);
                                break;
                            case Operation.NotEmpty:
                            case Operation.NotNull:
                                filter = Expression.Call(typeof(MESPagedResult).GetMethod(requestMESDto.Operation.ToString()), left);
                                break;
                            case Operation.Equal:
                                filter = Expression.Equal(left, right);
                                break;
                            case Operation.NotEqual:
                                filter = Expression.NotEqual(left, right);
                                break;
                            case Operation.GreaterThan:
                                filter = Expression.GreaterThan(left, right);
                                break;
                            case Operation.GreaterThanOrEqual:
                                filter = Expression.GreaterThanOrEqual(left, right);
                                break;
                            case Operation.LessThan:
                                filter = Expression.LessThan(left, right);
                                break;
                            case Operation.LessThanOrEqual:
                                filter = Expression.LessThanOrEqual(left, right);
                                break;
                            case Operation.Empry:
                                filter = Expression.Equal(left, Expression.Constant(""));
                                break;
                            case Operation.Null:
                                filter = Expression.Equal(left, Expression.Constant(null));
                                break;
                        }
                        if (filter != null)
                        {
                            if (totalExpr == null)
                            {

                                totalExpr = filter;
                            }
                            else
                            {
                                switch (requestMESDto.LinkOperation)
                                {
                                    case LinkOperation.And:
                                        totalExpr = Expression.And(filter, totalExpr);
                                        break;
                                    case LinkOperation.Or:
                                        totalExpr = Expression.Or(filter, totalExpr);
                                        break;
                                }
                            }
                        }

                    }
                    catch
                    {

                    }
                }
                else
                {
                    var subExpression = GetWhereExpression(requestMESDto.RequestMESDtos, param, properties);


                    if (subExpression != null)
                    {
                        if (totalExpr == null)
                        {

                            totalExpr = subExpression;

                        }
                        else
                        {
                            switch (requestMESDto.LinkOperation)
                            {

                                case LinkOperation.And:
                                    totalExpr = Expression.And(GetWhereExpression(requestMESDto.RequestMESDtos, param, properties), totalExpr);
                                    break;
                                case LinkOperation.Or:
                                    totalExpr = Expression.Or(GetWhereExpression(requestMESDto.RequestMESDtos, param, properties), totalExpr);
                                    break;
                            }
                        }
                    }


                }
            }

            return totalExpr;
        }

        public static bool IsNullableType(Type theType)
        {
            return (theType.IsGenericType && theType.
              GetGenericTypeDefinition().Equals
              (typeof(Nullable<>)));
        }

        public static bool NotContains(object t, string str)
        {
            return !(t.ToString().Contains(str));
        }
        public static bool NotEmpty(object t)
        {
            return !(t.ToString() == "");
        }
        public static bool NotNull(object t)
        {
            return !(t.ToString() == null);
        }

        public static bool RegEx(object t, string regEx)
        {
            return Regex.IsMatch(t.ToString(), regEx);
        }

        public static bool StartsWith(object t, string str)
        {
            return t.ToString().StartsWith(str);
        }

        public static bool EndsWith(object t, string str)
        {
            return t.ToString().EndsWith(str);
        }
    }
}
