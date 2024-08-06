using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Extensions
{
    public static class SortExtension
    {
        public static List<TSource> OrderByExtension<TSource>(this IEnumerable<TSource> sources, string propertyName)
        {
            var parameter = Expression.Parameter(typeof(TSource), "x");
            var property = Expression.Property(parameter, propertyName);
            if (property == null)
                throw new ArgumentException($"Property '{propertyName}' not found on type '{typeof(TSource)}'.");

            var lambda = Expression.Lambda(property, parameter);

            var orderByMethod = typeof(Enumerable).GetMethods()
                .First(x => x.Name == "OrderBy"
                         && x.GetParameters().Length == 2
                         && x.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == typeof(Func<,>));

            var orderByGeneric = orderByMethod.MakeGenericMethod(typeof(TSource), property.Type);

            var result = orderByGeneric.Invoke(null, new object[] { sources, lambda.Compile() });

            return ((IOrderedEnumerable<TSource>)result).ToList();
        }
        
        public static List<TSource> OrderByDescending<TSource>(this IEnumerable<TSource> sources, string propertyName)
        {
            var parameter = Expression.Parameter(typeof(TSource), "x");
            var property = Expression.Property(parameter, propertyName);
            if (property == null)
                throw new ArgumentException($"Property '{propertyName}' not found on type '{typeof(TSource)}'.");

            var lambda = Expression.Lambda(property, parameter);

            var orderByMethod = typeof(Enumerable).GetMethods()
                .First(x => x.Name == "OrderByDescending"
                         && x.GetParameters().Length == 2
                         && x.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == typeof(Func<,>));

            var orderByGeneric = orderByMethod.MakeGenericMethod(typeof(TSource), property.Type);

            var result = orderByGeneric.Invoke(null, new object[] { sources, lambda.Compile() });

            return ((IOrderedEnumerable<TSource>)result).ToList();
        }
    }
}
