﻿using System.Collections.Generic;
using System.Linq;

namespace Oulanka.Domain.Common
{
    public class PaginationSet<T>
    {
        public int Page { get; set; }

        public int Count => Items?.Count() ?? 0;

        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}