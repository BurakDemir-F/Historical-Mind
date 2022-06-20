
using UnityEngine;

namespace Utilities
{
    public static class VectorExtensions
    {
        public static bool IsInRangeXToY(this Vector2 vector2, float number)
        {
            return vector2.x < number && number <= vector2.y;
        }
    }
}
