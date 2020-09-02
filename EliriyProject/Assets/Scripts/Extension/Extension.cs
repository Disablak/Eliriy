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
    
    public static T randomElement<T>(this IEnumerable<T> enumerable)
    {
        if ( enumerable == null || !enumerable.Any() )
            Debug.LogError( $"IEnumerable<{typeof(T).Name}> {nameof(enumerable)} is null or empty!" );

        int index = Random.Range( 0, enumerable.Count() );
        return enumerable.ElementAt(index);
    }
}
