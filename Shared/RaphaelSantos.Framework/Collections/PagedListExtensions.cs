using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace RaphaelSantos.Framework.Collections
{
    public static class PagedListExtensions
    {
        /// <summary>
        /// Returns the ordered list
        /// </summary>
        /// <typeparam name="EntityType">Entity Type</typeparam>
        /// <param name="source">Entity Framework collection</param>
        /// <param name="filter">Sort information</param>
        /// <returns>A collection containing the page records</returns>
        public static IOrderedQueryable<EntityType> ToOrdered<EntityType>(this IQueryable<EntityType> source, EntityFilter<EntityType> filter)
            where EntityType : class
        {
            IOrderedQueryable<EntityType> sorted = null;

            if (filter.SortExpression != null)
            {
                sorted = Sort<EntityType>(source, filter.SortDescending, filter.SortExpression);
            }
            else
            {
                sorted = source.OrderBy(i => 1);
            }

            return sorted;
        }

        /// <summary>
        /// Returns the paged list
        /// </summary>
        /// <typeparam name="EntityType">Entity type</typeparam>
        /// <param name="source">Entity Framework collection</param>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>A collection containing page records</returns>
        public static PagedList<EntityType> ToPaged<EntityType>(this IOrderedQueryable<EntityType> source, int pageNumber, int pageSize)
            where EntityType : class
        {
            return new PagedList<EntityType>(source, pageNumber, pageSize);
        }

        /// <summary>
        /// Returns the paged and ordered list
        /// </summary>
        /// <typeparam name="EntityType">Entity type</typeparam>
        /// <param name="source">Entity Framework collection</param>
        /// <param name="filter">Sorting and Paging Information</param>
        /// <returns>A collection containing page records</returns>
        public static PagedList<EntityType> ToPaged<EntityType>(this IQueryable<EntityType> source, EntityFilter<EntityType> filter)
            where EntityType : class
        {
            var sorted = ToOrdered(source, filter);

            var result = ToPaged(sorted, filter.PageNumber, filter.PageSize);

            return result;
        }

        private static IOrderedQueryable<EntityType> Sort<EntityType>(IQueryable<EntityType> source, bool descending, LambdaExpression expression)
        {
            var methodName = descending ? "OrderByDescending" : "OrderBy";

            var all = typeof(Queryable).GetMethods(BindingFlags.Static | BindingFlags.Public);
            var method = all.FirstOrDefault(i => i.Name == methodName && i.GetParameters().Length == 2);

            var typeArgs = new Type[] { typeof(EntityType), expression.ReturnType };
            var generic = method.MakeGenericMethod(typeArgs);
            var args = new object[] { source, expression };

            var query = generic.Invoke(null, args);
            var result = query as IOrderedQueryable<EntityType>;

            return result;
        }
    }
}
