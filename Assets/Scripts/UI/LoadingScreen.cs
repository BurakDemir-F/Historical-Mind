using System;
using System.Collections.Generic;
using AYellowpaper;
using Patterns;
using UnityEngine;

namespace UI
{
    public class LoadingScreen : Singleton<LoadingScreen>
    {
        private IFadeAnimationPlayer[] _animationPlayers;

        private void Start()
        {
            _animationPlayers = GetComponentsInChildren<IFadeAnimationPlayer>();
        }

        public void PlayFadeIn()
        {
            print("play fade in called on loading screen");
            
            foreach (var fadePlayer in _animationPlayers)
            {
                fadePlayer.PlayFadeInAnimation();
            }
        }
        
        public void PlayFadeOut()
        {
            foreach (var fadePlayer in _animationPlayers)
            {
                fadePlayer.PlayFadeOutAnimation();
            }
        }
    }
}
