using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = gameObject.GetComponent<T>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
