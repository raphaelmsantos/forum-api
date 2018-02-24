using System.Collections.Generic;
using System.Linq;

namespace RaphaelSantos.Framework.Collections
{
    /// <summary>
    /// Paged list
    /// </summary>
    /// <typeparam name="EntityType">Entity type</typeparam>
    public sealed class PagedList<EntityType> : IPagedList
        where EntityType : class
    {
        private int _pageNumber = 0;
        private IEnumerable<EntityType> _items;

        private readonly static IEnumerable<EntityType> EmptyList = Enumerable.Empty<EntityType>();

        public PagedList(IOrderedQueryable<EntityType> source, int pageNumber, int pageSize)
        {
            Items = Enumerable.Empty<EntityType>();
            PageSize = pageSize;
            PageNumber = pageNumber;

            if (source == null || !source.Any())
                return;

            IEnumerable<EntityType> paged = source;

            if (pageSize > 0)
            {
                var start = PageSize * (PageNumber - 1);
                paged = source.Skip(start).Take(PageSize);
            }

            Items = paged;
            RecordCount = source.Count();
        }

        public PagedList(IEnumerable<EntityType> source, IPagedList info)
        {
            Items = source ?? EmptyList;

            if (info != null)
            {
                PageSize = info.PageSize;
                RecordCount = info.RecordCount;
                PageNumber = info.PageNumber;
            }
        }

        public PagedList(IEnumerable<EntityType> source) : this(source, null)
        {
        }

        public IEnumerable<EntityType> Items
        {
            get
            {
                if (_items == null)
                    _items = EmptyList;

                return _items;
            }
            private set
            {
                _items = value;
            }
        }

        public int PageSize { get; set; }

        public int RecordCount { get; set; }

        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                if (value <= 0)
                    _pageNumber = 1;

                if (value > PageCount)
                    _pageNumber = PageCount;

                _pageNumber = value;
            }
        }

        public int PageCount
        {
            get
            {
                if (PageSize == 0 || RecordCount == 0)
                    return 0;

                var remainder = RecordCount % PageSize;
                int count = RecordCount / PageSize;

                return remainder > 0 ? ++count : count;
            }
        }

        public static PagedList<EntityType> Empty()
        {
            return new PagedList<EntityType>(EmptyList);
        }

    }
}
