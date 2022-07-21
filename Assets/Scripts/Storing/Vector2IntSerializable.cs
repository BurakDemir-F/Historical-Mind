using UnityEngine;

namespace Storing
{
    [System.Serializable]
    public class Vector2IntSerializable
    {
        public int x;
        public int y;
        
        
        public Vector2IntSerializable(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}