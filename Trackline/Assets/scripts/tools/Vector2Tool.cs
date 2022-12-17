using UnityEngine;

namespace Tools
{
    public static class Vector2Tool
    {
        public static Vector2 ScreenToLocalByRect(this Vector2 origin, RectComponent rect)
        {
            return rect.World2Local(origin);
        }
    }
}
