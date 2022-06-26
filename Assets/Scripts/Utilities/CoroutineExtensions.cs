using System.Collections;
using UnityEngine;

namespace Utilities
{
    public static class CoroutineExtensions
    {
        public static Coroutine StartSingleCoroutine(this MonoBehaviour monoBehaviour, IEnumerator enumerator,ref Coroutine corHolder)
        {
            if(corHolder != null)
            {
                monoBehaviour.StopCoroutine(corHolder);
            }

            return corHolder = monoBehaviour.StartCoroutine(enumerator);
        }
    }//test coroutines for checking when storing a coroutine in enumerator variable when coroutine finish is variable became null or not, can we start coroutine multiple times on enumerator variable
}
