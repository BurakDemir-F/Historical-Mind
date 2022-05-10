using UnityEngine;

namespace Algorithms
{
    public interface IBackTrace
    {
        Vector2Int Position { get; }
        bool IsVisited { get; }
    }
}