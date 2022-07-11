using System.Collections;
using TMPro;
using UnityEngine;

namespace Utilities
{
    public static class TmpExtensions
    {
        public static IEnumerator DoColor(this TMP_Text tmp, Color startColor, Color endColor, float waitTime,
            float animationTime)
        {
            yield return new WaitForSeconds(waitTime);
            
            var elapsedTime = 0f;
            var waitFrame = new WaitForEndOfFrame();
            
            while (elapsedTime < animationTime)
            {
                var newAlpha = Mathf.Lerp(startColor.a, endColor.a, elapsedTime / animationTime);
                tmp.color = tmp.color.GetWithNewAlpha(newAlpha);
                elapsedTime += Time.deltaTime;
                yield return waitFrame;
            }
            
            tmp.color = endColor;
        } 
    }
}
