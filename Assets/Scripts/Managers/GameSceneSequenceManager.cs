using System;
using UI;
using UnityEngine;

namespace Managers
{
    public class GameSceneSequenceManager : MonoBehaviour
    {
        private BFadeManager[] _fadeManagers;

        private void Awake()
        {
            _fadeManagers = FindObjectsOfType<BFadeManager>();
        }

        private void Start()
        {
            print($"fade manager count{_fadeManagers.Length}");
            
            foreach (var fadeManager in _fadeManagers)
            {
                fadeManager.PlayFadeInAnimation();
            }
        }
    }
}
