using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using Utilities;

namespace MazeWorld.Dedalus
{
    public class DedalusEye : MonoBehaviour
    {
        [SerializeField]private CreatureObjectDictionary dedalusEyeData;
        [SerializeField] private Locator locator;
        private readonly Dictionary<MazeWorldCreatures, GameObject> _shownData = new ();
        
        public void ShowCreatures(List<MazeWorldCreatures> creatures)
        {
            foreach (var creature in creatures)
            {
                if (_shownData.ContainsKey(creature))
                {
                    _shownData[creature].SetActive(true);
                    continue;
                }

                var newCreature = Instantiate(dedalusEyeData[creature].prefab, transform, true);
                newCreature.transform.position = locator.GetPosition();
                _shownData.Add(creature, newCreature);
            }
        }

        [ContextMenu("Show Creatures")]
        public void ShowCreaturesTest()
        {
            ShowCreatures(new List<MazeWorldCreatures>(){MazeWorldCreatures.Player,MazeWorldCreatures.Ghost});
        }

        [ContextMenu("Restart")]
        public void Restart()
        {
            locator.Restart();
            foreach (var creaturePointer in _shownData)
            {
                DestroyImmediate(creaturePointer.Value);
            }
            _shownData.Clear();
        }
    }

    [Serializable]
    public class CreatureObjectDictionary : SerializableDictionary<MazeWorldCreatures, PointerScriptableObject>
    {
        
    }
    
}