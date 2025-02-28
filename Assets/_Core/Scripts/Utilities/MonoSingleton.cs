using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static volatile T instance;

    public static T ins
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindFirstObjectByType(typeof(T)) as T;
            return instance;
        }
    }
}
