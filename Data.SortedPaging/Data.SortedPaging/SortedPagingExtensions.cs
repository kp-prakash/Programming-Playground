#region References

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#endregion References

namespace Data.SortedPaging
{
    public static class SortedPagingExtensions
    {
        #region Public Methods

        public static IQueryable<TSource> GetFirstPage<TSource, TKey>(this IQueryable<TSource> query,
            Expression<Func<TSource, TKey>> keySelector, int pageSize, SortOrder sortOrder = SortOrder.Ascending)
        {
            ValidateParameters(query, keySelector, pageSize);
            query = OrderByKeySelector(query, keySelector, sortOrder);
            return query.Skip(0 * pageSize).Take(pageSize);
        }

        public static IQueryable<TSource> GetFirstPage<TSource, TKey>(this IQueryable<TSource> query,
            Expression<Func<TSource, TKey>> keySelector, int pageSize, IComparer<TKey> comparer,
            SortOrder sortOrder = SortOrder.Ascending)
        {
            ValidateParameters(query, keySelector, pageSize);
            ValidateComparer(comparer);
            query = OrderByKeySelectorAndComparer(query, keySelector, comparer, sortOrder);
            return query.Skip(0 * pageSize).Take(pageSize);
        }

        public static IQueryable<TSource> GetNextPage<TSource, TKey>(this  IQueryable<TSource> query,
            Expression<Func<TSource, TKey>> keySelector, int pageSize, int currentPage,
            SortOrder sortOrder = SortOrder.Ascending)
        {
            ValidateParameters(query, keySelector, pageSize);
            ValidateCurrentPage(currentPage);
            query = OrderByKeySelector(query, keySelector, sortOrder);
            return query.Skip(currentPage * pageSize).Take(pageSize);
        }

        public static IQueryable<TSource> GetNextPage<TSource, TKey>(this  IQueryable<TSource> query,
            Expression<Func<TSource, TKey>> keySelector, int pageSize, IComparer<TKey> comparer,
            int currentPage, SortOrder sortOrder = SortOrder.Ascending)
        {
            ValidateParameters(query, keySelector, pageSize);
            ValidateCurrentPage(currentPage);
            ValidateComparer(comparer);
            query = OrderByKeySelectorAndComparer(query, keySelector, comparer, sortOrder);
            return query.Skip(currentPage * pageSize).Take(pageSize);
        }

        public static IQueryable<TSource> GetPreviousPage<TSource, TKey>(this  IQueryable<TSource> query,
            Expression<Func<TSource, TKey>> keySelector, int pageSize, int currentPage,
            SortOrder sortOrder = SortOrder.Ascending)
        {
            ValidateParameters(query, keySelector, pageSize);
            ValidateCurrentPage(currentPage);
            if (currentPage == 1) return GetFirstPage(query, keySelector, pageSize, sortOrder);
            query = OrderByKeySelector(query, keySelector, sortOrder);
            return query.Skip((currentPage - 2) * pageSize).Take(pageSize);
        }

        public static IQueryable<TSource> GetPreviousPage<TSource, TKey>(this  IQueryable<TSource> query,
            Expression<Func<TSource, TKey>> keySelector, int pageSize, IComparer<TKey> comparer,
            int currentPage, SortOrder sortOrder = SortOrder.Ascending)
        {
            ValidateParameters(query, keySelector, pageSize);
            ValidateCurrentPage(currentPage);
            ValidateComparer(comparer);
            if (currentPage == 1) return GetFirstPage(query, keySelector, pageSize, comparer, sortOrder);
            query = OrderByKeySelectorAndComparer(query, keySelector, comparer, sortOrder);
            return query.Skip((currentPage - 2) * pageSize).Take(pageSize);
        }

        public static IQueryable<TSource> GetLastPage<TSource, TKey>(this IOrderedQueryable<TSource> query,
            Expression<Func<TSource, TKey>> keySelector, int pageSize, SortOrder sortOrder = SortOrder.Ascending)
        {
            ValidateParameters(query, keySelector, pageSize);
            int count = query.Count();
            int totalPages = count / pageSize;
            int remainingItems = count % pageSize;
            if (remainingItems == 0) totalPages--;
            return GetNextPage(query, keySelector, pageSize, totalPages, sortOrder);
        }

        public static IQueryable<TSource> GetLastPage<TSource, TKey>(this IOrderedQueryable<TSource> query,
            Expression<Func<TSource, TKey>> keySelector, int pageSize, IComparer<TKey> comparer,
            SortOrder sortOrder = SortOrder.Ascending)
        {
            ValidateParameters(query, keySelector, pageSize);
            ValidateComparer(comparer);
            int count = query.Count();
            int totalPages = count / pageSize;
            int remainingItems = count % pageSize;
            if (remainingItems == 0) totalPages--;
            return GetNextPage(query, keySelector, pageSize, comparer, totalPages, sortOrder);
        }

        public static bool IsLastPage<TSource, TKey>(this IOrderedQueryable<TSource> query,
            Expression<Func<TSource, TKey>> keySelector, int pageSize, int currentPageNumber)
        {
            int totalPages = GetTotalNumberOfPages(query, pageSize);
            return totalPages == currentPageNumber;
        }

        public static int GetTotalNumberOfPages<TSource>(this IOrderedQueryable<TSource> query, int pageSize)
        {
            int count = query.Count();
            int totalPages = count / pageSize;
            int remainingItems = count % pageSize;
            if (remainingItems != 0) totalPages++;
            return totalPages;
        }

        #endregion Public Methods

        #region Private Methods

        private static IQueryable<TSource> OrderByKeySelector<TSource, TKey>(IQueryable<TSource> query,
            Expression<Func<TSource, TKey>> keySelector, SortOrder sortOrder)
        {
            query = (sortOrder == SortOrder.Ascending)
                        ? query.OrderBy(keySelector)
                        : query.OrderByDescending(keySelector);
            return query;
        }

        private static IQueryable<TSource> OrderByKeySelectorAndComparer<TSource, TKey>(IQueryable<TSource> query, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer, SortOrder sortOrder)
        {
            query = (sortOrder == SortOrder.Ascending)
                        ? query.OrderBy(keySelector, comparer)
                        : query.OrderByDescending(keySelector, comparer);
            return query;
        }

        private static void ValidateParameters<TSource, TKey>(IQueryable<TSource> query,
            Expression<Func<TSource, TKey>> keySelector, int pageSize)
        {
            if (null == query) throw new ArgumentException("Query is null.", "query");
            if (null == keySelector) throw new ArgumentException("Key selector is null.", "keySelector");
            if (pageSize <= 0) throw new ArgumentException("Please use valid page size.", "pageSize");
        }

        private static void ValidateComparer<TKey>(IComparer<TKey> comparer)
        {
            if (null == comparer) throw new ArgumentException("Comparer is null.", "comparer");
        }

        private static void ValidateCurrentPage(int currentPage)
        {
            if (currentPage < 0) throw new ArgumentException("Please use valid page number.", "currentPage");
        }

        #endregion Private Methods
    }
}