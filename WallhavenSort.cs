using System;
using System.Collections.Generic;
using System.Text;

namespace Wallhaven.API
{
    public enum WallhavenSort
    {
        DateAdded,
        Relevance,
        Random,
        Views,
        Fav,
        Top
    }
    public static class WallhavenSortExtension
    {
        private static List<Tuple<WallhavenSort, string>> SortFilters = new List<Tuple<WallhavenSort, string>>()
        {
            new Tuple<WallhavenSort, string>(WallhavenSort.DateAdded, "date_added"),
            new Tuple<WallhavenSort, string>(WallhavenSort.Relevance, "relevance"),
            new Tuple<WallhavenSort, string>(WallhavenSort.Random, "random"),
            new Tuple<WallhavenSort, string>(WallhavenSort.Views, "views"),
            new Tuple<WallhavenSort, string>(WallhavenSort.Fav, "favorites"),
            new Tuple<WallhavenSort, string>(WallhavenSort.Top, "toplist")
        };
        public static string Value(this WallhavenSort sort) => SortFilters.Find(x => x.Item1 == sort).Item2;
    }
}
