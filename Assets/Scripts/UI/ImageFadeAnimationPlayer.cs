using System;
using DG.Tweening;
using General;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class ImageFadeAnimationPlayer : MonoBehaviour,IFadeAnimationPlayer
    {
        [SerializeField] private float fadeInWaitTime,fadeOutWaitTime,animationPlayTime;
        [SerializeField] private bool hookController = true;
        private IItemHolder<IFadeAnimationPlayer> _itemHolder;
        private Image _image;

        private void OnValidate()
        {
            _itemHolder = gameObject.GetComponentFromAllTransform<IItemHolder<IFadeAnimationPlayer>>(null);
            _image = GetComponent<Image>();
        }

        private void OnEnable()
        {
            if(hookController) _itemHolder?.Add(this);
        }

        private void OnDisable()
        {
            if(hookController) _itemHolder?.Remove(this);
        }

        public void PlayFadeInAnimation()
        {
            //_image.SetAlpha(0);

            var sequence = DOTween.Sequence();
            sequence.AppendInterval(fadeInWaitTime);
            sequence.Append(_image.DOColor(_image.color.GetFullAlpha(), animationPlayTime));
        }

        public void PlayFadeOutAnimation()
        {
            //_image.SetAlpha(1);

            var sequence = DOTween.Sequence();
            sequence.AppendInterval(fadeOutWaitTime);
            sequence.Append(_image.DOColor(_image.color.GetZeroAlpha(), animationPlayTime));
        }
    }
}
