using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/CreatureDistanceDistribution", fileName = "CreatureDistanceDistribution", order = 0)]
    public class CreatureDistanceDistribution : ScriptableObject
    {
        [SerializeField]private Vector2 starter,regular,boss;
        public Vector2 Starter => starter;
        public Vector2 Regular => regular;
        public Vector2 Boss => boss;
    }
    
}
