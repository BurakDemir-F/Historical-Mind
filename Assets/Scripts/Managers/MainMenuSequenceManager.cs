using UI;
using UnityEngine;
using System.Linq;

namespace Managers
{
    public class MainMenuSequenceManager : MonoBehaviour
    {
        private void Start()
        {
            FadeManagerItemHolder.PlayFadeIn();
        }
    }
}
