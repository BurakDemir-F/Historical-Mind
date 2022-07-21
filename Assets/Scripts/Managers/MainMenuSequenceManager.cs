using System.Collections;
using UI;
using UnityEngine;
using System.Linq;
using UnityEngine.Rendering;

namespace Managers
{
    public class MainMenuSequenceManager : MonoBehaviour
    {
        private void Start()
        {
            //yield return StartCoroutine(WaitForSplashScreen());
            FadeManagerItemHolder.PlayFadeIn();
        }

        private IEnumerator WaitForSplashScreen()
        {
            Debug.Log("Showing splash screen");
            SplashScreen.Begin();
            while (!SplashScreen.isFinished)
            {
                SplashScreen.Draw();
                yield return null;
            }
            Debug.Log("Finished showing splash screen");
        }
    }
}
