﻿using UnityEngine;
using System.Collections;

public static class ColliderUtils
{
/*  public static bool Contains (UI collider, Vector3  position)
    {
        if (collider.bounds.center.z != position.z) {
            Debug.LogError ("Somos WARNING: The items don't have the same z coordenate.");
        }
        return collider.bounds.Contains (position);
    }

    public static bool AreCollisioning (Collider collider, Collider anotherCollider)
    {
        return AreCollisioning (collider.bounds, anotherCollider.bounds);
    }

    public static bool AreCollisioning (Bounds bounds, Bounds anotherBounds)
    {
        Vector3 distance = bounds.center - anotherBounds.center;
        distance = VectorUtils.AbsoluteValues (distance);

        if (bounds.extents.x + anotherBounds.extents.x < distance.x)
            return false;
        if (bounds.extents.y + anotherBounds.extents.y < distance.y)
            return false;
        return true;
    }
*/
    public static bool IsFullyInside(UIWidget container, UIWidget targetObject)
    {
        return IsFullyInside(GetBounds(container), GetBounds(targetObject));
    }

    public static bool IsFullyInside(Bounds bounds, Bounds anotherBounds)
    {
        // So the have the same z value and are sited in the same plane
        bounds.center = new Vector3(bounds.center.x, bounds.center.y, 0);
        anotherBounds.center = new Vector3(anotherBounds.center.x, anotherBounds.center.y, 0);

        //TODO: ver si se puede hacer mas performante (sin crear +4 vectores nuevos)
        return bounds.Contains(anotherBounds.center + new Vector3(0, anotherBounds.extents.y)) && 
            bounds.Contains(anotherBounds.center - new Vector3(0, anotherBounds.extents.y)) && 
            bounds.Contains(anotherBounds.center + new Vector3(anotherBounds.extents.x, 0)) && 
            bounds.Contains(anotherBounds.center - new Vector3(anotherBounds.extents.x, 0));
    }

    public static Bounds GetBounds(UIWidget NGUIObject)
    {
        return new Bounds(GetGlobalPosition(NGUIObject.transform), NGUIObject.localSize);
    }
    
    private static Vector3 GetGlobalPosition(Transform transform)
    {
        return transform.localPosition + ((transform.parent == null) ? Vector3.zero : GetGlobalPosition(transform.parent));
    }
}
