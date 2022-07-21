using UnityEngine;

namespace Storing
{
    public static class JsonOperator<T>
    {
        public static string  ToJson(T data)
        {
            return JsonUtility.ToJson(data);
        }

        public static T FromJson(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }
    }
}