using UnityEngine;

namespace Storing
{
    public class JsonDataProvider<T> : IPersistentDataProvider<T>
    {
        private string _path;
        
        public void Save(T data)
        {
            JsonUtility.ToJson(data);
        }

        public T Load()
        {
            var json = "";
            return JsonUtility.FromJson<T>(json);
        }
    }
}