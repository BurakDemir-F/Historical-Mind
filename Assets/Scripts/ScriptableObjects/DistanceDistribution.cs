using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/DistanceDistribution", fileName = "DistanceDistribution", order = 0)]
    public class DistanceDistribution : ScriptableObject
    {
        [field:SerializeField]public List<Vector2> DistanceDistributions { get; private set; }
    }
    
}
