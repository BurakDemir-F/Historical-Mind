using System.Collections.Generic;
using General;
using UnityEngine;
using Utilities;

namespace UI
{
    public static class FadeManagerItemHolder
    {

        private static List<FadeAnimationManager> _fadeManagers = new List<FadeAnimationManager>();

        public static void Add(FadeAnimationManager fadeManager)
        {
            _fadeManagers.UniqueAdd(fadeManager);
        }

        public static void Remove(FadeAnimationManager fadeManager)
        {
            _fadeManagers.SafeRemove(fadeManager);
        }

        public static void PlayFadeIn()
        {
            foreach (var fadeManager in _fadeManagers)
            {
                fadeManager.PlayFadeIn();
            }
        }

        public static void PlayFadeOut()
        {
            foreach (var fadeManager in _fadeManagers)
            {
                fadeManager.PlayFadeOut();
            }
        }

    }
}
