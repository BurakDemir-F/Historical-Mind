using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/CreatureDistanceDistribution", fileName = "CreatureDistanceDistribution", order = 0)]
    public class CreatureDistanceDistribution : ScriptableObject
    {
        [field:SerializeField]public List<Vector2> DistanceDistributions { get; private set; }
    }
    
}
