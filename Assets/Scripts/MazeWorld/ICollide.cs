using UnityEngine;

namespace MazeWorld
{
    public interface ICollide
    {
        public void OnTriggerEnter(Collider collider);
    }
}