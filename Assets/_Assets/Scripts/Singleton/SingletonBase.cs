using UnityEngine;

public abstract class SingletonBase<T> : MonoBehaviour
    where T : class
{
    private static T instance = null;

    public static T Instance
    {
        get
        {
            if (instance != null) return instance;
            
            var listObjects = FindObjectsOfType(typeof(T));
            if (listObjects.Length == 1)
            {
                instance = listObjects[0] as T;
                return instance;
            }
            Debug.LogError($"Number Object with Type {typeof(T)} Found Is {listObjects.Length} ");
            
            return instance;
        }
    }
}
