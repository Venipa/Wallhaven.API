using System;
using System.Collections.Generic;

namespace Wallhaven.API
{
    public enum WallhavenSortOrder
    {
        Ascending,
        Descending
    }
    public static class WallhavenSortOrderExtension
    {
        private static List<Tuple<WallhavenSortOrder, string>> SortFilters = new List<Tuple<WallhavenSortOrder, string>>()
        {
            new Tuple<WallhavenSortOrder, string>(WallhavenSortOrder.Ascending, "asc"),
            new Tuple<WallhavenSortOrder, string>(WallhavenSortOrder.Descending, "desc")
        };
        public static string Value(this WallhavenSortOrder sort) => SortFilters.Find(x => x.Item1 == sort).Item2;
    }
}
