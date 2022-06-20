using MazeWorld;
using UnityEngine;

namespace ScriptableObjects
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
        [Tooltip("Danger value means how much closer to the goal room, " +
                 "when getting closer to the goal room, creatures must be more powerful.")]
        [SerializeField] private float dangerValue;

        public MazeWorldCreatures Kind => kind;
        public int Health => health;
        public int Power => power;
        public int Damage => damage;
        public GameObject Flesh => flesh;
        public float DangerValue => dangerValue;
    }
}