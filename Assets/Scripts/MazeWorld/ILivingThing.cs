using UnityEngine;

namespace MazeWorld
{
    public interface ILivingThing
    {
        public MazeWorldCreatures Kind { get; }
        public int Health { get; }
        public int Power { get; }
        public int Damage { get; }
        public GameObject Flesh { get; }
    }
}