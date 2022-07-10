using System.Collections;
using General;
using TMPro;
using UnityEngine;
using Utilities;

namespace UI
{
    public class BTextMeshPro : TextMeshProUGUI, IFadeInOut
    {

        private IItemHolder<IFadeInOut> _itemHolder;
 
        public float alphaStart = 0;
        public float alphaEnd = 1;

        protected override void OnValidate()
        {
            base.OnValidate();
            _itemHolder = gameObject.GetComponentFromAllTransform<IItemHolder<IFadeInOut>>(null);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _itemHolder?.Add(this);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _itemHolder?.Remove(this);
        }

        public void FadeIn(float time)
        {
            StartCoroutine(DoFadeAnimation(alphaStart,alphaEnd, time));
        }

        public void FadeOut(float time)
        {
            StartCoroutine(DoFadeAnimation(alphaEnd,alphaStart, time));
        }

        private IEnumerator DoFadeAnimation(float from, float to, float time)
        {
            var elapsedTime = 0f;
            var waitFrame = new WaitForEndOfFrame();

            while (elapsedTime < time)
            {
                var newAlpha = Mathf.Lerp(from, to, elapsedTime / time);
                color = color.GetWithNewAlpha(newAlpha);
                elapsedTime += Time.deltaTime;
                yield return waitFrame;
            }

            color = color.GetWithNewAlpha(to);
        }
    }
}