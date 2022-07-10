using DG.Tweening;
using General;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class BImage : Image, IFadeInOut
    {
        private IItemHolder<IFadeInOut> _itemHolder;

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

        public void SetAlpha(float value)
        {
            color = new Color(color.r,color.g,color.b,value);
        }

        public Color GetColor()
        {
            return color;
        }

        public Image GetImage()
        {
            return this;
        }

        public void FadeIn(float time)
        {
            this.DOColor(color.GetFullAlpha(), time);
        }

        public void FadeOut(float time)
        {
            this.DOColor(color.GetZeroAlpha(), time);
        }
    }
}
