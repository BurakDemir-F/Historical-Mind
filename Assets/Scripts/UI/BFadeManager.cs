using System.Collections.Generic;
using DG.Tweening;
using General;
using UnityEngine;
using Utilities;

namespace UI
{
    public class BFadeManager : MonoBehaviour,IItemHolder<IFadeInOut>,IFadeAnimationPlayer
    {
        private readonly List<IFadeInOut> _images = new ();
        [SerializeField] private float fadeTime = 1;
        public void Add(IFadeInOut item)
        {
            if(!_images.Contains(item)) _images.Add(item);
        }

        public void Remove(IFadeInOut item)
        {
            if(_images.Contains(item)) _images.Remove(item);
        }

        public void PlayFadeOutAnimation()
        {
            foreach (var image in _images)
            {
                image.FadeOut(fadeTime);
            }
        }
        
        public void PlayFadeInAnimation()
        {
            foreach (var image in _images)
            {
                image.FadeIn(fadeTime);
            }
        }
    }
}