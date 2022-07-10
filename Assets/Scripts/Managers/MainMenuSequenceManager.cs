using UI;
using UnityEngine;
using System.Linq;

namespace Managers
{
    public class MainMenuSequenceManager : MonoBehaviour
    {
        private BFadeManager[] _fadeManagers;

        private void OnValidate()
        {
            _fadeManagers = FindObjectsOfType<BFadeManager>();
        }


        private void Start()
        {
            foreach (var bFadeManager in _fadeManagers)
            {
                bFadeManager.PlayFadeInAnimation();
            }
        }
    }
}
