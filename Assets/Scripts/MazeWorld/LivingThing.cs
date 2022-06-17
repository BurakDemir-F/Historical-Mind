using UnityEngine;
using UnityEngine.Serialization;

namespace MazeWorld
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "ScriptableObjects/LivingThing", fileName = "LivingThing", order = 0)]
    public class LivingThing : ScriptableObject, ILivingThing
    {
        [SerializeField] private MazeWorldCreatures kind;
        [SerializeField] private int health;
        [SerializeField] private int power;
        [SerializeField] private int damage;
        [SerializeField] private GameObject flesh;

        public MazeWorldCreatures Kind => kind;
        public int Health => health;
        public int Power => power;
        public int Damage => damage;
        public GameObject Flesh => flesh;
    }
}