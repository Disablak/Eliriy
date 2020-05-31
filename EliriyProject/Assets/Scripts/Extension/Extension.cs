using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Extension
{
    public static T[] getComponentAllChildren<T>( this Transform transform )
    {
        T[] all = transform.GetComponentsInChildren<T>();
        return all.Skip(1).ToArray();
        
    }
}
