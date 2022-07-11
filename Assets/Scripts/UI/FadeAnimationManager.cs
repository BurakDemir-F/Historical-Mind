using System;
using System.Collections.Generic;
using General;
using UnityEngine;

namespace UI
{
    public class FadeAnimationManager : MonoBehaviour,IItemHolder<IFadeAnimationPlayer>
    {
        private List<IFadeAnimationPlayer> _fadeAnimationPlayers = new List<IFadeAnimationPlayer>();

        private void OnEnable()
        {
            FadeManagerItemHolder.Add(this);
        }

        private void OnDisable()
        {
            FadeManagerItemHolder.Remove(this);
        }
        
        public void Add(IFadeAnimationPlayer item)
        {
            if(!_fadeAnimationPlayers.Contains(item)) _fadeAnimationPlayers.Add(item);
        }

        public void Remove(IFadeAnimationPlayer item)
        {
            if(_fadeAnimationPlayers.Contains(item)) _fadeAnimationPlayers.Remove(item);
        }

        public void PlayFadeIn()
        {
            foreach (var fadeAnimationPlayer in _fadeAnimationPlayers)
            {
                fadeAnimationPlayer.PlayFadeInAnimation();
            }
        }
        
        public void PlayFadeOut()
        {
            foreach (var fadeAnimationPlayer in _fadeAnimationPlayers)
            {
                fadeAnimationPlayer.PlayFadeOutAnimation();
            }
        }
    }
}
