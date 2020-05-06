using System;
using System.Collections.Generic;
using System.Text;

namespace Wallhaven.API
{
    [Flags]
    public enum WallhavenCategory
    {
        General,
        Anime,
        People,
        A = Anime,
        P = People,
        G = General,
    }
}
