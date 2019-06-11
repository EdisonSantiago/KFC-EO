using System;
using System.Collections;
using System.Collections.Generic;

namespace Oulanka.Domain.Common
{
    [Serializable]
    public class PagedList<T> : IPagedList, IEnumerable<T>
    {
        public List<T> Items { get; private set; }
        public int TotalPages { get; set; }
        public long TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public PagedList()
        {
            Items = new List<T>();
        }

        public PagedList(IEnumerable<T> source, long totalCount, int pageIndex, int pageSize)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalCount = totalCount;
            TotalPages = (int)(TotalCount / pageSize);

            if (TotalCount % pageSize > 0)
                TotalPages++;

            Items = new List<T>(source);
        }

        public bool HasPreviousPage => (PageIndex > 0);

        public bool HasNextPage => PageIndex < TotalPages - 1;

        public int FirstItem => (PageIndex * PageSize) + 1;

        public int LastItem => FirstItem + PageSize - 1;

        public IEnumerator<T> GetEnumerator()
        {

            return Items.GetEnumerator();

        }

        IEnumerator IEnumerable.GetEnumerator()
        {

            return Items.GetEnumerator();

        }
    }
}