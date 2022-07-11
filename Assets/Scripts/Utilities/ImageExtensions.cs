using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    public static class ImageExtensions 
    {
        public static void SetAlpha(this Image image, float alphaValue)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, alphaValue);
        }
    }
}
