using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Extension
{
    public static T[] getComponentAllChildrenArray<T>( this Transform transform )
    {
        T[] all = transform.GetComponentsInChildren<T>();
        return all.Skip(1).ToArray();
    }

    public static List<T> getComponentAllChildrenList<T>( this Transform transform )
    {
        T[] all = transform.GetComponentsInChildren<T>();
        return all.Skip(1).ToList();
    }
}
