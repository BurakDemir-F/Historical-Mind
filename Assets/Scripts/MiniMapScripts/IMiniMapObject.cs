using UnityEngine;

namespace MiniMapScripts
{
    public interface IMiniMapObject
    {
        public Vector2Int Position { get;  }
        public float RememberingTime { get;  }
        public bool IsForgotten { get;  }
    }
}