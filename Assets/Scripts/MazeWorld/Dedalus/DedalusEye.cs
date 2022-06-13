using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;
using Object = System.Object;

namespace MazeWorld.Dedalus
{
    public class DedalusEye : MonoBehaviour
    {
        [SerializeField]private CreatureObjectDictionary dedalusEyeData = new CreatureObjectDictionary();
        [SerializeField] private Locator locator;
        private Dictionary<MazeWorldCreatures, GameObject> _shownData = new Dictionary<MazeWorldCreatures, GameObject>();
        
        
        public void ShowCreatures(List<MazeWorldCreatures> creatures)
        {
            foreach (var creature in creatures)
            {
                if (_shownData.ContainsKey(creature))
                {
                    _shownData[creature].SetActive(true);
                }
                else
                {
                    var newCreature = Instantiate(dedalusEyeData[creature].prefab);
                    newCreature.transform.position = locator.GetPosition();
                    _shownData.Add(creature,newCreature);
                }
            }
        }
        
    }
    
    [Serializable]
    public class CreatureObjectDictionary : SerializableDictionary<MazeWorldCreatures,PointerScriptableObject>{}
    
}