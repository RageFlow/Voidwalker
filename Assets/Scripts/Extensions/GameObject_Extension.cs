using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObject_Extension
{
    public static void RemoveChildren(this GameObject gameObject)
    {
        for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(gameObject.transform.GetChild(i).gameObject);
        }
    }
    public static void RemoveChildren(this Transform gameObject)
    {
        for (var i = gameObject.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(gameObject.GetChild(i).gameObject);
        }
    }
}
