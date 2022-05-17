using Algorithms;
using UnityEngine;

namespace MiniMapScripts
{
    public class MiniMapCell : MonoBehaviour, IMiniMapObject
    {
        public Vector2Int Position { get; private set; }
        public float RememberingTime { get; private set; }
        public bool IsForgotten { get; private set; }

        public void SetMiniMapCell(Vector2Int position, float rememberingTime)
        {
            Position = position;
            RememberingTime = rememberingTime;
        }

        public void DecreaseTime(float time)
        {
            if(IsForgotten) return;

            RememberingTime -= time;
            
            if (RememberingTime <= 0) IsForgotten = true;
        }

        public void SetRememberingTime(float time)
        {
            RememberingTime = time;
        }
        
    }
}