
using Storing;
using UnityEngine;

namespace Utilities
{
    public static class VectorExtensions
    {
        public static bool IsInRangeXToY(this Vector2 vector2, float number)
        {
            return vector2.x < number && number <= vector2.y;
        }

        public static Vector2IntSerializable ToSerializable(this Vector2Int vector2Int)
        {
            return new Vector2IntSerializable(vector2Int.x,vector2Int.y);
        }
    }
}
