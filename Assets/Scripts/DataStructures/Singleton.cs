using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;

    public static T Instance
    {
        get 
        { 
            if (instance == null)
            {
                T[] objects = Resources.FindObjectsOfTypeAll<T>();
                instance = objects == null || objects.Length == 0 ? null : objects[0];
                if (instance == null)
                {
                    GameObject gameObject = new GameObject();
                    instance = gameObject.AddComponent<T>();
                }
            }
            return instance;
        }
    }
}