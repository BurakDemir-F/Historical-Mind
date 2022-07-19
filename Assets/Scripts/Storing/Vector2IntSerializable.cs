using UnityEngine;

namespace Storing
{
    [System.Serializable]
    public struct Vector2IntSerializable
    {
        private int _x;
        private int _y;

        public int X => _x;
        public int Y => _y;
        
        public Vector2IntSerializable(int x, int y)
        {
            _x = x;
            _y = y;
        }
    }
}