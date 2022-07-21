using System;
using General;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class TmpFadeAnimationPlayer : MonoBehaviour,IFadeAnimationPlayer
    {
        
        [SerializeField] private float fadeInWaitTime,fadeOutWaitTime,animationPlayTime,fullAlphaValue = 1;
        [SerializeField] private bool hookController = true;
        private IItemHolder<IFadeAnimationPlayer> _itemHolder;
        private TMP_Text _tmp;

        private void Awake()
        {
            _itemHolder = gameObject.GetComponentFromAllTransform<IItemHolder<IFadeAnimationPlayer>>(null);
            _tmp = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            if (hookController) _itemHolder.Add(this);
        }

        private void OnDisable()
        {
            if (hookController) _itemHolder.Add(this);
        }

        public void PlayFadeInAnimation()
        {
            var cor = _tmp.DoColor(_tmp.color.GetZeroAlpha(),
                _tmp.color.GetWithNewAlpha(fullAlphaValue),
                fadeInWaitTime,
                animationPlayTime);
            StartCoroutine(cor);
        }

        public void PlayFadeOutAnimation()
        {
            var cor = _tmp.DoColor(_tmp.color.GetFullAlpha(),
                _tmp.color.GetZeroAlpha(),
                fadeOutWaitTime,
                animationPlayTime);
            StartCoroutine(cor);
        }
    }
}
