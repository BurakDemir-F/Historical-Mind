using Algorithms;
using MazeWorld;
using UnityEngine;

namespace ScriptableObjects
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "ScriptableObjects/LivingThing", fileName = "LivingThing", order = 0)]
    public class LivingThing : ScriptableObject, ILivingThing,IHasDistanceValue
    {
        [SerializeField] private MazeWorldCreatures kind;
        [SerializeField] private int health;
        [SerializeField] private int power;
        [SerializeField] private int damage;
        [SerializeField] private GameObject flesh;
        [Tooltip("Distance value means how much closer to the goal room, " +
                 "when getting closer to the goal room, creatures must be more powerful." +
                 " Less Distance value more harmful")]
        [SerializeField] private float distanceValue;

        public MazeWorldCreatures Kind => kind;
        public int Health => health;
        public int Power => power;
        public int Damage => damage;
        public GameObject Flesh => flesh;
        public float DistanceValue => distanceValue;
    }
}