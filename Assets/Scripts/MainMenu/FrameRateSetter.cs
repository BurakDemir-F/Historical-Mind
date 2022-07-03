using UnityEngine;

namespace MainMenu
{
    public class FrameRateSetter : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }
}
