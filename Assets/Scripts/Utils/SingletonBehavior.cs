﻿using UnityEngine;

public class SingletonBehavior<T> : MonoBehaviour where T : Component
{
    private static T instance;
    
    public static T Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<T> ();
                if (instance == null) {
                    GameObject obj = new GameObject {name = typeof(T).Name};
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }
 
    public virtual void Awake ()
    {
        if (instance == null) {
            instance = this as T;
        } else {
            Destroy (gameObject);
        }
    }
}